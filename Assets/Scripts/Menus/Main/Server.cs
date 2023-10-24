using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading;
using UnityEngine.tvOS;
using System;
using System.Text;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
    string ip;
    public InputField passwordField;
    string password;
    bool canJoin;

    // UDP
    Socket listen;
    IPEndPoint connect;
    EndPoint client;

    //Tcp
    //Socket listenTCP;
    //Socket conexionTCP;
    //IPEndPoint connectTCP;


    [SerializeField] GameObject fadeWaitingRoom;

    // Start is called before the first frame update
    void Start()
    {
        ip = GetLocalIP();
        canJoin = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void CreateTcpServer()
    //{
    //    password = passwordField.text;

    //    listenTCP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //    connectTCP = new IPEndPoint(IPAddress.Parse(ip), 6000);
    //    Debug.Log("Ip: " + ip);

    //    listenTCP.Bind(connectTCP);
    //    listenTCP.Listen(1);

    //    Thread threadTcp = new Thread(RecieveTcpClient);
    //    threadTcp.Start();

    //    PlayerPrefs.SetString("LocalIP", ip);
    //    PlayerPrefs.SetString("RoomPassword", password);
    //    fadeWaitingRoom.SetActive(true);
    //}

    //void RecieveTcpClient()
    //{
    //    while (canJoin)
    //    {
    //        conexionTCP = listenTCP.Accept();
            
    //        byte[] recibirInfo = new byte[1024];
    //        string data = "";
    //        int arraySize = 0;

    //        arraySize = conexionTCP.Receive(recibirInfo, 0, recibirInfo.Length, 0);
    //        Array.Resize(ref recibirInfo, arraySize);
    //        data = Encoding.Default.GetString(recibirInfo);

    //        if (data != password)
    //        {
    //            conexionTCP.Shutdown(SocketShutdown.Both);
    //            Debug.Log("Conexión rechazada");
    //        }
    //        else
    //        {
    //            Debug.Log("Conexión aceptada");
    //            canJoin = false;
    //        }
    //    }
    //}

    public void CreateUdpServer()
    {
        // UDP
        password = passwordField.text;

        listen = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        connect = new IPEndPoint(IPAddress.Any, 8000);

        listen.Bind(connect);

        Thread thread = new Thread(RecieveUdpClient);
        thread.Start();

        PlayerPrefs.SetString("LocalIP", ip);
        PlayerPrefs.SetString("RoomPassword", password);
        fadeWaitingRoom.SetActive(true);
    }

    void RecieveUdpClient()
    {
        while (canJoin)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            client = (EndPoint)(endPoint);

            byte[] info = new byte[2048];
            string data = "";
            int recv = listen.ReceiveFrom(info, ref client);
            data = Encoding.ASCII.GetString(info, 0, recv);

            if (data != password)
            {
                Debug.Log("Conexión rechazada");
                data = "Wrong Password";
                info = Encoding.ASCII.GetBytes(data);
                listen.SendTo(info, info.Length, SocketFlags.None, client);
            }
            else
            {
                Debug.Log("Conexión aceptada");
                canJoin = false;
            }
        }
    }

    public string GetLocalIP()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }
}
