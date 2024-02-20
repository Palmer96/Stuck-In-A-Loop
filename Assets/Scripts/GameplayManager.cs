using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : SingletonBase<GameplayManager>
{
    // game state events
    public delegate void GameEvent();
    public GameEvent gameSuceed;
    public GameEvent gameFail;

    public delegate void GameEventToggle(bool pause);
    public GameEventToggle gamePause;
    

    private bool m_gameRunning = true;
    public bool gameRunning { get { return m_gameRunning; } }
    
    private bool m_gamePaused = false;
    public bool gamePaused { get { return m_gamePaused; } }
       

    private CharacterMovement_Basic m_player;
    public CharacterMovement_Basic player
    {
        get
        {
            if (m_player == null)
            {
                m_player = FindObjectOfType<CharacterMovement_Basic>();
            }
            return m_player;
        }
    }
    
    float exitTimer;

    private void Start()
    {
        exitTimer = 5f;
    }

    private void Update()
    {
        if (gameRunning == false)
        {
            exitTimer -= Time.deltaTime;
            if (exitTimer <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame(!gamePaused);
            }
        }
    }

    public void GameComplete(bool success)
    {
        m_gameRunning = false;
        if (success)
        {
            gameSuceed?.Invoke();
        }
        else
        {
            gameFail?.Invoke();
        }
    }

    public void PauseGame(bool pause)
    {
        m_gamePaused = pause;
        if (m_gamePaused)
        {
            Time.timeScale = 0;
            gamePause?.Invoke(m_gamePaused);
        }
        else
        {
            Time.timeScale = 1;
            gamePause?.Invoke(m_gamePaused);
        }
    }


}
