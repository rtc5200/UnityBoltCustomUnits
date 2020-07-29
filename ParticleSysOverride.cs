using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;
using System;
[UnitCategory("パーティクル")]
[TypeIcon(typeof(ParticleSystem))]
public abstract class ParticleSystemOverrideBase : Unit
{
    /// <summary>
    /// SceneVariableに格納したParticleSystemの変数の名前
    /// </summary>
    private string psvarname = "particle";
    //[Inspectable, UnitHeaderInspectable("ParticleSystem"), InspectorWide]
    protected ParticleSystem Particle;
    [DoNotSerialize,PortLabelHidden]
    public ControlInput Enter { get; private set; }
    [DoNotSerialize, PortLabelHidden]
    public ControlOutput Exit { get; private set; }

    public ParticleSystemOverrideBase() : base() {}
    protected abstract void PerformOperation(Flow flow);
    protected override void Definition()
    {
        Enter = ControlInput(nameof(Enter), (flow) =>
        {
            if(Particle == null)
                Particle = Variables.Scene(flow.stack.scene).Get<ParticleSystem>(psvarname);
            PerformOperation(flow);
            return Exit;
        }); 
        Exit = ControlOutput(nameof(Exit));
        Succession(Enter, Exit);
    }
}
[UnitTitle("メインモジュール")]
public class ParticleMainModule : ParticleSystemOverrideBase
{
    private ParticleSystem.MainModule module;
    [DoNotSerialize]
    public ValueInput GravityModifier { get; private set; }
    [DoNotSerialize]
    public ValueInput MaxParticles { get; private set; }
    [DoNotSerialize]
    public ValueInput SimulationSpeed { get; private set; }
    [DoNotSerialize, PortLabel("StartColor(色)")]
    public ValueInput StartColor { get; private set; }
    [DoNotSerialize,PortLabel("StartColor(グラデーション)")]
    public ValueInput StartColorGrad { get; private set; }
    [DoNotSerialize]
    public ValueInput StartDelay { get; private set; }
    [DoNotSerialize]
    public ValueInput StartLifetime { get; private set; }
    [DoNotSerialize]
    public ValueInput StartRotation { get; private set; }
    [DoNotSerialize]
    public ValueInput StartSize { get; private set; }
    [DoNotSerialize]
    public ValueInput StartSpeed { get; private set; }

