using UnityEngine;

public class ScoreKeeper :MonoBehaviour
{
    [SerializeField] private int score;

    public int Score
    {
        get => score;
        set => score = value ;
    }

    public void ResetScore()
    {
        score = 0;
    }
    public static ScoreKeeper Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}