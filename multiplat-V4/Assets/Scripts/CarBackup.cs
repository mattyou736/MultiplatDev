using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBackup : MonoBehaviour
{
    public float maxSpeed = 60.0f;
    public float speed;
    public float powerInput = 10;
    public float accel = 1.0f;
    public float boostMeterValue;

    private float turnInput;
    public float turnSpeed;

    public float hoverForce = 15f;
    public float hoverHeight = 2f;
    private Rigidbody carRigidbody;

    private GameManager gameman;

    public Transform spawnPoint;

    // Use this for initialization
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        gameman = GameObject.Find("gameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        if (InputManager.MainHorizontal() >= 0.2f || InputManager.MainHorizontal() <= -0.2f)
        {
            Debug.Log(InputManager.MainJoystick());
            turnInput = InputManager.MainHorizontal();
            if (turnInput >= 0.1f)
            {
                turnSpeed = 100;
            }
            if (turnInput <= -0.1)
            {
                turnSpeed = -100;
            }

            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        }

        if (InputManager.Gass())
        {
            if (speed <= maxSpeed)
            {
                speed += accel * Time.deltaTime;
            }
        }
        else if (InputManager.Brake())
        {
            speed -= 20 * Time.deltaTime;
        }
        else
        {
            speed -= accel * Time.deltaTime;
            carRigidbody.AddRelativeForce(0f, 0f, -speed);
        }


        if (InputManager.Boost())
        {
            ///
            if (boostMeterValue > 0 && speed <= maxSpeed)
            {
                speed += 20 * Time.deltaTime;
                boostMeterValue -= 20 * Time.deltaTime;
            }
        }

        carRigidbody.AddRelativeForce(0f, 0f, speed);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (speed <= -1)
        {
            speed = -1;
        }
        if (boostMeterValue <= 0)
        {
            boostMeterValue = 0;
        }

        if (transform.position.y <= 0)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            carRigidbody.velocity = Vector3.zero;
            speed = 0;
        }
    }

    void FixedUpdate()
    {


        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "FinishLine")
        {
            gameman.laps += 1;
        }
        if (other.tag == "Up")
        {
            gameman.score += 100;
        }
        if (other.tag == "Checkpoint")
        {
            spawnPoint = other.gameObject.transform;
        }
    }
}