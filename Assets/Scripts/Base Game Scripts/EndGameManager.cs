using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameType
{
    Moves,
    Time
}

[System.Serializable]
public class EndGameRequirements
{
    public GameType gameType;
    public int counterValue;
}

public class EndGameManager : MonoBehaviour {

    public GameObject movesLabel;
    public GameObject timeLabel;
    public GameObject youWinPanel;
    public GameObject tryAgainPanel;
    public Text counter;
    public EndGameRequirements requirements;
    public int currentCounterValue;
    private float timerSeconds;
    private Board board;
    private GameData gameData;
    private ScoreManager scoreManager;

    // Use this for initialization
    void Start ()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameData = FindObjectOfType<GameData>();
        board = FindObjectOfType<Board>();
        SetGameType();
        SetupGame();      
	}

    void SetGameType()
    {
        if (board.world != null)
        {
            if (board.level < board.world.levels.Length)
            {
                if (board.world.levels[board.level] != null)
                {
                    requirements = board.world.levels[board.level].endGameRequirements;
                }
            }
        }
    }

    void SetupGame()
    {
        currentCounterValue = requirements.counterValue;
        if(requirements.gameType == GameType.Moves)
        {
            movesLabel.SetActive(true);
            timeLabel.SetActive(false);
        }
        else
        {
            timerSeconds = 1;
            movesLabel.SetActive(false);
            timeLabel.SetActive(true);
        }
        counter.text = "" + currentCounterValue;

    }
	
    public void DecreaseCounterValue()
    {
        if (board.currentState != GameState.pause)
        {
            currentCounterValue--;
            counter.text = "" + currentCounterValue;

            if (currentCounterValue <= 0)
            {
                LoseGame();
            }
        }
    }

    public void WinGame()
    {
        youWinPanel.SetActive(true);
        board.currentState = GameState.win;
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        SetStars();
        fade.GameOver();
    }

    public void LoseGame()
    {
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        Debug.Log("You Lose!");
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    void SetStars()
    {
        if(gameData != null)
        {
            if(scoreManager.score >= board.scoreGoals[2])
            {
                gameData.saveData.stars[board.level] = 3;
                //Debug.Log("3 Stars");

            }
            else 
            if (scoreManager.score >= board.scoreGoals[1])
            {
                gameData.saveData.stars[board.level] = 2;
                //Debug.Log("2 Stars");
            }
            else
            if (scoreManager.score >= board.scoreGoals[0])
            {
                gameData.saveData.stars[board.level] = 1;
                //Debug.Log("1 Stars");
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
		if(requirements.gameType == GameType.Time && currentCounterValue > 0)
        {
            timerSeconds -= Time.deltaTime;
            if(timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
	}
}
