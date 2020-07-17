using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Wood;
    public float boostedSpeed = 20f;
    public bool SpeedBoost = false;
    public float maxSpeed = 10f;
    public float movementSpeed = 10f;
    public float currentSpeed = 0;
    public float speedSmoothVelocity = 0f;
    public float speedSmoothTime = 0.1f;
    public float gravity = 3f;
    public float rotationSpeed = 1f;

    public float woodInventory = 0;
    public float maxInventory = 20;

    private Transform mainCameraTransform = null;
    private CharacterController controller = null;
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;
    }
    void Update()
    {
        if (SpeedBoost)
        {
            maxSpeed = boostedSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            woodInventory--;
        }
           

        Move();

        movementSpeed = maxSpeed * (1 - (woodInventory / maxInventory));
        


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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (woodInventory < 20)
            {
                woodInventory++;
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            SpeedBoost = true;
        }
    }

}
    
