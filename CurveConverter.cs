using Bolt;
using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[UnitCategory("カスタム")]
[UnitTitle("カーブ変換AnimCurve-->MinMaxCurve")]
[TypeIcon(typeof(AnimationCurve))]
public class CurveConverter : Unit
{
    [DoNotSerialize,PortLabel("AnimationCurve")]
    public ValueInput input { get; private set; }
    [DoNotSerialize,PortLabel("スカラー倍")]
    public ValueInput scalar { get; private set; }
    [DoNotSerialize,PortLabelHidden]
    public ValueOutput output { get; private set; }
    protected override void Definition()
    {
        input = ValueInput<AnimationCurve>(nameof(input), AnimationCurve.Linear(0,0,1f,1f));
        scalar = ValueInput<float>(nameof(scalar), 1f);
        output = ValueOutput<ParticleSystem.MinMaxCurve>(nameof(output), new Func<Flow, ParticleSystem.MinMaxCurve>((flow) => new ParticleSystem.MinMaxCurve(flow.GetValue<float>(scalar), flow.GetValue<AnimationCurve>(input))));
    }
}
