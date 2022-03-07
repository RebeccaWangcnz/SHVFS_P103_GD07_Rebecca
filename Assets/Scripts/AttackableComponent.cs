using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackableComponent : MonoBehaviour
{
    public AttackGUIDComponent AttackGUIDComponent;
    public Guid GUID;

    // Start is called before the first frame update
    private void Start()
    {
        GUID = AttackGUIDComponent.ID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
