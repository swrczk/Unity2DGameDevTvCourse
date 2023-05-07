using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
   [SerializeField] int playerLives = 3;
   [SerializeField] int score = 0;
   [SerializeField] TextMeshProUGUI livesText;
   [SerializeField] TextMeshProUGUI scoreText;
   void Awake()
   {
      int numGameSessions = FindObjectsOfType<GameSession>().Length;
      if (numGameSessions > 1)
      {
         gameObject.SetActive(false);
         Destroy(gameObject);
      }
      else
      {
         DontDestroyOnLoad(gameObject);
      }
      UpdateUI();
   }

   public void ProcessPlayerDeath()
   {
      if (playerLives > 1)
         Invoke("TakeLife", 1.0f);
      else
         Invoke("ResetGameSession", 1.0f);
   }

   public void ProcessPickup(int reward)
   {
      score += reward;
      UpdateUI();
   }

   void TakeLife()
   {
      playerLives--;
      int currSceneindex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currSceneindex);
      UpdateUI();
   }

   void ResetGameSession()
   {
      FindObjectOfType<ScenePersist>().ResetScenePersist();
      SceneManager.LoadScene(0);
      Destroy(gameObject);
   }

   void UpdateUI()
   {
      livesText.text = playerLives.ToString();
      scoreText.text = score.ToString();
   }
}
