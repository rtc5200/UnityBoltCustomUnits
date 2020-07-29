using Bolt;
using Ludiq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[UnitCategory("パーティクル")]
[UnitTitle("シンプルパーティクルコントロール")]
[TypeIcon(typeof(ParticleSystem))]
public class EasyParticleUnit : Unit
{
    [Inspectable, UnitHeaderInspectable("ParticleSystem"), InspectorWide]
    public ParticleSystem Particle;
    [DoNotSerialize, PortLabelHidden]
    public ControlInput Enter { get; private set; }
    [DoNotSerialize, PortLabelHidden]
    public ControlOutput Exit { get; private set; }
    protected override void Definition()
    {
        Enter = ControlInput(nameof(Enter), (flow) =>
        {
            PerformOperation(flow);
            return Exit;
        });
        Exit = ControlOutput(nameof(Exit));
        Succession(Enter, Exit);
    }
    protected void PerformOperation(Flow flow) {

    }
}
