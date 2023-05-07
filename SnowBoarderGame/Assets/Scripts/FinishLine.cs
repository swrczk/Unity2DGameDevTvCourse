using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
   [SerializeField] float loadDelay = 2f;
   [SerializeField] ParticleSystem finishEffect;

   AudioSource finishSound;

   void Start()
   {
      finishSound = GetComponent<AudioSource>();
   }

   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         finishEffect.Play();
         finishSound.Play();
         Invoke("ResetScene", loadDelay);
      }
   }
   void ResetScene()
   {
      SceneManager.LoadScene(0);
   }
}
