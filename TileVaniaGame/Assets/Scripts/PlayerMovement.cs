using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] float runSpeed = 10;
   [SerializeField] float climbSpeed = 10;
   [SerializeField] float jumpForce = 10;
   [SerializeField] Vector2 deathKick = new Vector2(5f, 10f);
   [SerializeField] LayerMask groundLayer;
   [SerializeField] LayerMask climbLayer;
   [SerializeField] LayerMask deadlyLayers;
   [SerializeField] GameObject bullet;
   [SerializeField] Transform gun;
   Vector2 moveInput;
   Rigidbody2D rgBody;
   Animator animator;
   CapsuleCollider2D playerCollider;
   CircleCollider2D feetCollider;
   bool playerHasHorizontalSpeed;
   bool playerHasVerticalSpeed;
   bool playerIsOnLadder;
   float defaultGravity;
   bool isAlive = true;
   GameSession gameSession;

   void Start()
   {
      rgBody = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      playerCollider = GetComponent<CapsuleCollider2D>();
      feetCollider = GetComponent<CircleCollider2D>();
      defaultGravity = rgBody.gravityScale;
      gameSession = FindObjectOfType<GameSession>();
   }

   void Update()
   {
      if (!isAlive) return;
      SetAnimations();
      Run();
      FlipSprite();
      ClimbLadder();
      DetectEnemy();
   }

   void SetAnimations()
   {
      playerIsOnLadder = playerCollider.IsTouchingLayers(climbLayer);
      playerHasVerticalSpeed = Mathf.Abs(rgBody.velocity.y) > Mathf.Epsilon;
      playerHasHorizontalSpeed = Mathf.Abs(rgBody.velocity.x) > 0.01;

      animator.SetBool("isClimbing", playerIsOnLadder && playerHasVerticalSpeed);
      animator.SetBool("isRunning", playerHasHorizontalSpeed);
   }

   void OnMove(InputValue value)
   {
      if (!isAlive) return;
      moveInput = value.Get<Vector2>();
   }

   void OnJump(InputValue value)
   {
      if (!isAlive) return;
      if (value.isPressed && feetCollider.IsTouchingLayers(groundLayer))
      {
         Jump();
      }
   }

   void OnFire(InputValue value)
   {
      if (!isAlive) return;
      if (value.isPressed)
      {
         Fire();
      }
   }

   void Run()
   {
      Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rgBody.velocity.y);
      rgBody.velocity = playerVelocity;
   }

   void FlipSprite()
   {
      if (rgBody.velocity.x != 0)
      {
         transform.localScale = new Vector2(Mathf.Sign(rgBody.velocity.x), 1f);
      }
   }

   void Jump()
   {
      rgBody.velocity += new Vector2(0f, jumpForce);
   }

   void ClimbLadder()
   {
      if (!playerIsOnLadder)
      {
         rgBody.gravityScale = defaultGravity;
         return;
      }

      Vector2 climbVelocity = new Vector2(rgBody.velocity.x, moveInput.y * climbSpeed);
      rgBody.velocity = climbVelocity;
      rgBody.gravityScale = 0f;
   }

   void DetectEnemy()
   {
      if (playerCollider.IsTouchingLayers(deadlyLayers))
      {
         Die();
      }
   }

   void Fire()
   {
      Instantiate(bullet, gun.position, transform.rotation);
   }

   void Die()
   {
      isAlive = false;
      animator.SetTrigger("Dying");
      rgBody.velocity = deathKick * new Vector2(-Mathf.Sign(rgBody.velocity.x), 1);

      gameSession.ProcessPlayerDeath();
   }
}
