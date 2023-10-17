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
    //TCP
    Socket listen;
    IPEndPoint connect;

    public InputField ipField;
    public InputField passwordField;

    bool checkingJoin;

    [SerializeField] GameObject fadeIn;

    //UDP
    Socket listenUdp;
    IPEndPoint endPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        checkingJoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTcpClient()
    {
        //if (checkingJoin) return;

        listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        connect = new IPEndPoint(IPAddress.Parse(ipField.text), 6000); //IP y port tienen que ser la misma que el server
        Debug.Log("Ip: " + ipField.text);

        Thread threadTcp = new Thread(ConnectWithServer);
        threadTcp.Start();
    }

    void ConnectWithServer()
    {
        listen.Connect(connect);

        byte[] enviarInfo = new byte[1200];
        string sendData;

        sendData = passwordField.text;
        enviarInfo = Encoding.Default.GetBytes(sendData);
        listen.Send(enviarInfo);

        //StartCoroutine(JoinRoom());
    }

    public void CreateUdpClient()
    {
        
        endPoint = new IPEndPoint(IPAddress.Parse(ipField.text), 6000);

        listenUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Debug.Log("Ip: " + ipField.text);

        //Esto va aqui???
        //listen.Connect(connect);


        byte[] data = new byte[1024];
        string input, stringData;
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        listenUdp.SendTo(data, data.Length, SocketFlags.None, endPoint);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)sender;

        data = new byte[1024];
        int recv = listenUdp.ReceiveFrom(data, ref Remote);

        //Console.WriteLine("Message received from {0}:", Remote.ToString());
        //Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

        StartCoroutine(JoinRoom());

    }


    IEnumerator JoinRoom()
    {
        checkingJoin = true;
        yield return new WaitForSeconds(0.3f);

        if (listen.Connected)
        {
            Debug.Log("Joining Room...");
            fadeIn.SetActive(true);
        }
        else
        {
            Debug.Log("Imposible to join.");
            checkingJoin = false;
        }
    }
}
