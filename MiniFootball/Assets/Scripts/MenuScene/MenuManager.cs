using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //Da fixamo toggle
    public Toggle MultyplayerToggle;
    public GameObject DifficultyToggles;
          

    private void Start()
    {
        //Moramo slusati onEvent change
        MultyplayerToggle.onValueChanged.AddListener(
            isMultyplayerOn => DifficultyToggles.SetActive(!isMultyplayerOn));
        MultyplayerToggle.isOn = GameValues.IsMultyplayer;

        //Na pocetku zelimo da je easy ukljucen
        DifficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetMultyplayer(bool isOn)
    {
        GameValues.IsMultyplayer = isOn;
    }

    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Easy;
    }

    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Medium;
    }

    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Hard;
    }
    #endregion
}
