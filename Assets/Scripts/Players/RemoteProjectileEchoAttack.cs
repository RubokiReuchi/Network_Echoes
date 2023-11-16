using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemoteProjectileEchoAttack : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Transform spawn;
    [SerializeField] GameObject echoPrefab;
    PlayerController playerController;

    bool echoInput;
    public bool echoReady;
    bool aimingUp;

    Vector3 direction;

    [NonEditable][SerializeField] bool upgraded;
    [SerializeField] float cooldown = 2.0f;
    [SerializeField] float upgradeCooldown = 1.5f;

    [SerializeField] AudioSource attack_audioSource;
    
    [SerializeField] AudioClip scream2;

    SerializedThings inputs;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
       
        echoReady = true;

        upgraded = false;

        inputs = OnlineManager.instance.remoteImputs;
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagePause.instance.paused) return;

        ReadInputs();

        if (echoInput && echoReady)
        {
            Invoke("PlayScream", 0.2f);

            if (aimingUp)
            {
                direction = new Vector3(0, 0, 90);
                animator.SetTrigger("Attack_Up");
            }
            else
            {
                if (playerController.rotationDirection == -1) direction = new Vector3(0, 180, 0);
                else if (playerController.rotationDirection == 1) direction = new Vector3(0, 0, 0);
                else if (playerController.lookingRight) direction = new Vector3(0, 0, 0);
                else direction = new Vector3(0, 180, 0);
                animator.SetTrigger("Attack_Front");
            }
            echoReady = false;
            if (upgraded) Invoke("EchoReadyAgain", upgradeCooldown);
            else Invoke("EchoReadyAgain", cooldown);
        }

        inputs.Reset();
    }

    void ReadInputs()
    {
        // shot
        echoInput = false;
        if (inputs.shiftPressed) echoInput = true;

        // aim up
        aimingUp = false;
        if (inputs.Wpressed) aimingUp = true;
    }

    void EchoReadyAgain()
    {
        echoReady = true;
    }

    public void SpawnProjectile()
    {
        Instantiate(echoPrefab, spawn.position, Quaternion.Euler(direction));
    }

    public void Upgrade()
    {
        upgraded = true;
    }

    void PlayScream()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                attack_audioSource.clip = scream2;
            break;
            case 1:
                attack_audioSource.clip = scream2;
            break;
        }
        attack_audioSource.Play();
    }
}
