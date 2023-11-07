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
using UnityEditor.PackageManager;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject fadeWaitingRoom;
    bool goToWaitingRoom = false;
    int activeSceneIndex = 0;
    bool goToGame = false;
    float waitSecs = 0;

    //[SerializeField] GameObject fadeIn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goToWaitingRoom)
        {
            goToWaitingRoom = false;
            DontDestroyOnLoad(gameObject);
            fadeWaitingRoom.SetActive(true);
        }
        else if (goToGame)
        {
            goToGame = false;
            SceneManager.LoadScene(3);
        }

        if (waitSecs > 0)
        {
            waitSecs -= Time.deltaTime;
        }

        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
        listen = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        endPoint = new IPEndPoint(IPAddress.Parse(ipField.text), 8000);
        Debug.Log("Ip: " + ipField.text);

        Thread threadUdp = new Thread(ConnectWithUdpServer);
        threadUdp.Start();

    }
    void ConnectWithUdpServer()
    {
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint server = (EndPoint)sender;

        byte[] sendInfo = new byte[2048];
        string data = passwordField.text;
        sendInfo = Encoding.ASCII.GetBytes(data);
        listen.SendTo(sendInfo, sendInfo.Length, SocketFlags.None, endPoint);

        bool exit = false;
        byte[] recieveInfo = new byte[1024];
        int recv;
        while (!exit)
        {
            recv = listen.ReceiveFrom(recieveInfo, ref server);
            data = Encoding.ASCII.GetString(recieveInfo, 0, recv);

            if (data == "Wrong Password") return;
            else if (data == "Correct Password") exit = true;
            else Debug.LogError("Logic Error");
        }
        goToWaitingRoom = true;

        // waitingRoom
        while (activeSceneIndex != 4) { }
        waitSecs = 3.0f;
        while (waitSecs > 0) { }
        data = "OnWaitingRoom";
        sendInfo = Encoding.ASCII.GetBytes(data);
        listen.SendTo(sendInfo, sendInfo.Length, SocketFlags.None, endPoint);
        exit = false;
        while (!exit)
        {
            recv = listen.ReceiveFrom(recieveInfo, ref server);
            data = Encoding.ASCII.GetString(recieveInfo, 0, recv);
            if (data == "OnWaitingRoom") exit = true;
        }
        goToGame = true;
    }

    //IEnumerator JoinRoom()
    //{
    //    checkingJoin = true;
    //    yield return new WaitForSeconds(0.3f);

    //    if (listen.Connected)
    //    {
    //        Debug.Log("Joining Room...");
    //        //fadeIn.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.Log("Imposible to join.");
    //        checkingJoin = false;
    //    }
    //}
}