    public ParticleMainModule() : base() { }
    protected override void Definition()
    {
        base.Definition();
        GravityModifier = ValueInput<float>(nameof(GravityModifier), 0f);
        MaxParticles = ValueInput<int>(nameof(MaxParticles), 1000);
        SimulationSpeed = ValueInput<float>(nameof(SimulationSpeed), 1f);

        StartColor = ValueInput<Color>(nameof(StartColor),Color.white);
        StartColorGrad = ValueInput<Gradient>(nameof(Gradient));

        StartDelay = ValueInput<float>(nameof(StartDelay), 0f);
        StartLifetime = ValueInput<float>(nameof(StartLifetime), 5f);
        StartRotation = ValueInput<float>(nameof(StartRotation), 0f);
        StartSize = ValueInput<float>(nameof(StartSize), 1f);
        StartSpeed = ValueInput<float>(nameof(StartSpeed), 5f);
        Requirement(GravityModifier, Enter);
        Requirement(MaxParticles, Enter);
        Requirement(SimulationSpeed, Enter);
        Requirement(StartColor, Enter);
        Requirement(StartDelay, Enter);
        Requirement(StartLifetime, Enter);
        Requirement(StartRotation, Enter);
        Requirement(StartSize, Enter);
        Requirement(StartSpeed, Enter);
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.main;
        module.gravityModifier = flow.GetValue<float>(GravityModifier);
        module.maxParticles = flow.GetValue<int>(MaxParticles);
        module.simulationSpeed = flow.GetValue<float>(SimulationSpeed);
        if (StartColorGrad.hasValidConnection)
            module.startColor = flow.GetValue<Gradient>(StartColorGrad);
        else
            module.startColor = flow.GetValue<Color>(StartColor);
        module.startDelay = flow.GetValue<float>(StartDelay);
        module.startLifetime = flow.GetValue<float>(StartLifetime);
        module.startRotation = flow.GetValue<float>(StartRotation);
        module.startSize = flow.GetValue<float>(StartSize);
        module.startSpeed = flow.GetValue<float>(StartSpeed);
    }
}
[UnitTitle("Emissionモジュール")]
public class ParticleEmissionModule : ParticleSystemOverrideBase
{
    private ParticleSystem.EmissionModule module;
    [DoNotSerialize]
    public ValueInput RateOverDistance { get; private set; }
    [DoNotSerialize]
    public ValueInput RateOverTime { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        RateOverDistance = ValueInput<float>(nameof(RateOverDistance),0f);
        RateOverTime = ValueInput<float>(nameof(RateOverTime),10f);
        Requirement(RateOverDistance, Enter);
        Requirement(RateOverTime, Enter);
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.emission;
        module.enabled = true;
        module.rateOverDistance = flow.GetValue<float>(RateOverDistance);
        module.rateOverTime = flow.GetValue<float>(RateOverTime);
    }
}
[UnitTitle("Shapeモジュール")]
public class ParticleShapeModule : ParticleSystemOverrideBase
{
    private ParticleSystem.ShapeModule module;
    [Inspectable, UnitHeaderInspectable("ShapeType"), InspectorWide]
    public ParticleSystemShapeType ShapeType { get; private set; } = ParticleSystemShapeType.Cone;
    [DoNotSerialize]
    public ValueInput AlignToDirection { get; private set; }
    [DoNotSerialize]
    public ValueInput Angle { get; private set; }
    [DoNotSerialize]
    public ValueInput Arc { get; private set; }
    [DoNotSerialize]
    public ValueInput ArcMode { get; private set; }
    [DoNotSerialize]
    public ValueInput ArcSpeed { get; private set; }
    [DoNotSerialize]
    public ValueInput ArcSpread { get; private set; }
    [DoNotSerialize]
    public ValueInput BoxThickness { get; private set; }
    [DoNotSerialize]
    public ValueInput DonutRadius { get; private set; }
    [DoNotSerialize]
    public ValueInput Length { get; private set; }
    [DoNotSerialize]
    public ValueInput Position { get; private set; }
    [DoNotSerialize]
    public ValueInput Rotation { get; private set; }
    [DoNotSerialize]
    public ValueInput Scale { get; private set; }
    [DoNotSerialize]
    public ValueInput Radius { get; private set; }
    [DoNotSerialize]
    public ValueInput RadiusMode { get; private set; }
    [DoNotSerialize]
    public ValueInput RadiusSpeed { get; private set; }
    [DoNotSerialize]
    public ValueInput RadiusSpread { get; private set; }
    [DoNotSerialize]
    public ValueInput RadiusThickness { get; private set; }
    [DoNotSerialize]
    public ValueInput RandomDirectionAmount { get; private set; }
    [DoNotSerialize]
    public ValueInput RandomPositionAmount { get; private set; }
    [DoNotSerialize]
    public ValueInput SphericalDirectionAmount { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        AlignToDirection = ValueInput<bool>(nameof(AlignToDirection),false);
        Position = ValueInput<Vector3>(nameof(Position),Vector3.zero);
        Rotation = ValueInput<Vector3>(nameof(Rotation), Vector3.zero);
        Scale = ValueInput<Vector3>(nameof(Scale), Vector3.one);
        RandomDirectionAmount = ValueInput<float>(nameof(RandomDirectionAmount),0f);
        SphericalDirectionAmount = ValueInput<float>(nameof(SphericalDirectionAmount),0f);
        RandomPositionAmount = ValueInput<float>(nameof(RandomPositionAmount),0f);
        Requirement(AlignToDirection, Enter);
        Requirement(Position, Enter);
        Requirement(Rotation, Enter);
        Requirement(Scale, Enter);
        Requirement(RandomDirectionAmount, Enter);
        Requirement(SphericalDirectionAmount, Enter);
        Requirement(RandomPositionAmount, Enter);
        switch (ShapeType)
        {
            case ParticleSystemShapeType.Cone:
                Angle = ValueInput<float>(nameof(Angle), 25f);
                Radius = ValueInput<float>(nameof(Radius), 1f);
                RadiusThickness = ValueInput<float>(nameof(RadiusThickness),1f);
                Arc = ValueInput<float>(nameof(Arc), 360f);
                ArcMode = ValueInput<ParticleSystemShapeMultiModeValue>(nameof(ArcMode), ParticleSystemShapeMultiModeValue.Random);
                ArcSpread = ValueInput<float>(nameof(ArcSpread), 0f);
                Requirement(Angle, Enter);
                Requirement(Radius, Enter);
                Requirement(RadiusThickness, Enter);
                Requirement(Arc, Enter);
                Requirement(ArcMode, Enter);
                Requirement(ArcSpread, Enter);
                break;
            case ParticleSystemShapeType.ConeVolume:
                Angle = ValueInput<float>(nameof(Angle), 25f);
                Radius = ValueInput<float>(nameof(Radius), 1f);
                RadiusThickness = ValueInput<float>(nameof(RadiusThickness), 1f);
                Arc = ValueInput<float>(nameof(Arc), 360f);
                ArcMode = ValueInput<ParticleSystemShapeMultiModeValue>(nameof(ArcMode), ParticleSystemShapeMultiModeValue.Random);
                ArcSpread = ValueInput<float>(nameof(ArcSpread), 0f);
                Length = ValueInput<float>(nameof(Length), 5f);
                Requirement(Angle, Enter);
                Requirement(Radius, Enter);
                Requirement(RadiusThickness, Enter);
                Requirement(Arc, Enter);
                Requirement(ArcMode, Enter);
                Requirement(ArcSpread, Enter);
                Requirement(Length, Enter);
                break;
            case ParticleSystemShapeType.Sphere:
            case ParticleSystemShapeType.Hemisphere:
            case ParticleSystemShapeType.Circle:
                Radius = ValueInput<float>(nameof(Radius), 1f);
                RadiusThickness = ValueInput<float>(nameof(RadiusThickness), 1f);
                Arc = ValueInput<float>(nameof(Arc), 360f);
                ArcMode = ValueInput<ParticleSystemShapeMultiModeValue>(nameof(ArcMode), ParticleSystemShapeMultiModeValue.Random);
                ArcSpread = ValueInput<float>(nameof(ArcSpread), 0f);
                Requirement(Radius, Enter);
                Requirement(RadiusThickness, Enter);
                Requirement(Arc, Enter);
                Requirement(ArcMode, Enter);
                Requirement(ArcSpread, Enter);
                break;
            case ParticleSystemShapeType.Donut:
                Radius = ValueInput<float>(nameof(Radius), 1f);
                DonutRadius = ValueInput<float>(nameof(DonutRadius), 0.2f);
                RadiusThickness = ValueInput<float>(nameof(RadiusThickness), 1f);
                Arc = ValueInput<float>(nameof(Arc), 360f);
                ArcMode = ValueInput<ParticleSystemShapeMultiModeValue>(nameof(ArcMode), ParticleSystemShapeMultiModeValue.Random);
                ArcSpread = ValueInput<float>(nameof(ArcSpread), 0f);
                Requirement(Radius, Enter);
                Requirement(DonutRadius, Enter);
                Requirement(RadiusThickness, Enter);
                Requirement(Arc, Enter);
                Requirement(ArcMode, Enter);
                Requirement(ArcSpread, Enter);
                break;
            case ParticleSystemShapeType.Rectangle:
            case ParticleSystemShapeType.Box:
                break;
            case ParticleSystemShapeType.BoxEdge:
            case ParticleSystemShapeType.BoxShell:
                BoxThickness = ValueInput<Vector3>(nameof(BoxThickness),Vector3.zero);
                Requirement(BoxThickness, Enter);
                break;
            case ParticleSystemShapeType.SingleSidedEdge:
                RadiusMode = ValueInput<ParticleSystemShapeMultiModeValue>(nameof(RadiusMode), ParticleSystemShapeMultiModeValue.Random);
                Radius = ValueInput<float>(nameof(Radius), 1f);
                RadiusSpread = ValueInput<float>(nameof(RadiusSpread), 0f);
                Requirement(RadiusMode, Enter);
                Requirement(Radius, Enter);
                Requirement(RadiusSpread, Enter);
                break;
        }
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.shape;
        module.enabled = true;
        module.alignToDirection = flow.GetValue<bool>(AlignToDirection);
        module.position = flow.GetValue<Vector3>(Position);
        module.rotation = flow.GetValue<Vector3>(Rotation);
        module.scale = flow.GetValue<Vector3>(Scale);
        module.randomDirectionAmount = flow.GetValue<float>(RandomDirectionAmount);
        module.sphericalDirectionAmount = flow.GetValue<float>(SphericalDirectionAmount);
        module.randomPositionAmount = flow.GetValue<float>(RandomPositionAmount);
        module.shapeType = ShapeType;
        switch (ShapeType)
        {
            case ParticleSystemShapeType.Cone:
                module.angle = flow.GetValue<float>(Angle);
                module.radius = flow.GetValue<float>(Radius);
                module.radiusThickness = flow.GetValue<float>(RadiusThickness);
                module.arc = flow.GetValue<float>(Arc);
                module.arcMode = flow.GetValue<ParticleSystemShapeMultiModeValue>(ArcMode);
                module.arcSpread = flow.GetValue<float>(ArcSpread);
                break;
            case ParticleSystemShapeType.ConeVolume:
                module.angle = flow.GetValue<float>(Angle);
                module.radius = flow.GetValue<float>(Radius);
                module.radiusThickness = flow.GetValue<float>(RadiusThickness);
                module.arc = flow.GetValue<float>(Arc);
                module.arcMode = flow.GetValue<ParticleSystemShapeMultiModeValue>(ArcMode);
                module.arcSpread = flow.GetValue<float>(ArcSpread);
                module.length = flow.GetValue<float>(Length);
                break;
            case ParticleSystemShapeType.Sphere:
            case ParticleSystemShapeType.Hemisphere:
            case ParticleSystemShapeType.Circle:
                module.radius = flow.GetValue<float>(Radius);
                module.radiusThickness = flow.GetValue<float>(RadiusThickness);
                module.arc = flow.GetValue<float>(Arc);
                module.arcMode = flow.GetValue<ParticleSystemShapeMultiModeValue>(ArcMode);
                module.arcSpread = flow.GetValue<float>(ArcSpread);
                break;
            case ParticleSystemShapeType.Donut:
                module.radius = flow.GetValue<float>(Radius);
                module.donutRadius = flow.GetValue<float>(DonutRadius);
                module.radiusThickness = flow.GetValue<float>(RadiusThickness);
                module.arc = flow.GetValue<float>(Arc);
                module.arcMode = flow.GetValue<ParticleSystemShapeMultiModeValue>(ArcMode);
                module.arcSpread = flow.GetValue<float>(ArcSpread);
                break;
            case ParticleSystemShapeType.Rectangle:
            case ParticleSystemShapeType.Box:
                break;
            case ParticleSystemShapeType.BoxEdge:
            case ParticleSystemShapeType.BoxShell:
                module.boxThickness = flow.GetValue<Vector3>(BoxThickness);
                break;
            case ParticleSystemShapeType.SingleSidedEdge:
                module.radiusMode = flow.GetValue<ParticleSystemShapeMultiModeValue>(RadiusMode);
                module.radius = flow.GetValue<float>(Radius);
                module.radiusSpread = flow.GetValue<float>(RadiusSpread);
                break;
        }
    }
}

