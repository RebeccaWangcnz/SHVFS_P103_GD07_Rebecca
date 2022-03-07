using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackerComponent : MonoBehaviour
{
    //we want this to be self-contained
    //Globally unique identifier
    public AttackGUIDComponent AttackGUIDComponent;
    //public Guid ID;
    public GameObject Attacker;
    public float AttackPower;
    public float AttackActiveTime;
    private float attackActiveTimer;
    private Guid guid;

    private void OnEnable()
    {
        
        Attacker.SetActive(false);
        
    }

    private void Update()
    {
        if(attackActiveTimer<0f)
        {
            attackActiveTimer = 0f;
        }

            attackActiveTimer -= Time.deltaTime;
            Attacker.transform.localScale = Vector3.one * attackActiveTimer / AttackActiveTime;
            attackActiveTimer -= Time.deltaTime;
            Attacker.transform.localScale = Vector3.one * attackActiveTimer / AttackActiveTime;


        if (attackActiveTimer > 0f)
        {
            Attacker.SetActive(true);
            return;
        }

        Attacker.SetActive(false);

        if (!Input.GetMouseButtonDown(0)) return;//rightclick
        
        attackActiveTimer = AttackActiveTime;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent <AttackableComponent> ()) return;

        Debug.Log($"hit{other.name} | other guid:{other.GetComponent<AttackableComponent>().GUID} |my guid:{guid}");

        if (other.GetComponent<AttackableComponent>().GUID.Equals(guid)) return;

        Debug.Log($"attackacle dude { other.name}");

        other.GetComponent<Rigidbody>().AddForce((-transform.forward +transform.up) * AttackPower, ForceMode.Impulse);
    }
}
