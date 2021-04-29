using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement3D : MonoBehaviour
{
    //private Rigidbody rbody;
    private CharacterController charController;

	private bool isWalking = false;
	public bool IsWalking
	{
		get { return isWalking; }
	}

    [SerializeField]
    private Camera playerCamera;
	[SerializeField]
	private float movementSpeed = 1.2f;
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private AudioSource walkingSoundEffect = null;

    private float cameraVerticalAngle = 0f;

	// Use this for initialization
	void Start () {

        //rbody = GetComponent<Rigidbody> ();
        charController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MovePlayer();

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            RotatePlayer();
        }

        HandleWalkingSoundEffect();
	}

    private void MovePlayer()
    {
        //Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 movementVector = transform.forward * Input.GetAxisRaw("Vertical");
        movementVector += transform.right * Input.GetAxisRaw("Horizontal");
        //rbody.transform.for

        movementVector.Normalize();

        if (movementVector != Vector3.zero)
        {
            isWalking = true;
            //movementVector += transform.forward;
            //movementVector.Normalize();
        }
        else
        {
            isWalking = false;
        }
        //rbody.MovePosition(rbody.position + movementVector * Time.fixedDeltaTime * movementSpeed);
        charController.Move(movementVector * Time.deltaTime * movementSpeed);
    }

    private void RotatePlayer()
    {
        // horizontal character rotation
        {
            // rotate the transform with the input speed around its local Y axis
            transform.Rotate(new Vector3(0f, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0f), Space.Self);
        }

        // vertical camera rotation
        {
            // add vertical inputs to the camera's vertical angle
            cameraVerticalAngle += -1 * Input.GetAxisRaw("Mouse Y") * rotationSpeed;

            // limit the camera's vertical angle to min/max
            cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

            // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
            playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
        }
    }

    private void HandleWalkingSoundEffect()
    {
        if(isWalking)
        {
            if (!walkingSoundEffect.isPlaying)
            {
                walkingSoundEffect.Play();
            }
        }
        else
        {
            walkingSoundEffect.Stop();
        }
    }

	
    private void OnDisable()
    {
        walkingSoundEffect.Stop();
    }
}
