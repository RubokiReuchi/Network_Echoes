using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using UnityEngine.UI;
using System.Linq;

public class Client : MonoBehaviour
{
    Socket listen;
    IPEndPoint connect;

    public InputField ipField;
    public InputField passwordField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTcpClient()
    {
        listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        connect = new IPEndPoint(IPAddress.Parse(ipField.text), 6000); //IP y port tienen que ser la misma que el server
        Debug.Log("Ip: " + ipField.text);

        listen.Connect(connect);

        byte[] enviar_info = new byte[1200];
        string sendData;

        sendData = /*passwordField.text*/Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
        enviar_info = Encoding.Default.GetBytes(sendData);
        listen.Send(enviar_info);
    }
}
