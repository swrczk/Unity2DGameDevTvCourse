using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
   [SerializeField] int rewardPoints = 1;
   [SerializeField] AudioClip coinPickupSFX;
   bool wasCollected = false;
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player" && !wasCollected)
      {
         wasCollected = true;
         FindObjectOfType<GameSession>().ProcessPickup(rewardPoints);
         AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
         Destroy(gameObject);
      }
   }
}
