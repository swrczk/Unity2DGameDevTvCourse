using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
   [SerializeField] float loadDelay = 0.5f;
   [SerializeField] ParticleSystem crashEffect;
   [SerializeField] AudioClip crashSoundEffect;
   AudioSource crashSoundComponent;
   PlayerController playerController;
   bool hasCrashed = false;

   void Start()
   {
      crashSoundComponent = GetComponent<AudioSource>();
      playerController = FindObjectOfType<PlayerController>();
   }
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Ground" && !hasCrashed)
      {
         hasCrashed = !hasCrashed;
         playerController.DisableControls();
         crashEffect.Play();
         crashSoundComponent.PlayOneShot(crashSoundEffect);
         Invoke("ResetScene", loadDelay);
      }
   }
   void ResetScene()
   {
      SceneManager.LoadScene(0);
   }
}
