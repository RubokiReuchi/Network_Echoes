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

    bool checkingJoin;

    [SerializeField] GameObject fadeIn;

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
        if (checkingJoin) return;

        listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        connect = new IPEndPoint(IPAddress.Parse(ipField.text), 6000); //IP y port tienen que ser la misma que el server
        Debug.Log("Ip: " + ipField.text);

        listen.Connect(connect);

        byte[] enviarInfo = new byte[1024];
        string sendData;

        sendData = passwordField.text;
        enviarInfo = Encoding.Default.GetBytes(sendData);
        listen.Send(enviarInfo);

        StartCoroutine(JoinRoom());
    }

    public void CreateUdpClient()
    {
        
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
