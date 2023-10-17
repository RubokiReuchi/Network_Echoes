using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingRoom : MonoBehaviour
{
    public TextMeshProUGUI ipDisplay;
    public TextMeshProUGUI passwordDisplay;

    // Start is called before the first frame update
    void Start()
    {
        ipDisplay.text = PlayerPrefs.GetString("LocalIP");
        passwordDisplay.text = PlayerPrefs.GetString("RoomPassword");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
