using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
   public int CorrectAnswers { get; private set; }
   public int QuestionSeen { get; private set; }

   void Start()
   {
      CorrectAnswers = 0;
      QuestionSeen = 0;
   }

   public void IncrementCorrectAnswers()
   {
      CorrectAnswers++;
   }

   public void IncrementQuestionSeen()
   {
      QuestionSeen++;
   }

   public int Score()
   {
      return Mathf.RoundToInt(CorrectAnswers / (float)QuestionSeen * 100);
   }
}
