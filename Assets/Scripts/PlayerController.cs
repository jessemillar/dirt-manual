using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 3;
    public float rotation = 30;
    public float stepHeight = 0.1f;

    public SpriteRenderer frontSprite;
    public SpriteRenderer backSprite;
    private Rigidbody rb;
    private float standHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // standHeight = frontSprite.transform.position.y;
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
            // Wobble if we're walking
            frontSprite.transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 0, 1) * Quaternion.Euler(0, 180 + Mathf.Sin(Time.time * 10) * rotation * 1.5f, Mathf.Sin(Time.time * 10) * rotation);
            // frontSprite.transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, stepHeight, transform.position.z), Time.deltaTime * 10);

            backSprite.transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 0, 1) * Quaternion.Euler(0, Mathf.Sin(Time.time * 10) * rotation * 1.5f, Mathf.Sin(Time.time * 10) * rotation);
            // backSprite.transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, stepHeight, transform.position.z), Time.deltaTime * 10);
        }
        else
        {
            // Lerp back to standing straight up
            frontSprite.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 180 + transform.eulerAngles.y, 0), Time.deltaTime * 10);
            // frontSprite.transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, standHeight, transform.position.z), Time.deltaTime * 10);

            backSprite.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), Time.deltaTime * 10);
            // backSprite.transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, standHeight, transform.position.z), Time.deltaTime * 10);
        }
    }
}