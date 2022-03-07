using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public float jumpHeight;
    public float UpSpeed;
    public float DownSpeed;
    private Rigidbody rigidBody;
    private Vector3 processedInput;
    private float processedTurnInput;
    private float processedLookInput;
    public GameObject cameraContainer;
    private Animator animator;
    bool isJump;
    bool isGround;
    public float sensitivity;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        isGround = true;
    }
    private void Update()
    {
        animator.SetFloat("Speed",Mathf.Sqrt(Mathf.Pow( rigidBody.velocity.x,2)+ Mathf.Pow(rigidBody.velocity.z, 2)));
        if (!isGround)
            animator.SetTrigger("Jump");
        else
            animator.gameObject.transform.localPosition = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if(verticalInput>0)
        {
            animator.SetBool("Forward", true);
            animator.SetBool("Back", true);
            animator.SetBool("Right", true);
            animator.SetBool("Left", true);
        }

        else if(verticalInput < 0)
        {
            animator.SetBool("Forward", true);
            animator.SetBool("Back", true);
            animator.SetBool("Right", true);
            animator.SetBool("Left", true);
        }

        else if (horizontalInput > 0)
            animator.SetBool("Right", true);
        else if(horizontalInput<0)
            animator.SetBool("Left", true);
        processedInput = transform.forward * verticalInput + transform.right * horizontalInput;
        if (horizontalInput == 0 && verticalInput == 0)
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
        if(Input.GetKeyDown(KeyCode.Space)&&isGround==true)
        {
            isJump = true;
        }
        var turnInput = Input.GetAxis("Mouse X");
        processedTurnInput = turnInput;
        var lookInput = Input.GetAxis("Mouse Y");
        processedLookInput = lookInput;
       cameraContainer.transform.Rotate(new Vector3(-processedLookInput, 0, 0));

       //Camera.main.transform.Rotate(new Vector3(-processedLookInput, 0, 0));
       // this.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0));
      //  this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
      //  transform.Translate(processedInput * Time.deltaTime*MovementSpeed);
       // Debug.Log($"x: {rigidBody.velocity.x}|y: {rigidBody.velocity.z}");
    }
    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, processedTurnInput*sensitivity,0) * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
      // rigidBody.MovePosition(transform.position + (processedInput * MovementSpeed*Time.fixedDeltaTime));
        //rigidBody.AddForce(processedInput ,ForceMode.Force);
        rigidBody.velocity =new Vector3( processedInput.x * MovementSpeed * Time.fixedDeltaTime, rigidBody.velocity.y,processedInput.z * MovementSpeed * Time.fixedDeltaTime);
        if(isJump==true)
        {
            rigidBody.AddForce(new Vector3(0, jumpHeight, 0));
            isJump = false;
        }    
        if(rigidBody.velocity.y>0)
        {
            rigidBody.AddForce(new Vector3(0,- UpSpeed, 0));
        }
        else if(rigidBody.velocity.y<0)
        {
            rigidBody.AddForce(new Vector3(0, -DownSpeed, 0));
        }
        //Debug.Log($"x: {rigidBody.velocity.x}|y: {rigidBody.velocity.z}");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Terrain")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            isGround = false;
        }
    }
}

