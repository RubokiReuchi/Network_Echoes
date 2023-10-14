using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading;
using UnityEngine.tvOS;
using System;
using System.Text;

public class Server : MonoBehaviour
{
    Socket newSocket;
    IPEndPoint ipep;
    EndPoint client;

    //Tcp
    Socket listen;
    Socket conexion;
    IPEndPoint connect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateTcpServer()
    {
        listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        connect = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6400);

        listen.Bind(connect);
        listen.Listen(10);

        conexion = listen.Accept();
        Console.WriteLine("Conexión aceptada");

        //Thread threadTcp = new Thread(RecieveTcpClient);

        //threadTcp.Start();

        byte[] recibir_info = new byte[1024];
        string data = "";
        int array_size = 0;

        array_size = conexion.Receive(recibir_info, 0, recibir_info.Length, 0);
        Array.Resize(ref recibir_info, array_size);
        data = Encoding.Default.GetString(recibir_info);

        Console.WriteLine("La info recibida es: {0}", data);
        Console.ReadKey();
    }

    void RecieveTcpClient()
    {
        byte[] recibir_info = new byte[1024];
        string data = "";
        int array_size = 0;

        array_size = conexion.Receive(recibir_info, 0, recibir_info.Length, 0);
        Array.Resize(ref recibir_info, array_size);
        data = Encoding.Default.GetString(recibir_info);

        Console.WriteLine("La info recibida es: {0}", data);
        Console.ReadKey();
    }

    public void CreateUdpServer()
    {
        // UDP
        newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipep = new IPEndPoint(IPAddress.Any, 8000);
        newSocket.Bind(ipep);
       
        Console.WriteLine("Waiting for a client...");


        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        client = (EndPoint)(sender);
        //client = (EndPoint)sender;

        Thread thread = new Thread(RecieveUdpClient);

        thread.Start();
    }

    void RecieveUdpClient()
    {
        while (true)
        {
            byte[] data = new byte[2048];
            int recv = newSocket.ReceiveFrom(data, ref client);
            Console.WriteLine("Message received from {0}:");
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string welcome = "Welcome to my Patata server";
            data = Encoding.ASCII.GetBytes(welcome);
            newSocket.SendTo(data, data.Length, SocketFlags.None, client);

        }
    }
}
