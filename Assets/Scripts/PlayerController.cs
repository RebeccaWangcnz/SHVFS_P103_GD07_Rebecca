using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject runFX;
    private GameObject runFX_instance;
    public float MovementSpeed;
    public float SpeedUpSpeed;
    private float currentSpeed;
    private float speedUpTime = 10f;
    private float timer;
    private bool isSpeedUp=false;

    public float jumpHeight;
    public float UpSpeed;
    public float DownSpeed;
    public GameObject hand;
    public float force;

    private Rigidbody rigidBody;
    private Vector3 processedInput;
    private float processedTurnInput;
    private GameObject audio;
   // private float processedLookInput;
   // public GameObject cameraContainer;
    private Animator animator;
    bool isJump;
    bool isGround;
   // public float sensitivity;
    private float horizontalInput;
    private float verticalInput;

    private GameObject foodCanPick;
    [HideInInspector]
    public bool isHoldingFood;

    [Header("Team")]
    public int teamID;//the ID of the team
    public Zone zone;
    [Header("Input")]
    public string xInput;
    public string yInput;
    public KeyCode interact;
    private void Awake()
    {
        audio = FindObjectOfType<AudioSource>().gameObject;
        currentSpeed = MovementSpeed;
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        isGround = true;
    }
    private void Update()
    {
        setIsHoldingFood();
        animator.SetFloat("Speed", rigidBody.velocity.magnitude);
        //if (!isGround)
        //    animator.SetTrigger("Jump");
        //else
        //    animator.gameObject.transform.localPosition = Vector3.zero;
       // Cursor.lockState = CursorLockMode.Locked;

        //input
        horizontalInput = Input.GetAxis(xInput);
        verticalInput = Input.GetAxis(yInput);
        processedInput = transform.forward * verticalInput + transform.right * horizontalInput;
        if (horizontalInput == 0 && verticalInput == 0)
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);

        //interact
        if(Input.GetKeyDown(interact))
        {
            PickOrDropFood();
        }
        //time for speed up
        if(isSpeedUp)
        {
            timer += Time.deltaTime;
            if(timer>speedUpTime)
            {
                timer = 0;
                currentSpeed = MovementSpeed;
                if(runFX_instance)
                        Destroy(runFX_instance);
                isSpeedUp = false;
            }
        }
    }
    private void FixedUpdate()
    {
        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, processedTurnInput*sensitivity,0) * Time.fixedDeltaTime);
        //rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
        //move
        rigidBody.velocity =new Vector3( processedInput.x * currentSpeed * Time.fixedDeltaTime, rigidBody.velocity.y, processedInput.z * currentSpeed* Time.fixedDeltaTime);
        // rotate
            if (horizontalInput > 0&&verticalInput==0)
                transform.GetChild(0).localEulerAngles = new Vector3(0, 90, 0);
            else if(horizontalInput > 0 && verticalInput > 0)
                transform.GetChild(0).localEulerAngles = new Vector3(0, 45, 0);
            else if(horizontalInput > 0 && verticalInput < 0)
                transform.GetChild(0).localEulerAngles = new Vector3(0, 135, 0);
            else if(horizontalInput==0&& verticalInput > 0)
                 transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
             else if (horizontalInput == 0 && verticalInput < 0)
                 transform.GetChild(0).localEulerAngles = new Vector3(0, 180, 0);
            else if(horizontalInput < 0 && verticalInput < 0)
                 transform.GetChild(0).localEulerAngles = new Vector3(0, 225, 0);
        else if (horizontalInput < 0 && verticalInput == 0)
            transform.GetChild(0).localEulerAngles = new Vector3(0, 270, 0);
        else if (horizontalInput < 0 && verticalInput > 0)
            transform.GetChild(0).localEulerAngles = new Vector3(0, 315, 0);

        if (isJump==true)
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

    //Pick food or Drop food
    public void PickOrDropFood()
    {
        if(!isHoldingFood&& foodCanPick)
        {
            audio.GetComponents<AudioSource>()[1].Play();
            //isHoldingFood = true;
            foodCanPick.transform.SetParent(hand.transform);
            foodCanPick.transform.localPosition = Vector3.zero;
            foodCanPick.GetComponent<FoodLogic>().isHold = true;
            foodCanPick.GetComponent<Rigidbody>().isKinematic = true;
            var colliders = foodCanPick.GetComponentsInChildren<SphereCollider>();
            colliders[1].enabled = false;
            //for(int i=0;i<colliders.Length;i++)
            //{
            //    colliders[i].enabled = false;
            //}
        }
        else if(isHoldingFood)
        {
            audio.GetComponents<AudioSource>()[2].Play();
            //isHoldingFood = false;
            var food = GetComponentInChildren<FoodLogic>();
            food.isHold = false;
            food.transform.SetParent(null);
            food.GetComponent<Rigidbody>().isKinematic = false;
            food.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward *force);
            var colliders = food.GetComponentsInChildren<SphereCollider>();
            colliders[1].enabled = true;
            //for (int i = 0; i < colliders.Length; i++)
            //{
            //    colliders[i].enabled = true;
            //}
        }
    }
    //set bool isHoldingFood
    public void setIsHoldingFood()
    {
       if( GetComponentInChildren<FoodLogic>())
        {
            isHoldingFood = true;
        }
       else
        {
            isHoldingFood = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FoodLogic>()/*&& other.GetComponent<FoodLogic>().isHold==false*/)
        {
            foodCanPick = other.gameObject;
        }
        if(other.GetComponent<SpeedUpProp>()&&!isSpeedUp)
        {
            Destroy(other.gameObject);
            currentSpeed = SpeedUpSpeed;
            runFX_instance= Instantiate(runFX, this.transform);
            isSpeedUp = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FoodLogic>() )
        {
            foodCanPick = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
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