[UnitTitle("VelocityOverLifetimeモジュール")]
public class ParticleVelocityOverLifetimeModule : ParticleSystemOverrideBase
{
    private ParticleSystem.VelocityOverLifetimeModule module;
    [Inspectable, UnitHeaderInspectable("Space"), InspectorWide]
    public ParticleSystemSimulationSpace Space = ParticleSystemSimulationSpace.Local;
    [DoNotSerialize]
    public ValueInput X { get; private set; }
    [DoNotSerialize]
    public ValueInput Y { get; private set; }
    [DoNotSerialize]
    public ValueInput Z { get; private set; }
    [DoNotSerialize]
    public ValueInput OrbitalX { get; private set; }
    [DoNotSerialize]
    public ValueInput OrbitalY { get; private set; }
    [DoNotSerialize]
    public ValueInput OrbitalZ { get; private set; }
    [DoNotSerialize]
    public ValueInput OffsetX { get; private set; }
    [DoNotSerialize]
    public ValueInput OffsetY { get; private set; }
    [DoNotSerialize]
    public ValueInput OffsetZ { get; private set; }
    [DoNotSerialize]
    public ValueInput Radial { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        X = ValueInput<float>(nameof(X), 1f);
        Y = ValueInput<float>(nameof(Y), 1f);
        Z = ValueInput<float>(nameof(Z), 1f);
        OrbitalX = ValueInput<float>(nameof(OrbitalX), 0f);
        OrbitalY = ValueInput<float>(nameof(OrbitalY), 0f);
        OrbitalZ = ValueInput<float>(nameof(OrbitalZ), 0f);
        OffsetX = ValueInput<float>(nameof(OffsetX), 0f);
        OffsetY = ValueInput<float>(nameof(OffsetY), 0f);
        OffsetZ = ValueInput<float>(nameof(OffsetZ), 0f);
        Radial = ValueInput<float>(nameof(Radial), 0f);
        Requirement(X, Enter);
        Requirement(Y, Enter);
        Requirement(Z, Enter);
        Requirement(OrbitalX, Enter);
        Requirement(OrbitalY, Enter);
        Requirement(OrbitalZ, Enter);
        Requirement(OffsetX, Enter);
        Requirement(OffsetY, Enter);
        Requirement(OffsetZ, Enter);
        Requirement(Radial, Enter);
    }

