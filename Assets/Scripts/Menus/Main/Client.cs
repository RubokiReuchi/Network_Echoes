using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class Client : MonoBehaviour
{
    Socket listen;
    IPEndPoint connect;

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
        connect = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6400); //IP y port tienen que ser la misma que el server

        listen.Connect(connect);

        byte[] enviar_info = new byte[1200];
        string data;
        Console.WriteLine("Ingrese la info a enviar");
        data = "patata";

        enviar_info = Encoding.Default.GetBytes(data);
        listen.Send(enviar_info);
        Console.ReadKey();
    }
}
