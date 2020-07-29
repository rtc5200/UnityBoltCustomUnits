using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Threading;
using Bolt;
using Ludiq;
using System;

public class UdpReciever : MonoBehaviour
{
    private UdpClient client;
    private const int port = 10000;
    private CancellationTokenSource token = new CancellationTokenSource();
    /// <summary>
    /// 加速度XYZ
    /// </summary>
    public static float[] Acceleration { get; private set; } = new float[3];
    /// <summary>
    /// 角速度XYZ
    /// </summary>
    public static float[] AngleVelocity { get; private set; } = new float[3];
    /// <summary>
    /// 地磁気XYZ
    /// </summary>
    public static float[] Magnetic { get; private set; } = new float[3];
    /// <summary>
    /// クォータニオンWXYZ
    /// </summary>
    public static float[] Quaternion { get; private set; } = new float[4];
    void Awake()
    {
        client = new UdpClient(port);
        //client.Client.Blocking = false;
        //client.Client.ReceiveTimeout = 300;
        Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                    var data = client.Receive(ref remote);
                    var array = Encoding.UTF8.GetString(data).Split(',').Where((e, index) => index < 14).Select(e => float.Parse(e));
                    Acceleration = array.Where((e, index) => index > 0 && index < 4).ToArray();
                    AngleVelocity = array.Where((e, index) => index > 3 && index < 7).ToArray();
                    Magnetic = array.Where((e, index) => index > 6 && index < 10).ToArray();
                    Quaternion = array.Where((e, index) => index > 9 && index < 14).ToArray();
                    
                }
                catch (SocketException e)
                {
                    Debug.LogError("UDP受信失敗");
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        });
    }
    void OnApplicationQuit() => token.Cancel();
}
/// <summary>
/// 加速度センサーの値を出力する
/// </summary>
[UnitCategory("センサー")]
[TypeIcon(typeof(float))]
[UnitTitle("加速度センサー")]
public class AccSensorValue : Unit
{
    /// <summary>
    /// 加速度X
    /// </summary>
    [DoNotSerialize,PortLabel("加速度X")]
    public ValueOutput Accx { get; private set; }
    /// <summary>
    /// 加速度Y
    /// </summary>
    [DoNotSerialize, PortLabel("加速度Y")]
    public ValueOutput Accy { get; private set; }
    /// <summary>
    /// 加速度Z
    /// </summary>
    [DoNotSerialize, PortLabel("加速度Z")]
    public ValueOutput Accz { get; private set; }
    public override bool canDefine => true;
    protected override void Definition()
    {
        Accx = ValueOutput<float>(nameof(Accx), new Func<Flow, float>((flow) => UdpReciever.Acceleration[0])).Predictable();
        Accy = ValueOutput<float>(nameof(Accy), new Func<Flow, float>((flow) => UdpReciever.Acceleration[1])).Predictable();
        Accz = ValueOutput<float>(nameof(Accz), new Func<Flow, float>((flow) => UdpReciever.Acceleration[2])).Predictable();
    }
}
/// <summary>
/// 角速度センサーの値を出力する
/// </summary>
[UnitCategory("センサー")]
[TypeIcon(typeof(float))]
[UnitTitle("角速度センサー")]
public class AngVSensorValue : Unit
{
    [DoNotSerialize,PortLabel("角速度X")]
    public ValueOutput Angvx { get; private set; }
    [DoNotSerialize, PortLabel("角速度Y")]
    public ValueOutput Angvy { get; private set; }
    [DoNotSerialize, PortLabel("角速度Z")]
    public ValueOutput Angvz { get; private set; }
    public override bool canDefine => true;
    protected override void Definition()
    {
        Angvx = ValueOutput<float>(nameof(Angvx), new Func<Flow, float>((flow) => UdpReciever.AngleVelocity[0])).Predictable();
        Angvy = ValueOutput<float>(nameof(Angvy), new Func<Flow, float>((flow) => UdpReciever.AngleVelocity[1])).Predictable();
        Angvz = ValueOutput<float>(nameof(Angvz), new Func<Flow, float>((flow) => UdpReciever.AngleVelocity[2])).Predictable();
    }
}
/// <summary>
/// 地磁気センサーの値を出力する
/// </summary>
[UnitCategory("センサー")]
[TypeIcon(typeof(float))]
[UnitTitle("地磁気センサー")]
public class MagSensorValue : Unit
{
    [DoNotSerialize, PortLabel("地磁気X")]
    public ValueOutput Magx { get; private set; }
    [DoNotSerialize, PortLabel("地磁気Y")]
    public ValueOutput Magy { get; private set; }
    [DoNotSerialize, PortLabel("地磁気Z")]
    public ValueOutput Magz { get; private set; }
    public override bool canDefine => true;
    protected override void Definition()
    {
        Magx = ValueOutput<float>(nameof(Magx), new Func<Flow, float>((flow) => UdpReciever.Magnetic[0])).Predictable();
        Magy = ValueOutput<float>(nameof(Magy), new Func<Flow, float>((flow) => UdpReciever.Magnetic[1])).Predictable();
        Magz = ValueOutput<float>(nameof(Magz), new Func<Flow, float>((flow) => UdpReciever.Magnetic[2])).Predictable();
    }
}
/// <summary>
/// クォータニオンの値を出力する
/// </summary>
[UnitCategory("センサー")]
[TypeIcon(typeof(float))]
[UnitTitle("クォータニオン")]
public class QSensorValue : Unit
{
    [DoNotSerialize, PortLabel("W")]
    public ValueOutput W { get; private set; }
    [DoNotSerialize, PortLabel("X")]
    public ValueOutput X { get; private set; }
    [DoNotSerialize, PortLabel("Y")]
    public ValueOutput Y { get; private set; }
    [DoNotSerialize, PortLabel("Z")]
    public ValueOutput Z { get; private set; }
    public override bool canDefine => true;
    protected override void Definition()
    {
        W = ValueOutput<float>(nameof(W), new Func<Flow, float>((flow) => UdpReciever.Quaternion[0])).Predictable();
        X = ValueOutput<float>(nameof(X), new Func<Flow, float>((flow) => UdpReciever.Quaternion[1])).Predictable();
        Y = ValueOutput<float>(nameof(Y), new Func<Flow, float>((flow) => UdpReciever.Quaternion[2])).Predictable();
        Z = ValueOutput<float>(nameof(Z), new Func<Flow, float>((flow) => UdpReciever.Quaternion[3])).Predictable();
    }
}

