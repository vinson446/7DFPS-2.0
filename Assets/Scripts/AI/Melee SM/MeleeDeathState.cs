﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDeathState : MeleeState
{
    [SerializeField] int exp;
    public int Exp { get => exp; set => exp = value; }

    public float yOffset;

    public int chanceToDropPickup;
    public GameObject[] pickupObjs;

    AudioManager audioManager;
    public GameObject clip;

    public override void EnterState()
    {
        stateMachine.MEnemy.ChangeState("Range Death State");

        stateMachine.MEnemy.OnDeathAnimation();
        stateMachine.PlayerTrans.GetComponent<Player>().GainExp(Exp);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.IncreaseScore(Exp);
        gameManager.UpdateEnemyKilled();

        EnemyUI enemyUI = stateMachine.gameObject.transform.parent.GetComponentInChildren<EnemyUI>();
        enemyUI.TurnOnOffUI(false);

        int dropPickupChance = UnityEngine.Random.Range(0, 101);
        if (dropPickupChance <= chanceToDropPickup)
        {
            DropPickup();
        }

        stateMachine.MEnemy.enabled = false;
        stateMachine.NavAgent.enabled = false;
        Collider coll = gameObject.GetComponentInParent<Collider>();
        coll.enabled = false;
        // Rigidbody rb = gameObject.transform.parent.gameObject.AddComponent<Rigidbody>();
        // rb.velocity = Vector3.zero;

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetAudioObj(clip);
        audioManager.PlayOneShotRandomPitch(11);

        Destroy(gameObject.transform.parent.gameObject, 5);
    }

    void DropPickup()
    {
        int pickup = UnityEngine.Random.Range(0, 2);

        if (pickup == 0)
        {
            Instantiate(pickupObjs[0], transform.parent.position + new Vector3(0, yOffset, 0), Quaternion.identity);
        }
        else if (pickup == 1)
        {
            Instantiate(pickupObjs[1], transform.parent.position + new Vector3(0, yOffset, 0), Quaternion.identity);
        }
    }
}
