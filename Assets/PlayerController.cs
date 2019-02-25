using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Walk();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    // TODO Make the object walk by rotating and taking "steps"
    void Walk()
    {
        if (rb.velocity.magnitude > 0.5)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 10) * 40);
            transform.position = new Vector3(transform.position.x, 1 + Mathf.PingPong(Time.time * 2, 0.6f), transform.position.z);
        }
        else
        {
            // Lerp back to standing straight up
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }
}