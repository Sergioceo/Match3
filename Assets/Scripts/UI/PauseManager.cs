using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject pausePanel;
    private Board board;
    public bool paused = false;
    public Image soundButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    private SoundManager soundManager;

	// Use this for initialization
	void Start ()
    {
        //In player prefs, the "Sound" key is for sound
        //If sound == 0 mute, if sound = 1 unmute
        if(PlayerPrefs.HasKey("Sound"))
        {
            if(PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOffSprite;
            } else
            {
                soundButton.sprite = musicOnSprite;
            }
        } else
        {
            soundButton.sprite = musicOnSprite;
        }

        pausePanel.SetActive(false);
        board = FindObjectOfType<Board>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void SoundButton()
    {
        //In player prefs, the "Sound" key is for sound
        //If sound == 0 mute, if sound = 1 unmute
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOnSprite;
                PlayerPrefs.SetInt("Sound", 1);
                soundManager.AdjustVolume();
            }
            else
            {
                soundButton.sprite = musicOffSprite;
                PlayerPrefs.SetInt("Sound", 0);
                soundManager.AdjustVolume();
            }
        }
        else
        {
            soundButton.sprite = musicOffSprite;
            PlayerPrefs.SetInt("Sound", 1);
            soundManager.AdjustVolume();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(paused && !pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            board.currentState = GameState.pause;
        }
        if(!paused && pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            board.currentState = GameState.move;
        }
	}

    public void PauseGame()
    {
        paused = !paused;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Splash");
    }
}
