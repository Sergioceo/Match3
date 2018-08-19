﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour {

    [Header("Level Information")]
    public string levelToLoad;
    public int level;
    private GameData gameData;
    private int starsActive;
    private int highScore;

    [Header ("UI Stuff")]
    public Image[] stars;
    public Text highScoreText;
    public Text starText;


    private void Start()
    {
        
    }

    void OnEnable ()
    {
        gameData = FindObjectOfType<GameData>();
        LoadData();
        ActivateStars();
        SetText();
	}

    void LoadData()
    {
        //Is gamedata present?
        if (gameData != null)
        {
            starsActive = gameData.saveData.stars[level - 1];
            highScore = gameData.saveData.highScores[level - 1];
        }
    }

    void SetText()
    {
        highScoreText.text = "" + highScore;
        starText.text = "" + starsActive + "/3";
    }

    void ActivateStars()
    {
        for (int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void Cancel()
    {
        this.gameObject.SetActive(false);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = false;
        }
    }

    public void Play()
    {
        gameData.Save();
        PlayerPrefs.SetInt("Current Level", level - 1);
        SceneManager.LoadScene(levelToLoad); 
    }

}