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

    Socket newSocket;
    IPEndPoint ipep;
    EndPoint client;

    //Tcp
    Socket listen;
    Socket conexion;
    IPEndPoint connect;


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

    public void CreateTcpServer()
    {
        password = passwordField.text;

        listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        connect = new IPEndPoint(IPAddress.Parse(ip), 6000);
        Debug.Log("Ip: " + ip);

        listen.Bind(connect);
        listen.Listen(1);

        Thread threadTcp = new Thread(RecieveTcpClient);
        threadTcp.Start();

        PlayerPrefs.SetString("LocalIP", ip);
        PlayerPrefs.SetString("RoomPassword", password);
        fadeWaitingRoom.SetActive(true);
    }

    void RecieveTcpClient()
    {
        while (canJoin)
        {
            conexion = listen.Accept();
            
            byte[] recibirInfo = new byte[1024];
            string data = "";
            int arraySize = 0;

            arraySize = conexion.Receive(recibirInfo, 0, recibirInfo.Length, 0);
            Array.Resize(ref recibirInfo, arraySize);
            data = Encoding.Default.GetString(recibirInfo);

            if (data != password)
            {
                conexion.Shutdown(SocketShutdown.Both);
                Debug.Log("Conexión rechazada");
            }
            else
            {
                Debug.Log("Conexión aceptada");
                canJoin = false;
            }
        }
    }

    public void CreateUdpServer()
    {
        // UDP
        newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipep = new IPEndPoint(IPAddress.Any, 8000);
        newSocket.Bind(ipep);
        

        Debug.Log("Waiting for a client...");

        Thread thread = new Thread(RecieveUdpClient);

        thread.Start();
    }

    void RecieveUdpClient()
    {
        while (true)
        {

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            client = (EndPoint)(sender);
            //client = (EndPoint)sender;

            byte[] data = new byte[2048];
            int recv = newSocket.ReceiveFrom(data, ref client);
            Debug.Log("Message received from {0}:");
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));


            string welcome = "Welcome to my Patata server";
           
            data = Encoding.ASCII.GetBytes(welcome);
            newSocket.SendTo(data, data.Length, SocketFlags.None, client);
        }
    }

    public string GetLocalIP()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }
}
