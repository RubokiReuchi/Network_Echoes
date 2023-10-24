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

    static MemoryStream stream;
    bool a = true;

    public class testClass
    {
        public int hp = 12;
        public List<int> pos = new List<int> { 3, 3, 3 };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            
            serializeJson();
            deserializeJson();
            
            a = false;
        }
    }

 

    void serializeJson()
    {
        var t = new testClass();
        t.hp = 40;
        t.pos = new List<int> { 10, 3, 12 };
        string json = JsonUtility.ToJson(t);
        stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(json);
    }

    void deserializeJson()
    {
        var t = new testClass();
        BinaryReader reader = new BinaryReader(stream);
        stream.Seek(0, SeekOrigin.Begin);

        string json = reader.ReadString();
        Debug.Log(json);
        t = JsonUtility.FromJson<testClass>(json);
        Debug.Log(t.hp.ToString() + " " + t.pos.ToString());
    }
}
