using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingRoom : MonoBehaviour
{
    int points;
    public TextMeshProUGUI waitingPoints1;
    public TextMeshProUGUI waitingPoints2;

    public TextMeshProUGUI ipDisplay1;
    public TextMeshProUGUI ipDisplay2;
    public TextMeshProUGUI passwordDisplay1;
    public TextMeshProUGUI passwordDisplay2;

    // Start is called before the first frame update
    void Start()
    {
        points = 3;
        StartCoroutine(ManagePoints());

        ipDisplay1.text = "IP: " + PlayerPrefs.GetString("LocalIP");
        ipDisplay2.text = "IP: " + PlayerPrefs.GetString("LocalIP");
        passwordDisplay1.text = "Password: " + PlayerPrefs.GetString("RoomPassword");
        passwordDisplay2.text = "Password: " + PlayerPrefs.GetString("RoomPassword");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // break conection
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator ManagePoints()
    {
        while (true)
        {
            points++;
            if (points > 3) points = 0;
            string pointsAuxText = "";
            for (int i = 0; i < points; i++) pointsAuxText += ".";
            waitingPoints1.text = pointsAuxText;
            waitingPoints2.text = pointsAuxText;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
