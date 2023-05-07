using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
   [SerializeField] float timeToCompleteQuestion = 30f;
   [SerializeField] float timeToShowCorrectAnswer = 10f;
   [HideInInspector] public bool loadNextQuestion;
   [HideInInspector] public float fillFraction;
   [HideInInspector] public bool isAnsweringQuestion;
   float timerValue;

   void Update()
   {
      UpdateTimer();
   }

   public void CancelTimer()
   {
      timerValue = 0;
   }
   void UpdateTimer()
   {
      timerValue -= Time.deltaTime;

      if (isAnsweringQuestion)
      {
         if (timerValue > 0)
         {
            fillFraction = timerValue / timeToCompleteQuestion;
         }
         else
         {
            isAnsweringQuestion = false;
            timerValue = timeToShowCorrectAnswer;
         }
      }
      else
      {
         if (timerValue > 0)
         {
            fillFraction = timerValue / timeToShowCorrectAnswer;
         }
         else
         {
            isAnsweringQuestion = true;
            timerValue = timeToCompleteQuestion;
            loadNextQuestion = true;
         }
      }
   }
}
