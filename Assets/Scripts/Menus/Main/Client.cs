using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using UnityEngine.UI;

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

        byte[] recibir_info = new byte[1024];
        string data = "";
        int array_size = 0;

        array_size = listen.Receive(recibir_info, 0, recibir_info.Length, 0);
        Array.Resize(ref recibir_info, array_size);
        data = Encoding.Default.GetString(recibir_info);

        byte[] enviar_info = new byte[1200];
        string sendData;

        if (data == passwordField.text) { sendData = "passwordCorrect"; Debug.Log("Conexión aceptada"); }
        else { sendData = "passwordIncorrect"; Debug.Log("Conexión aceptada"); }

        enviar_info = Encoding.Default.GetBytes(sendData);
        listen.Send(enviar_info);
    }
}
