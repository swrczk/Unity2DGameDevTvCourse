using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
   [Header("Questions")]
   [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
   [SerializeField] TextMeshProUGUI questionText;
   QuestionSO currentQuestion;

   [Header("Answers")]
   [SerializeField] List<GameObject> answerButtons;
   bool hasAnsweredEarly = true;

   [Header("Buttons Colors")]
   [SerializeField] Sprite defaultAnswerSprite;
   [SerializeField] Sprite correctAnswerSprite;

   [Header("Timer")]
   [SerializeField] Image timerImage;
   Timer timer;

   [Header("Scoring")]
   [SerializeField] TextMeshProUGUI scoreText;
   ScoreKeeper scoreKeeper;

   [Header("Slider")]
   [SerializeField] Slider progressBar;
   public bool isComplete = false;

   void Awake()
   {
      timer = FindObjectOfType<Timer>();
      scoreKeeper = FindObjectOfType<ScoreKeeper>();
      SetButtonState(true);
      InitProgressBar();
   }

   void Update()
   {
      timerImage.fillAmount = timer.fillFraction;
      if (timer.loadNextQuestion)
      {
         if (progressBar.value == progressBar.maxValue)
         {
            isComplete = true;
            return;
         }
         GetNextQuestion();
         timer.loadNextQuestion = false;
         hasAnsweredEarly = false;
      }
      else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
      {
         DisplayAnswer(-1);
         SetButtonState(false);
         hasAnsweredEarly = true;
      }
   }

   public void OnAnswerSelected(int index)
   {
      hasAnsweredEarly = true;
      DisplayAnswer(index);
      SetButtonState(false);
      timer.CancelTimer();
   }

   void DisplayQuestion()
   {
      questionText.text = currentQuestion.GetQuestion();
      var answers = currentQuestion.GetAnswers();
      for (int i = 0; i < answers.Count; i++)
      {
         var buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
         buttonText.text = answers[i];
      }
   }

   void DisplayAnswer(int index)
   {
      if (index == currentQuestion.GetCorrectAnswerIndex())
      {
         questionText.text = "Correct!";
         scoreKeeper.IncrementCorrectAnswers();
      }
      else
      {
         questionText.text = "Sorry, the correct answer was: \n" + currentQuestion.GetCorrectAnswer();
      }
      if (index < 0)
      {
         index = currentQuestion.GetCorrectAnswerIndex();
      }
      Image buttonImage = answerButtons[index].GetComponent<Image>();
      buttonImage.sprite = correctAnswerSprite;
      UpdateScoreKeeper();
   }

   void GetNextQuestion()
   {
      if (questions.Count > 0)
      {
         SetButtonState(true);
         SetDefaultButtonSprite();
         GetRandomQuestion();
         DisplayQuestion();
         UpdateProgressBar();
      }
   }

   void GetRandomQuestion()
   {
      int index = Random.Range(0, questions.Count);
      currentQuestion = questions[index];
      if (questions.Contains(currentQuestion))
         questions.Remove(currentQuestion);
   }

   void InitProgressBar()
   {
      progressBar.value = 0;
      progressBar.minValue = 0;
      progressBar.maxValue = questions.Count;
   }

   void UpdateProgressBar()
   {
      progressBar.value++;
   }

   void UpdateScoreKeeper()
   {
      scoreKeeper.IncrementQuestionSeen();
      scoreText.text = $"Score: {scoreKeeper.Score()}%";
   }

   void SetButtonState(bool state)
   {
      foreach (var answerButton in answerButtons)
      {
         answerButton.GetComponent<Button>().interactable = state;
      }
   }

   void SetDefaultButtonSprite()
   {
      foreach (var answerButton in answerButtons)
      {
         answerButton.GetComponent<Image>().sprite = defaultAnswerSprite;
      }
   }
}
