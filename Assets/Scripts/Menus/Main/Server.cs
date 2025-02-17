using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading;
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

    bool goToGame = false;

    bool exitGameLoop = false;

    //Tcp
    //Socket listenTCP;
    //Socket conexionTCP;
    //IPEndPoint connectTCP;


    [SerializeField] GameObject fadeWaitingRoom;

    RemoteInputs reInputs;
    bool asignInputClass;


    // Start is called before the first frame update
    void Start()
    {
        ip = GetLocalIP();
        canJoin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (goToGame)
        {
            goToGame = false;
            SceneManager.LoadScene(3);
        }
        else if (asignInputClass)
        {
            asignInputClass = false;
            reInputs = GameObject.FindGameObjectWithTag("OnlineManager").GetComponent<RemoteInputs>();
        }
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
    //            Debug.Log("Conexi�n rechazada");
    //        }
    //        else
    //        {
    //            Debug.Log("Conexi�n aceptada");
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
        DontDestroyOnLoad(gameObject);
        fadeWaitingRoom.SetActive(true);
    }

    void RecieveUdpClient()
    {
        while (canJoin)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            client = (EndPoint)(endPoint);

            byte[] reciveInfo = new byte[2048];
            string data = "";
            int recv = listen.ReceiveFrom(reciveInfo, ref client);
            data = Encoding.ASCII.GetString(reciveInfo, 0, recv);

            if (data != password)
            {
                Debug.Log("Conexi�n rechazada");
                data = "Wrong Password";
            }
            else
            {
                Debug.Log("Conexi�n aceptada");
                data = "Correct Password";
                canJoin = false;
            }
            byte[] sendInfo = new byte[1024];
            sendInfo = Encoding.ASCII.GetBytes(data);
            listen.SendTo(sendInfo, sendInfo.Length, SocketFlags.None, client);

            // waitingRoom
            bool exit = false;
            while (!exit)
            {
                recv = listen.ReceiveFrom(reciveInfo, ref client);
                data = Encoding.ASCII.GetString(reciveInfo, 0, recv);
                if (data == "OnWaitingRoom")
                {
                    sendInfo = Encoding.ASCII.GetBytes(data);
                    listen.SendTo(sendInfo, sendInfo.Length, SocketFlags.None, client);
                    goToGame = true;
                    exit = true;
                }
            }

            // in game
            Thread threadSend = new Thread(SendInfo);
            threadSend.Start();
            Thread threadRecieve = new Thread(RecieveInfo);
            threadRecieve.Start();
        }
    }

    void SendInfo()
    {
        while (!exitGameLoop)
        {
            if (OnlineManager.instance == null || Serialize.instance == null) continue;
            //Debug.Log("Server Send");
            byte[] sendInfo = new byte[1024];
            sendInfo = Serialize.instance.SerializeJson().GetBuffer();
            listen.SendTo(sendInfo , sendInfo.Length, SocketFlags.None, client);
        }

    }

    void RecieveInfo()
    {
        while (!exitGameLoop)
        {
            if (OnlineManager.instance == null || Serialize.instance == null) continue;
            if (!reInputs)
            {
                asignInputClass = true;
                continue;
            }
            //Debug.Log("Server Recieve");
            byte[] receiveInfo = new byte[1024];
            listen.ReceiveFrom(receiveInfo, ref client);
            Serialize.instance.DeserializeJson(receiveInfo, ref reInputs);
        }
    }

    public string GetLocalIP()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }


    private void OnApplicationQuit()
    {
        exitGameLoop = true;
    }
}
