using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public float steerSpeed = 200f;
    public float moveSpeed = 20f;
    public float slowSpeed = 15f;
    public float boostSpeed = 30f;
    public float buffDuration = 5f;

    float baseSpeed;

    void Update()
    {
        float steerAmout = -Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float speedAmout = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, steerAmout);
        transform.Translate(0, speedAmout, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("slow speed");
        moveSpeed = slowSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedUp")
        {
            Debug.Log("boost speed");
            moveSpeed = boostSpeed;

        }
    }

}
