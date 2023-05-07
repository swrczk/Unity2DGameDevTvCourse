using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] float torqueAmount = 1f;
   [SerializeField] float boostSpeed = 1f;
   Rigidbody2D rb2d;
   SurfaceEffector2D surfaceEffector2D;
   float surfaceEffectorBaseSpeed;
   bool canMove = true;

   void Start()
   {
      rb2d = GetComponent<Rigidbody2D>();
      surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
      surfaceEffectorBaseSpeed = surfaceEffector2D.speed;
   }

   void Update()
   {
      if (canMove)
      {
         RotatePlayer();
         RespondToBust();
      }
   }

   void RotatePlayer()
   {
      if (Input.GetKey(KeyCode.LeftArrow))
      {
         rb2d.AddTorque(torqueAmount);
      }
      else if (Input.GetKey(KeyCode.RightArrow))
      {
         rb2d.AddTorque(-torqueAmount);
      }
   }

   void RespondToBust()
   {
      if (Input.GetKey(KeyCode.UpArrow))
      {
         surfaceEffector2D.speed = surfaceEffectorBaseSpeed + boostSpeed;
      }
      else
      {
         surfaceEffector2D.speed = surfaceEffectorBaseSpeed;
      }
   }

   public void DisableControls()
   {
      canMove = false;
   }
}
