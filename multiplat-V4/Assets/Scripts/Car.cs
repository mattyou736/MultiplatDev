using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float maxSpeed = 60.0f;
    public float speed;
    public float accel = .3f;
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
        GetComponent<Rigidbody>().rotation = Quaternion.identity;
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
            if ( speed <= maxSpeed)
            {
                speed += accel * Time.deltaTime;              
            }
        }
        else if (InputManager.Brake())
        {
            speed -= 20 * Time.deltaTime;
        }
        else if (speed >= 1)
        {
            speed -= accel * Time.deltaTime;
        }


        if (InputManager.Boost())
        {
            ///
           
            if (boostMeterValue > 0)
            {
                maxSpeed = 60;
                if (speed <= maxSpeed)
                {
                    speed += 20 * Time.deltaTime;
                    boostMeterValue -= 20 * Time.deltaTime;
                }
                
            }
        }
        if (InputManager.NoBoost())
        {
            maxSpeed = 40;
        }
        if (boostMeterValue <= 0)
        {
            boostMeterValue = 0;
        }

        if(transform.position.y <= 0)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            speed = 0;
            boostMeterValue = 0;
        }
    }

    void FixedUpdate()
    {

        if(speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            carRigidbody.velocity = transform.forward * speed;
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
            boostMeterValue += 10;
        }
        if (other.tag == "Checkpoint")
        {
            spawnPoint = other.gameObject.transform;
        }
    }
}