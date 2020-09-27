using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{   //Kreiramo enum za score
    public enum Score
    {
        AiScore, PlayerScore
    }
    //Da se zna tko je zabio
    public Text AiScoreTxt, PlayerScoreTxt;
    //Instanca UiManagera
    public UiManager uiManager;
    //Do koliko ide pa se restarta
    public int MaxScore;

    #region Scores
    private int aiScore, playerScore;
    //Property ==> slicno ko polja ali mozes runnati kod
    private int AiScore
    {
        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(true);
        }
    }

    private int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(false);
        }
    }

    #endregion
    //Da znamo sto treba incrementirati
    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AiScore)
            AiScoreTxt.text = (++AiScore).ToString();
        else
            PlayerScoreTxt.text = (++PlayerScore).ToString();
    }
    //Resetirati podatke od prosle partije
    public void ResetScores()
    {
        AiScore = PlayerScore = 0;
        AiScoreTxt.text = PlayerScoreTxt.text = "0";
    }
}