    protected override void PerformOperation(Flow flow)
    {
        module = Particle.velocityOverLifetime;
        module.enabled = true;
        module.space = Space;
        module.x = flow.GetValue<float>(X);
        module.y = flow.GetValue<float>(Y);
        module.z = flow.GetValue<float>(Z);
        module.orbitalX= flow.GetValue<float>(OrbitalX);
        module.orbitalY = flow.GetValue<float>(OrbitalY);
        module.orbitalZ = flow.GetValue<float>(OrbitalZ);
        module.orbitalOffsetX = flow.GetValue<float>(OffsetX);
        module.orbitalOffsetY = flow.GetValue<float>(OffsetY);
        module.orbitalOffsetZ = flow.GetValue<float>(OffsetZ);
        module.radial = flow.GetValue<float>(Radial);
    }
}

[UnitTitle("ColorOverLifetimeモジュール")]
public class ParticleColorOverLifetimeModule : ParticleSystemOverrideBase
{
    private ParticleSystem.ColorOverLifetimeModule module;
    [Inspectable, UnitHeaderInspectable("モード")]
    public ParticleSystemGradientMode Mode = ParticleSystemGradientMode.Color;
    [DoNotSerialize, PortLabel("色")]
    public ValueInput Color { get; private set; }
    [DoNotSerialize,PortLabel("色1(Min)")]
    public ValueInput Color1 { get; private set; }
    [DoNotSerialize, PortLabel("色2(Max)")]
    public ValueInput Color2 { get; private set; }
    [DoNotSerialize,PortLabel("グラデーション")]
    public ValueInput Grad { get; private set; }
    [DoNotSerialize, PortLabel("グラデーション1(Min)")]
    public ValueInput Grad1 { get; private set; }
    [DoNotSerialize, PortLabel("グラデーション2(Max)")]
    public ValueInput Grad2 { get; private set; }
    protected override void Definition()
    {
        base.Definition();
        switch (Mode)
        {
            case ParticleSystemGradientMode.Color:
                Color = ValueInput(nameof(Color), UnityEngine.Color.white);
                Requirement(Color, Enter);
                break;
            case ParticleSystemGradientMode.Gradient:
            case ParticleSystemGradientMode.RandomColor:
                Grad = ValueInput<Gradient>(nameof(Grad), new Gradient());
                Requirement(Grad, Enter);
                break;
            case ParticleSystemGradientMode.TwoColors:
                Color1 = ValueInput(nameof(Color1), UnityEngine.Color.white);
                Color2 = ValueInput(nameof(Color2), UnityEngine.Color.white);
                Requirement(Color1, Enter);
                Requirement(Color2, Enter);
                break;
            case ParticleSystemGradientMode.TwoGradients:
                Grad1 = ValueInput<Gradient>(nameof(Grad1), new Gradient());
                Grad2 = ValueInput<Gradient>(nameof(Grad2), new Gradient());
                Requirement(Grad1, Enter);
                Requirement(Grad2, Enter);
                break;
        }
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.colorOverLifetime;
        module.enabled = true;
        var mm = new ParticleSystem.MinMaxGradient();
        mm.mode = Mode;
        switch (Mode)
        {
            case ParticleSystemGradientMode.Color:
                mm.color = flow.GetValue<Color>(Color);
                break;
            case ParticleSystemGradientMode.Gradient:
            case ParticleSystemGradientMode.RandomColor:
                mm.gradient = flow.GetValue<Gradient>(Grad);
                break;
            case ParticleSystemGradientMode.TwoColors:
                mm.colorMin = flow.GetValue<Color>(Color1);
                mm.colorMax = flow.GetValue<Color>(Color2);
                break;
            case ParticleSystemGradientMode.TwoGradients:
                mm.gradientMin = flow.GetValue<Gradient>(Grad1);
                mm.gradientMax = flow.GetValue<Gradient>(Grad2);
                break;
        }
    }
}
[UnitTitle("ColorBySpeedモジュール")]
public class ParticleColorBySpeedModule : ParticleSystemOverrideBase
{
    private ParticleSystem.ColorBySpeedModule module;
    [DoNotSerialize]
    public ValueInput Color { get; private set; }
    [DoNotSerialize]
    public ValueInput SpeedMin { get; private set; }
    [DoNotSerialize]
    public ValueInput SpeedMax { get; private set; }
    protected override void Definition()
    {
        base.Definition();
        Color = ValueInput<UnityEngine.Color>(nameof(Color), UnityEngine.Color.white);
        SpeedMin = ValueInput<float>(nameof(SpeedMin), 0f);
        SpeedMax = ValueInput<float>(nameof(SpeedMax), 1f);
        Requirement(Color, Enter);
        Requirement(SpeedMin, Enter);
        Requirement(SpeedMax, Enter);
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.colorBySpeed;
        module.enabled = true;
        module.color = flow.GetValue<UnityEngine.Color>(Color);
        module.range = new Vector2(flow.GetValue<float>(SpeedMin), flow.GetValue<float>(SpeedMax));
    }
}
[UnitTitle("SizeOverLifetimeモジュール")]
public class ParticleSizeOverLifetimeModule : ParticleSystemOverrideBase
{
    private ParticleSystem.SizeOverLifetimeModule module;
    [Inspectable, UnitHeaderInspectable("Separate Axes")]
    public bool SeparateAxes { get; private set; } = false;
    [DoNotSerialize]
    public ValueInput Size { get; private set; }
    [DoNotSerialize]
    public ValueInput SizeX { get; private set; }
    [DoNotSerialize]
    public ValueInput SizeY { get; private set; }
    [DoNotSerialize]
    public ValueInput SizeZ { get; private set; }
    protected override void Definition()
    {
        base.Definition();
        if (!SeparateAxes)
        {
            Size = ValueInput<ParticleSystem.MinMaxCurve>(nameof(Size), new ParticleSystem.MinMaxCurve(0f, 1f));
            Requirement(Size, Enter);
        }   
        else
        {
            SizeX = ValueInput<ParticleSystem.MinMaxCurve>(nameof(SizeX), new ParticleSystem.MinMaxCurve(0f, 1f));
            SizeY = ValueInput<ParticleSystem.MinMaxCurve>(nameof(SizeY), new ParticleSystem.MinMaxCurve(0f, 1f));
            SizeZ = ValueInput<ParticleSystem.MinMaxCurve>(nameof(SizeZ), new ParticleSystem.MinMaxCurve(0f, 1f));
            Requirement(SizeX, Enter);
            Requirement(SizeY, Enter);
            Requirement(SizeZ, Enter);
        }
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.sizeOverLifetime;
        module.separateAxes = SeparateAxes;
        if (!SeparateAxes)
            module.size = flow.GetValue<ParticleSystem.MinMaxCurve>(Size);
        else
        {
            module.x = flow.GetValue<ParticleSystem.MinMaxCurve>(SizeX);
            module.y = flow.GetValue<ParticleSystem.MinMaxCurve>(SizeY);
            module.z = flow.GetValue<ParticleSystem.MinMaxCurve>(SizeZ);
        }
    }
}
[UnitTitle("SizeBySpeedモジュール")]
public class ParticleSizeBySpeed : ParticleSystemOverrideBase
{
    private ParticleSystem.SizeBySpeedModule module;
    [Inspectable, UnitHeaderInspectable("Separate Axes")]
    public bool SeparateAxes { get; private set; } = false;
    [DoNotSerialize]
    public ValueInput Size { get; private set; }
    [DoNotSerialize]
    public ValueInput SizeX { get; private set; }
    [DoNotSerialize]
    public ValueInput SizeY { get; private set; }
    [DoNotSerialize]
    public ValueInput SizeZ { get; private set; }
    [DoNotSerialize]
    public ValueInput SpeedMin { get; private set; }
    [DoNotSerialize]
    public ValueInput SpeedMax { get; private set; }
    protected override void Definition()
    {
        base.Definition();
        if (!SeparateAxes)
        {
            Size = ValueInput<ParticleSystem.MinMaxCurve>(nameof(Size), new ParticleSystem.MinMaxCurve(0f, 1f));
            Requirement(Size, Enter);
        }
        else
        {
            SizeX = ValueInput<ParticleSystem.MinMaxCurve>(nameof(SizeX), new ParticleSystem.MinMaxCurve(0f, 1f));
            SizeY = ValueInput<ParticleSystem.MinMaxCurve>(nameof(SizeY), new ParticleSystem.MinMaxCurve(0f, 1f));
            SizeZ = ValueInput<ParticleSystem.MinMaxCurve>(nameof(SizeZ), new ParticleSystem.MinMaxCurve(0f, 1f));
            Requirement(SizeX, Enter);
            Requirement(SizeY, Enter);
            Requirement(SizeZ, Enter);
        }
        SpeedMin = ValueInput<float>(nameof(SpeedMin), 0f);
        SpeedMax = ValueInput<float>(nameof(SpeedMax), 1f);
        Requirement(SpeedMin, Enter);
        Requirement(SpeedMax, Enter);
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.sizeBySpeed;
        module.separateAxes = SeparateAxes;
        if (!SeparateAxes)
            module.size = flow.GetValue<ParticleSystem.MinMaxCurve>(Size);
        else
        {
            
            module.x = flow.GetValue<ParticleSystem.MinMaxCurve>(SizeX);
            module.y = flow.GetValue<ParticleSystem.MinMaxCurve>(SizeY);
            module.z = flow.GetValue<ParticleSystem.MinMaxCurve>(SizeZ);
        }
        module.range = new Vector2(flow.GetValue<float>(SpeedMin), flow.GetValue<float>(SpeedMax));
    }
}
[UnitTitle("Noiseモジュール")]
public class ParticleNoise : ParticleSystemOverrideBase
{
    private ParticleSystem.NoiseModule module;
    [Inspectable, UnitHeaderInspectable("Separate Axes")]
    public bool SeparateAxes { get; private set; } = false;
    [DoNotSerialize]
    public ValueInput Strength { get; private set; }
    [DoNotSerialize]
    public ValueInput StrengthX { get; private set; }
    [DoNotSerialize]
    public ValueInput StrengthY { get; private set; }
    [DoNotSerialize]
    public ValueInput StrengthZ { get; private set; }
    [DoNotSerialize]
    public ValueInput Frequency { get; private set; }
    [DoNotSerialize]
    public ValueInput ScrollSpeed { get; private set; }
    [Inspectable, UnitHeaderInspectable("Damping")]
    public bool Damping { get; private set; } = true;
    [DoNotSerialize]
    public ValueInput Octaves { get; private set; }
    [DoNotSerialize]
    public ValueInput PositionAmount { get; private set; }
    [DoNotSerialize]
    public ValueInput RotationAmount { get; private set; }
    [DoNotSerialize]
    public ValueInput SizeAmount { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        if (!SeparateAxes)
        {
            Strength = ValueInput<float>(nameof(Strength), 1f);
            Requirement(Strength, Enter);
        }
        else
        {
            StrengthX = ValueInput<float>(nameof(StrengthX), 1f);
            StrengthY = ValueInput<float>(nameof(StrengthY), 1f);
            StrengthZ = ValueInput<float>(nameof(StrengthZ), 1f);
            Requirement(StrengthX, Enter);
            Requirement(StrengthY, Enter);
            Requirement(StrengthZ, Enter);
        }
        Frequency = ValueInput<float>(nameof(Frequency), .5f);
        ScrollSpeed = ValueInput<float>(nameof(ScrollSpeed), 0f);
        Octaves = ValueInput<int>(nameof(Octaves), 1);
        PositionAmount = ValueInput<float>(nameof(PositionAmount), 1f);
        RotationAmount = ValueInput<float>(nameof(RotationAmount), 0f);
        SizeAmount = ValueInput<float>(nameof(SizeAmount), 0f);
        Requirement(Frequency, Enter);
        Requirement(ScrollSpeed, Enter);
        Requirement(Octaves, Enter);
        Requirement(PositionAmount, Enter);
        Requirement(RotationAmount, Enter);
        Requirement(SizeAmount, Enter);
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.noise;
        module.separateAxes = SeparateAxes;
        module.damping = Damping;
        if (!SeparateAxes)
            module.strength = flow.GetValue<float>(Strength);
        else
        {
            module.strengthX = flow.GetValue<float>(StrengthX);
            module.strengthY = flow.GetValue<float>(StrengthY);
            module.strengthZ = flow.GetValue<float>(StrengthZ);
        }
        module.frequency = flow.GetValue<float>(Frequency);
        module.scrollSpeed = flow.GetValue<float>(ScrollSpeed);
        module.octaveCount = flow.GetValue<int>(Octaves);
        module.positionAmount = new ParticleSystem.MinMaxCurve(flow.GetValue<float>(PositionAmount));
        module.rotationAmount = new ParticleSystem.MinMaxCurve(flow.GetValue<float>(RotationAmount));
        module.sizeAmount = new ParticleSystem.MinMaxCurve(flow.GetValue<float>(SizeAmount));
    }
}
[UnitTitle("レンダラーモジュール")]
public class ParticleRenderer : ParticleSystemOverrideBase
{
    private ParticleSystemRenderer Renderer;
    [DoNotSerialize]
    public ValueInput Material { get; private set; }
    [DoNotSerialize]
    public ValueInput TrailMaterial { get; private set; }
    protected override void Definition()
    {
        base.Definition();
        Material = ValueInput<Material>(nameof(Material)).AllowsNull();
        TrailMaterial = ValueInput<Material>(nameof(TrailMaterial)).AllowsNull();
        Requirement(Material, Enter);
        Requirement(TrailMaterial, Enter);
    }
    protected override void PerformOperation(Flow flow)
    {
        if(Renderer == null)
            Renderer = Particle.gameObject.GetComponent<UnityEngine.ParticleSystemRenderer>();
        if (Material.hasValidConnection)
            Renderer.material = flow.GetValue<Material>(Material);
        if(TrailMaterial.hasValidConnection)
            Renderer.trailMaterial = flow.GetValue<Material>(TrailMaterial);
    }
}
[UnitTitle("Trailモジュール")]
public class ParticleTrail : ParticleSystemOverrideBase
{
    private ParticleSystem.TrailModule module;
    [Inspectable, UnitHeaderInspectable("Mode"), InspectorWide]
    public ParticleSystemTrailMode Mode { get; private set; } = ParticleSystemTrailMode.PerParticle;
    [DoNotSerialize]
    public ValueInput Ratio { get; private set; }
    [DoNotSerialize]
    public ValueInput Lifetime { get; private set; }
    [DoNotSerialize]
    public ValueInput MinimumVertexDistance { get; private set; }
    [Inspectable, UnitHeaderInspectable("DieWithParticles")]
    public bool DieWithParticles { get; private set; } = true;
    [Inspectable, UnitHeaderInspectable("Texture Mode"), InspectorWide]
    public ParticleSystemTrailTextureMode TextureMode { get; private set; } = ParticleSystemTrailTextureMode.Stretch;
    [Inspectable, UnitHeaderInspectable("SizeAffectsWidth")]
    public bool SizeAffectsWidth { get; private set; } = true;
    [DoNotSerialize]
    public ValueInput SizeAffectsLifetime { get; private set; }
    [Inspectable, UnitHeaderInspectable("InheritParticleColor")]
    public bool InheritParticleColor { get; private set; } = true;
    [DoNotSerialize]
    public ValueInput ColorOverLifetime { get; private set; }
    [DoNotSerialize]
    public ValueInput WidthOverTrail { get; private set; }
    [DoNotSerialize]
    public ValueInput ColorOverTrail { get; private set; }
    [DoNotSerialize]
    public ValueInput RibbonCount { get; private set; }
    [DoNotSerialize]
    public ValueInput SplitSubEmitterRibbons { get; private set; }
    [DoNotSerialize]
    public ValueInput AttachRibbonsToTransform { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        switch (Mode)
        {
            case ParticleSystemTrailMode.PerParticle:
                Ratio = ValueInput<float>(nameof(Ratio), 1f);
                Lifetime = ValueInput<float>(nameof(Lifetime), 1f);
                MinimumVertexDistance = ValueInput<float>(nameof(MinimumVertexDistance), .2f);
                SizeAffectsLifetime = ValueInput<bool>(nameof(SizeAffectsLifetime), false);
                Requirement(Ratio, Enter);
                Requirement(Lifetime, Enter);
                Requirement(MinimumVertexDistance, Enter);
                Requirement(SizeAffectsLifetime, Enter);
                break;
            case ParticleSystemTrailMode.Ribbon:
                RibbonCount = ValueInput<int>(nameof(RibbonCount), 1);
                SplitSubEmitterRibbons = ValueInput<bool>(nameof(SplitSubEmitterRibbons), false);
                AttachRibbonsToTransform = ValueInput<bool>(nameof(AttachRibbonsToTransform), false);
                Requirement(RibbonCount, Enter);
                Requirement(SplitSubEmitterRibbons, Enter);
                Requirement(AttachRibbonsToTransform, Enter);
                break;
        }
        ColorOverLifetime = ValueInput<Color>(nameof(ColorOverLifetime), Color.white);
        WidthOverTrail = ValueInput<float>(nameof(WidthOverTrail), 1f);
        ColorOverTrail = ValueInput<Color>(nameof(ColorOverTrail), Color.white);
        Requirement(ColorOverLifetime, Enter);
        Requirement(WidthOverTrail, Enter);
        Requirement(ColorOverTrail, Enter);
    }
    protected override void PerformOperation(Flow flow)
    {
        module = Particle.trails;
        module.mode = Mode;
        module.dieWithParticles = DieWithParticles;
        module.textureMode = TextureMode;
        module.sizeAffectsWidth = SizeAffectsWidth;
        module.inheritParticleColor = InheritParticleColor;
        switch (Mode)
        {
            case ParticleSystemTrailMode.PerParticle:
                module.ratio = flow.GetValue<float>(Ratio);
                module.lifetime = flow.GetValue<float>(Lifetime);
                module.minVertexDistance = flow.GetValue<float>(MinimumVertexDistance);
                module.sizeAffectsLifetime = flow.GetValue<bool>(SizeAffectsLifetime);
                break;
            case ParticleSystemTrailMode.Ribbon:
                module.ribbonCount = flow.GetValue<int>(RibbonCount);
                module.splitSubEmitterRibbons = flow.GetValue<bool>(SplitSubEmitterRibbons);
                module.attachRibbonsToTransform = flow.GetValue<bool>(AttachRibbonsToTransform);
                break;
        }
        module.colorOverLifetime = flow.GetValue<Color>(ColorOverLifetime);
        module.colorOverTrail = flow.GetValue<Color>(ColorOverTrail);
        module.widthOverTrail = flow.GetValue<float>(WidthOverTrail);
    }
}