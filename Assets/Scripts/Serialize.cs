using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


public class Serialize : MonoBehaviour
{
    public static Serialize instance;
    static MemoryStream stream;
    bool a = true;

    byte[] receiveInfo;

    RemoteInputs t = new RemoteInputs();

    public Transform controlledCharacter;

    private void Awake()
    {
        instance = this;
        stream = new MemoryStream();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        t.Dpressed = false;
        t.Wpressed = false;
        t.Apressed = false;
        t.spacePressed = false;
        t.shiftPressed = false;

        if (Input.GetKey(KeyCode.D))
        {
            t.Dpressed = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            t.Wpressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            t.Apressed = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            t.spacePressed = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            t.shiftPressed = true;
        }

        t.positionX = controlledCharacter.position.x;
        t.positionY = controlledCharacter.position.y;
        t.positionZ = controlledCharacter.position.z;
    }



    public MemoryStream SerializeJson()
    {
        string json = JsonUtility.ToJson(t);
        stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);

        writer.Write(json);

        return stream;
    }

    public void DeserializeJson(byte[] sendInfo, ref RemoteInputs serializedThings)
    {
        
        MemoryStream stream = new MemoryStream(sendInfo);
        BinaryReader reader = new BinaryReader(stream);
        stream.Seek(0, SeekOrigin.Begin);
        string json = reader.ReadString();
        //Debug.Log(json);
        JsonUtility.FromJsonOverwrite(json, serializedThings);

    }
}
