using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
   [SerializeField] float loadDelay = 2;
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         StartCoroutine(LoadNextLevel());
      }
   }
   IEnumerator LoadNextLevel()
   {
      yield return new WaitForSecondsRealtime(loadDelay);

      //anything you want to do after waiting;
      FindObjectOfType<ScenePersist>().ResetScenePersist();

      var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      var nextSceneIndex = currentSceneIndex + 1;

      if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
      {
         nextSceneIndex = 0;
      }

      SceneManager.LoadScene(nextSceneIndex);
   }
}
