using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Wood;
    public GameObject Cloths;
    public GameObject Rope;
    public GameObject Food;

    public GameObject Water;
    public GameObject Player;
    public ParticleSystem Particles;
    
    public Text invText;


    public float boostedSpeed = 20f;
    public bool SpeedBoost = false;
    public float maxSpeed = 10f;
    public float movementSpeed = 10f;
    public float currentSpeed = 0;
    public float speedSmoothVelocity = 0f;
    public float speedSmoothTime = 0.1f;
    public float gravity = 7f;
    public float rotationSpeed = 1f;

    public float woodInventory = 0;
    public float clothInventory = 0;
    public float ropeInventory = 0;
    public float foodInventory = 0;
    public float totalInventory = 0;
    public float maxInventory = 20;

    private bool onPlatform = false;

    private Transform mainCameraTransform = null;
    private CharacterController controller = null;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;
    }
    void Update()
    {
        totalInventory = woodInventory + clothInventory + ropeInventory + foodInventory;
     

        //Update text
        invText.text = "Inventory \nWood :" + woodInventory.ToString() + "   Cloth: " + clothInventory.ToString() +"\nRope: "+ ropeInventory.ToString()+"   Food:"+foodInventory;

        //Check Inventory fullness
        if (totalInventory == maxInventory)
        {
            invText.text += "\n Too Many Items! Press '1' '2' '3' or '4'";
        }

        //Check if Speedboost Equipped
        if (SpeedBoost)
        {
            maxSpeed = boostedSpeed;
        }

        //Drop an items ---------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.Space) && !onPlatform)
        {
            if (woodInventory > 0)
            {
                Vector3 newPosition = transform.position;
                newPosition.z -= 1;
                woodInventory--;
                Instantiate(Wood, newPosition, transform.rotation);
            } else if (clothInventory > 0)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.z -= 1;
                    clothInventory--;
                    Instantiate(Cloths, newPosition, transform.rotation);
                } else if (ropeInventory > 0)
            {
                Vector3 newPosition = transform.position;
                newPosition.z -= 1;
                ropeInventory--;
                Instantiate(Rope, newPosition, transform.rotation);
            } else if (foodInventory > 0)
            {
                Vector3 newPosition = transform.position;
                newPosition.z -= 1;
                foodInventory--;
                Instantiate(Food, newPosition, transform.rotation);
            }
        }
        
        //---------------------------------------------------------------------------
        





        Move();


        //Slow down player with more items-----------------------------------------
        movementSpeed = maxSpeed * (1 - (totalInventory / maxInventory));

        //Check if drowning
        float waterLevel = Water.GetComponent<Transform>().position.y;
        if (waterLevel > (transform.position.y + 2))
        {
            Dying(); 
        }

        //if not on Platform
        onPlatform = false;


    }

    private void Move()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;
        Vector3 gravityVector = Vector3.zero;

        if (!controller.isGrounded)
        {
            gravityVector.y -= gravity;

        }

        if (desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed);
        }

        float targetSpeed = movementSpeed * movementInput.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        controller.Move(desiredMoveDirection*currentSpeed*Time.deltaTime);
        controller.Move(gravityVector * Time.deltaTime);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Wood")
        {
            if (woodInventory < 20)
            {
                woodInventory++;
                Destroy(collision.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Rope")
        {
            if (woodInventory < 20)
            {
                ropeInventory++;
                Destroy(collision.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Cloth")
        {
            if (woodInventory < 20)
            {
                clothInventory++;
                Destroy(collision.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Food")
        {
            if (woodInventory < 20)
            {
                foodInventory++;
                Destroy(collision.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            SpeedBoost = true;
        }

        if (collision.gameObject.tag == "Platform")
        {
            onPlatform = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                collision.gameObject.GetComponent<BoatPlatformController>().woodCollected += woodInventory;
                woodInventory = 0;

                collision.gameObject.GetComponent<BoatPlatformController>().clothCollected += clothInventory;
                clothInventory = 0;

                collision.gameObject.GetComponent<BoatPlatformController>().ropeCollected += ropeInventory;
                ropeInventory = 0;

                collision.gameObject.GetComponent<BoatPlatformController>().foodCollected += foodInventory;
                foodInventory = 0;
            }

        }

    }



    private void Dying()
    {
        Instantiate(Particles, transform.position, transform.rotation);
        Destroy(invText);
        Destroy(gameObject);
        
    }

}
    
