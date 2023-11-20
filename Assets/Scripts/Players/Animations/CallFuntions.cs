using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFuntions : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] ProjectileEchoAttack pEchoAttack;
    [SerializeField] DetectorEchoAttack dEchoAttack;
    [SerializeField] RemotePlayerController playerControllerRemote;
    [SerializeField] RemoteProjectileEchoAttack pEchoAttackRemote;
    [SerializeField] RemoteDetectorEchoAttack dEchoAttackRemote;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPlayerFuntionAttack()
    {
        if (pEchoAttack && pEchoAttack.enabled) pEchoAttack.SpawnProjectile();
        else if (pEchoAttackRemote && pEchoAttackRemote.enabled) pEchoAttackRemote.SpawnProjectile();
        if (dEchoAttack && dEchoAttack.enabled) dEchoAttack.CastAttack();
        else if (dEchoAttackRemote && dEchoAttackRemote.enabled) dEchoAttackRemote.CastAttack();
    }

    public void CallPlayerLandingAudio()
    {
        if (playerController && playerController.enabled) playerController.PlayLandingSound();
        else if (playerControllerRemote && playerControllerRemote.enabled) playerControllerRemote.PlayLandingSound();
    }
}
