using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 3;
    public float rotation = 30;
    public float stepHeight = 0.1f;

    private SpriteRenderer sprite;
    private Rigidbody rb;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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

    void Walk()
    {
        if (rb.velocity.magnitude > 0.5)
        {
            sprite.transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 0, 1) * Quaternion.Euler(0, Mathf.Sin(Time.time * 10) * rotation * 1.5f, Mathf.Sin(Time.time * 10) * rotation);
            sprite.transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, stepHeight, transform.position.z), Time.deltaTime * 10);
        }
        else
        {
            // Lerp back to standing straight up
            sprite.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), Time.deltaTime * 10);
            sprite.transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Time.deltaTime * 10);
        }
    }
}