using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemoteDetectorEchoAttack : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem EchoParticles;

    public bool echoInput;
    public bool echoReady;

    [NonEditable][SerializeField] bool upgraded;
    [SerializeField] float cooldown = 2.0f;
    [SerializeField] float upgradeCooldown = 1.5f;

    [SerializeField] AudioSource attack_audiosource;
    [SerializeField] AudioClip accord1;
    [SerializeField] AudioClip accord2;
    [SerializeField] AudioClip accord3;

    RemoteInputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        echoReady = true;

        upgraded = false;

        inputs = GameObject.FindGameObjectWithTag("OnlineManager").GetComponent<RemoteInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs == null) return;

        if (ManagePause.instance.paused) return;

        ReadInputs();

        if (echoInput && echoReady)
        {
            Invoke("PlayAccord", 0.3f);

            animator.SetTrigger("Attack");
            echoReady = false;
            if (upgraded) Invoke("EchoReadyAgain", upgradeCooldown);
            else Invoke("EchoReadyAgain", cooldown);
        }

        inputs.ResetEcho();
    }

    void ReadInputs()
    {
        // shot
        echoInput = false;
        if (inputs.shiftPressed) echoInput = true;
    }

    void EchoReadyAgain()
    {
        echoReady = true;
    }

    public void CastAttack()
    {
        EchoParticles.transform.rotation = Quaternion.identity;
        EchoParticles.Play();
    }

    public void Upgrade()
    {
        upgraded = true;
    }

    void PlayAccord()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                attack_audiosource.clip = accord1;
                break;
            case 1:
                attack_audiosource.clip = accord2;
                break;
            case 2:
                attack_audiosource.clip = accord3;
                break;
        }

        attack_audiosource.Play();
    }
    
}
