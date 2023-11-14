using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

public enum GameType
{
    Moves,
    Time
}

[System.Serializable]
public class EndGameRequiremenets
{
    public GameType gameType;
    public int counterValue;
}

public class EndGameManager : MonoBehaviour
{
    public GameObject movesLabel;
    public GameObject timeLabel;
    public GameObject youWinPanel;
    public GameObject tryAgainPanel;
    public TextMeshProUGUI counter;
    public EndGameRequiremenets requiremenets;
    public int currentCounterValue;
    private Board board;
    private FadePanelController fadePanelController;
    private float timerSeconds;

    void Start()
    {
        fadePanelController = FindObjectOfType<FadePanelController>();
        board = FindObjectOfType<Board>();
        SetGameType();
        SetUpGame();
    }

    void SetGameType()
    {
        if (board.world != null)
        {
            if (board.level < board.world.levels.Length && board.level >= 0)
            {
                if (board.world.levels[board.level] != null)
                {
                    requiremenets = board.world.levels[board.level].endGameRequiremenets;
                }
            }
        }
    }

    void SetUpGame()
    {
        currentCounterValue = requiremenets.counterValue;
        if (requiremenets.gameType == GameType.Moves)
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

            if (currentCounterValue == 0)
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
        fadePanelController.GameOver();
    }

    public void LoseGame()
    {
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        fadePanelController.GameOver();
    }


    void Update()
    {
        if (requiremenets.gameType == GameType.Time && currentCounterValue>0)
        {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }
}
