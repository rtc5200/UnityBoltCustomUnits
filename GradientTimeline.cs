using Bolt;
using Ludiq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TypeIcon(typeof(Color))]
[UnitCategory("カスタム")]
[UnitTitle("グラデーション")]
public class GradientTimeline : Unit
{
    [Serialize]
    private int _keycount = 0;
    [Inspectable,UnitHeaderInspectable("キー数")]
    public int KeyCount { get { return _keycount; } set { _keycount = Mathf.Clamp(value, 0, 8); } }
    [Inspectable, UnitHeaderInspectable("モード"),InspectorWide]
    public GradientMode Mode { get; private set; }
    [DoNotSerialize]
    public List<ValueInput> input = new List<ValueInput>();
    [DoNotSerialize,PortLabel("グラデーション")]
    public ValueOutput output { get; private set; }
    protected override void Definition()
    {
        input.Clear();
        for(int i = 0; i < KeyCount; i++)
        {
            var cl = ValueInput<Color>($"色{i}",Color.white);
            var prog = ValueInput<float>($"位置{i}", (float)i / KeyCount);
            input.Add(cl);
            input.Add(prog);
        }
        output = ValueOutput<Gradient>(nameof(output), (flow) => {
            var list = new ArrayList(KeyCount);
            var grad = new Gradient();
            var colorkeys = new GradientColorKey[KeyCount];
            var alphakeys = new GradientAlphaKey[KeyCount];
            for(int i = 0; i < KeyCount; i++)
            {
                var color = flow.GetValue<Color>(input[2 * i]);
                var time = flow.GetValue<float>(input[2 * i + 1]);
                colorkeys[i].color = color;
                colorkeys[i].time = time;
                alphakeys[i].alpha = color.a;
                alphakeys[i].time= time;
            }
            grad.SetKeys(colorkeys, alphakeys);
            grad.mode = Mode;
            return grad;
        });
    }

}
