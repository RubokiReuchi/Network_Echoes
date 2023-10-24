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
    IPEndPoint endPoint;

    public InputField ipField;
    public InputField passwordField;

    //Socket listenTCP;
    //IPEndPoint connectTCP;

    //public InputField ipFieldTCP;
    //public InputField passwordFieldTCP;

    bool checkingJoin;

    //[SerializeField] GameObject fadeIn;


    // Start is called before the first frame update
    void Start()
    {
        checkingJoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void CreateTcpClient()
    //{
    //    if (checkingJoin) return;

    //    listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //    connect = new IPEndPoint(IPAddress.Parse(ipField.text), 6000); //IP y port tienen que ser la misma que el server
    //    Debug.Log("Ip: " + ipField.text);

    //    Thread threadTcp = new Thread(ConnectWithServer);
    //    threadTcp.Start();
    //}

    //void ConnectWithServer()
    //{
    //    listen.Connect(connect);

    //    byte[] enviarInfo = new byte[1200];
    //    string sendData;

    //    sendData = passwordField.text;
    //    enviarInfo = Encoding.Default.GetBytes(sendData);
    //    listen.Send(enviarInfo);


    //    Debug.Log("Joining Room...");
    //    StartCoroutine(JoinRoom());
    //}

    public void CreateUdpClient()
    {
        if (checkingJoin) return;

        listen = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        endPoint = new IPEndPoint(IPAddress.Parse(ipField.text), 8000);
        Debug.Log("Ip: " + ipField.text);

        Thread threadUdp = new Thread(ConnectWithUdpServer);
        threadUdp.Start();
    }
    void ConnectWithUdpServer()
    {
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)sender;

        byte[] size = new byte[1024];
        string data = passwordField.text;
        size = Encoding.ASCII.GetBytes(data);
        listen.SendTo(size, size.Length, SocketFlags.None, endPoint);
    }

    IEnumerator JoinRoom()
    {
        checkingJoin = true;
        yield return new WaitForSeconds(0.3f);

        if (listen.Connected)
        {
            Debug.Log("Joining Room...");
            //fadeIn.SetActive(true);
        }
        else
        {
            Debug.Log("Imposible to join.");
            checkingJoin = false;
        }
    }
}
