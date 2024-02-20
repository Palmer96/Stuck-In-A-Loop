using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : SingletonBase<GameplayManager>
{
    public delegate void GameEvent();
    public GameEvent gameSuceed;
    public GameEvent gameFail;

    bool m_gamePaused;
    public delegate void GameEventToggle(bool pause);
    public GameEventToggle gamePause;


    private bool m_gameRunning = true;
    public bool gameRunning { get { return m_gameRunning; } }

    float exitTimer = 5f;

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
                PauseGame(!m_gamePaused);
            }
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
