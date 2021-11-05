using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Variables Declaration
    public static UIController Instance;

    public GameObject UI_MainMenu;

    public GameObject UI_LevelSelection;

    public GameObject _UI_Settings;

    public GameObject[] levels;

    private int index;

    public Button nextButton;

    public Button previousButton;

    public bool nextButtonClick;

    public bool previousButtonClick;

    public int selectedLevelIndex;

    public GameObject[] levelObjectiveText;
    

    #endregion

    #region Start/Awake

    void Start()
    {
        UI_initiate();
        LevelUnlockSystem();
        Debug.Log(PlayerPrefs.GetInt("LevelUnlocked"));
        Debug.Log(index);
    }


   

    #endregion

    #region UI

    public void UI_initiate()
    {
        if (index == 0)
        {
            previousButton.interactable = false;
            levelObjectiveText[0].SetActive(true);
        }
    }

    public void start()
    {
        UI_MainMenu.SetActive(false);
        UI_LevelSelection.SetActive(true);
        levelObjectiveText[index].SetActive(true);
        AudioManager.instance.playSFX();
    }

    public void NextLevel()
    {
        nextButtonClick = true;
        previousButtonClick = false;
        levels[index].SetActive(false);
        levelObjectiveText[index].SetActive(false);
        index++;
        levels[index].SetActive(true);
        levelObjectiveText[index].SetActive(true);
        AudioManager.instance.playSFX();


        if (index == 4)
        {
            nextButton.interactable = false;
        }
        else if (index > 0)
        {
            nextButton.interactable = true;
            previousButton.interactable = true;
        }
    }

    public void PreviousLevel()
    {
        previousButtonClick = true;
        nextButtonClick = false;
        levels[index].SetActive(false);
        levelObjectiveText[index].SetActive(false);
        index--;
        levels[index].SetActive(true);
        levelObjectiveText[index].SetActive(true);
        AudioManager.instance.playSFX();

        if (index == 0)
        {
            previousButton.interactable = false;
        }
        else if (index < 4)
        {
            nextButton.interactable = true;
            previousButton.interactable = true;
        }
    }

    public void activateLevel(int n)
    {
        selectedLevelIndex = index;
        PlayerPrefs.SetInt("SelectedLevel", selectedLevelIndex);
        SceneManager.LoadScene("Gameplay");
    }

    public void _Back_LevelSelection()
    {
        UI_LevelSelection.SetActive(false);
        UI_MainMenu.SetActive(true);
        AudioManager.instance.playSFX();
    }

    public void _UI_SettingsPanel()
    {
        _UI_Settings.SetActive(true);
        AudioManager.instance.playSFX();
    }

    public void _Back_Settings()
    {
        _UI_Settings.SetActive(false);
        UI_MainMenu.SetActive(true);
        AudioManager.instance.playSFX();
    }

    #endregion

    #region Level Unlock System

    public void LevelUnlockSystem()
    {
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].GetComponent<Button>().interactable = false;
        }

        for (int j = 0; j < PlayerPrefs.GetInt("Unlockable"); j++)
        {
            levels[j].GetComponent<Button>().interactable = true;
        }
    }

    #endregion
}