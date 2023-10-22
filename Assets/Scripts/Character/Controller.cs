using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    Vector3 move;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    public float speedMultiplier=1;
    [SerializeField] Animator charAnimator;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SpeedMultiplier"))
        {
            PlayerPrefs.SetFloat("SpeedMultiplier", speedMultiplier);
        }
        else
        {
            speedMultiplier= PlayerPrefs.GetFloat("SpeedMultiplier");
        }
    }
    private void Start()
    {
        charAnimator.GetParameter(0).defaultBool = false;

        if (gameObject!=null)
        {
            cameraManager = FindObjectOfType<CameraManager>();
            inputManager = FindObjectOfType<InputManager>();

            cameraManager.StartFollow(gameObject);
            cameraManager.SetFollowAngle(-90f);

        }
    }
    private void FixedUpdate()
    {
        Movement();
        //Debug.Log(inputManager.GetInput());

    }
    public void Movement()
    {
        float inputX = inputManager.GetInput().x;
        float inputZ = inputManager.GetInput().y;
        move = new Vector3(inputX, 0, inputZ).normalized;

        rb.velocity = move * speed * Time.fixedDeltaTime*speedMultiplier;
        //transform.Translate(move);

        //transform.position += move * Time.fixedDeltaTime * speed;
        if (move!=Vector3.zero)
        {
            charAnimator.SetBool("isRunning", true);
            transform.forward = Vector3.Lerp(transform.forward,move, Time.deltaTime * 50);
            //transform.rotation = Quaternion.LookRotation(move, Vector3.up);
            //transform.LookAt(transform.position + move);
        }
        else
        {
            charAnimator.SetBool("isRunning", false);

        }
    }
    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if (inputManager)
        {
            inputManager.JoystickDisable();

        }
        charAnimator.SetBool("isRunning", false);
    }
    
  }
