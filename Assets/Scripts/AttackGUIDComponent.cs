using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackGUIDComponent : MonoBehaviour
{
    public Guid ID;
    public void Awake()
    {
        ID = Guid.NewGuid();
       // Debug.Log(ID);
    }
  
}
