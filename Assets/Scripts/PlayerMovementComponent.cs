using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    private Rigidbody rigidBody;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var processedInput = new Vector3(horizontalInput, 0f, verticalInput);

        Debug.Log($"Horizontal Input:{horizontalInput}|Vertical Input:{verticalInput}");
    }
}
