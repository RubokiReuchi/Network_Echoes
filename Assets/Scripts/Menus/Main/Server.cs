using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading;
using UnityEngine.tvOS;

public class Server : MonoBehaviour
{
    Socket newSocket;
    IPEndPoint ipep;
    EndPoint client;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateServer()
    {
        // UDP
        newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipep = new IPEndPoint(IPAddress.Any, 8000);
        newSocket.Bind(ipep);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        client = (EndPoint)sender;

        Thread thread = new Thread(RecieveClient);

        thread.Start();
    }

    void RecieveClient()
    {
        while (true)
        {
            byte[] data = new byte[2048];
            int recv = newSocket.ReceiveFrom(data, ref client);
            newSocket.SendTo(data, recv, SocketFlags.None, client);
        }
    }
}
