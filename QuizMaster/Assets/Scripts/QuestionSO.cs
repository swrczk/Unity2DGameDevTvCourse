using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Quiz Question", order = 0)]
public class QuestionSO : ScriptableObject
{
   [TextArea(2, 6)]
   [SerializeField] string question = "Enter new question text here";
   [SerializeField] List<string> answers = new List<string>() { "Enter new answer text here" };
   [SerializeField] int correctAnswerIndex;


   public string GetQuestion()
   {
      return question;
   }
   public List<string> GetAnswers()
   {
      return answers;
   }
   public string GetAnswer(int index)
   {
      return answers[index];
   }
   public int GetCorrectAnswerIndex()
   {
      return correctAnswerIndex;
   }
   public string GetCorrectAnswer()
   {
      return answers[correctAnswerIndex];
   }
}
