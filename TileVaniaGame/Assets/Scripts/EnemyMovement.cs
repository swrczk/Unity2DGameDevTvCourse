using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] float moveSpeed = 1f;
   Rigidbody2D rgBody;
   BoxCollider2D wallCollider;
   bool wasTriggered = false;

   void Start()
   {
      rgBody = GetComponent<Rigidbody2D>();
      wallCollider = GetComponent<BoxCollider2D>();
   }

   void Update()
   {
      rgBody.velocity = new Vector2(moveSpeed, 0f);
   }

   void OnTriggerExit2D(Collider2D other)
   {
      if (wasTriggered) return;

      Invoke("delayDetection", 0.5f);
      wasTriggered = true;
      moveSpeed *= -1;
      FlipSprite();
   }

   void FlipSprite()
   {
      var direction = -(Mathf.Sign(rgBody.velocity.x));
      transform.localScale = new Vector2(direction, 1f);
   }

   void delayDetection()
   {
      wasTriggered = false;
   }
}
