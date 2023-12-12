using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCollider : MonoBehaviour
{
    RemoteInputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "DieCollider") GetComponent<MeshRenderer>().enabled = false;
        inputs = GameObject.FindGameObjectWithTag("OnlineManager").GetComponent<RemoteInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.death == true)
        {
            inputs.death = false;
            Debug.Log("Player has death");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FadeManager.instance.FadeIn();
            inputs.death = true;
        }
        else if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.GetComponent<Key>().RespawnKey();
        }
    }
}
