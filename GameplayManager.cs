using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio.Google;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    #region Variables Declaration
    
    public static GameplayManager instance;
    public GameObject[] levels;

    public int levelIndex;

    public GameObject[] levelText;

    public GameObject levelCompletePanel;
    
    public GameObject gameCompletePanel;

    public Button NextLevelButton;

    public bool levelComplete;

    public GameObject gameOverPanel;

    private int value=0;
    

    #endregion
    
    #region Awake/Start
    
    void Awake()
    {
        
    }
    

    private void Start()
    {
        StartCoroutine(LvLcomplete());  
        singleton();
        Game_LevelStatus();
    }
    

    #endregion

    #region Singleton

    public void singleton()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    #endregion

    #region Check Game/Level Status

    public void Game_LevelStatus()
    {
        levelIndex=PlayerPrefs.GetInt("SelectedLevel");
        Debug.Log("Level is "+PlayerPrefs.GetInt("SelectedLevel"));
        if (PlayerPrefs.GetInt("SelectedLevel")<=4)
        {
            levels[PlayerPrefs.GetInt("SelectedLevel")].SetActive(true);
            Debug.Log("Unlockable Level is "+PlayerPrefs.GetInt("Unlockable"));
            levelText[levelIndex].SetActive(true); 
        }
        else if(PlayerPrefs.GetInt("SelectedLevel")==5)
        {
            gameCompletePanel.SetActive(true);
        }
    }

    #endregion
    
    #region Gameplay UI

    public void restartLevel()
    {
        if (PlayerPrefs.GetInt("SelectedLevel")<=4)
        {
            SceneManager.LoadScene("Gameplay");
        }
        else if (PlayerPrefs.GetInt("SelectedLevel")==5)
        {
           PlayerPrefs.SetInt("SelectedLevel",PlayerPrefs.GetInt("SelectedLevel")-1);
           SceneManager.LoadScene("Gameplay");
        }
       
       
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void nextLevel()
    {
        
        if (PlayerPrefs.GetInt("SelectedLevel")<=4)
        {
            PlayerPrefs.SetInt("SelectedLevel",PlayerPrefs.GetInt("SelectedLevel")+1);
            SceneManager.LoadScene("Gameplay");
        }

       
       
        
    }
    

    private IEnumerator LvLcomplete()
    {
        if (PlayerPrefs.GetInt("SelectedLevel")<4)
        {
            yield return new WaitForSecondsRealtime(2);
            levelComplete = true;
            levelCompletePanel.SetActive(true);
        }
        else
        {
            yield return new WaitForSecondsRealtime(2);
            levelCompletePanel.SetActive(false);
            gameCompletePanel.SetActive(true);
            Debug.Log("GameComplete");
            
        }

        if (PlayerPrefs.GetInt("Unlockable")==PlayerPrefs.GetInt("SelectedLevel"))
        { 
            PlayerPrefs.SetInt("Unlockable",PlayerPrefs.GetInt("SelectedLevel")+1);
            Debug.Log("Unlockable Level Set To  " + PlayerPrefs.GetInt("Unlockable"));
        }
        
    }
    #endregion
    
}
    
    


    

   

   

  

