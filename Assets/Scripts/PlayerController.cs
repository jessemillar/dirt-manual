using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;

    private Rigidbody rb;
    public Animator animator;
    private float minWalkVelocity = 0.3f;

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

    void Walk()
    {
        float velocity = rb.velocity.magnitude;

        // Look in the direction of movement
        if (velocity > 0)
        {
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.LookRotation(-rb.velocity), Time.deltaTime * 10);
        }

        if (velocity > minWalkVelocity)
        {
            animator.speed = Mathf.Clamp(velocity / speed, 0, 1);
            animator.SetBool("walking", true);
        }
        else
        {
            animator.speed = 1;
            animator.SetBool("walking", false);
        }
    }
}