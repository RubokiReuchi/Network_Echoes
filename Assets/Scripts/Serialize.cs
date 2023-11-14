using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class SerializedThings
{
    public bool Apressed = false;
    public bool Wpressed = false;
    public bool Dpressed = false;
    public bool spacePressed = false;
    public bool shiftPressed = false;
}

public class Serialize : MonoBehaviour
{
    public static Serialize instance;
    static MemoryStream stream;
    bool a = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

 

    public void SerializeJson()
    {

        var t = new SerializedThings();
        t.spacePressed = false;

        if(Input.GetKey(KeyCode.W)) {
        t.spacePressed = true;
        }

        
        string json = JsonUtility.ToJson(t);
        stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(json);
    }

    public void DeserializeJson(ref SerializedThings serializedThings)
    {
        var t = new SerializedThings();
        BinaryReader reader = new BinaryReader(stream);
        stream.Seek(0, SeekOrigin.Begin);

        string json = reader.ReadString();
        Debug.Log(json);
        t = JsonUtility.FromJson<SerializedThings>(json);
        //Debug.Log(t.hp.ToString() + " " + t.pos.ToString());
    }
}
