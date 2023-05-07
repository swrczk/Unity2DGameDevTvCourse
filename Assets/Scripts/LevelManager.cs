using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    MainMenu = 0,
    Game = 1,
    GameOver = 2,
}
public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    public void LoadMainMenu() => LoadScene(Scene.MainMenu);
    public void LoadGame() => LoadScene(Scene.Game);
    public void LoadGameOver() => LoadScene(Scene.GameOver); 
    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (score == null) return;
        score.text = ScoreKeeper.Instance.Score.ToString("000000000");
        ScoreKeeper.Instance.Score = 0;
    }
    
    private static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }

}