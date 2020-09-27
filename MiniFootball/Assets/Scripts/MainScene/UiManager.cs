using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Globalization;
//Klasa za Ui manager
public class UiManager : MonoBehaviour
{
    public int scorePlyr, scoreAi;
    public Text PlayerMainScore, AiMainScore;
    //Non presistant singleton
    #region Singleton
    public static UiManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    //Da nam je lakse pratiti varijable u Unityu napravit cemo headere
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject WinTxt;
    public GameObject LoseTxt;
 

    [Header("Other")]
    public AudioManager audioManager;

    public ScoreScript scoreScript;

    //public BallScript ballScript;
    //public PlayerMovement playerMovement;
    //public AiScript aiScript;
    public List<IResetable> ResetableGameObjects = new List<IResetable>();

    //Zelimo li pokazati Win ili Lose text
    public void ShowRestartCanvas(bool didAiWin)
    {
        //Zelimo zaustaviti igru
        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (didAiWin)
        {
            audioManager.PlayLostGame();
            WinTxt.SetActive(false);
            LoseTxt.SetActive(true);
            scorePlyr++;
            PlayerMainScore.text = scorePlyr.ToString();
        }
        else
        {
            audioManager.PlayWinGame();
            WinTxt.SetActive(true);
            LoseTxt.SetActive(false);
            scoreAi++;
            AiMainScore.text = scoreAi.ToString();
        }
    }
    //Ponasanje u restartu igre
    public void RestartGame()
    {
        Time.timeScale = 1;

        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScores();

        foreach (var obj in ResetableGameObjects)
            obj.ResetPosition();
        //ballScript.CenterBall();
        //Reset igraca
        //playerMovement.ResetPosition();
        //aiScript.ResetPosition();
    }

    //Za prikaz menua
    public void ShowMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
    }

}
