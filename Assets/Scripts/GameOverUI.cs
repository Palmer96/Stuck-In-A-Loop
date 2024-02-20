using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject m_backgroundImage;
    [SerializeField] private GameObject m_successObj;
    [SerializeField] private GameObject m_failObj;

    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.Instance.gameSuceed += GameSuccess;
        GameplayManager.Instance.gameFail += GameFail;
    }

    public void GameSuccess()
    {
        m_successObj.SetActive(true);
    }
    public void GameFail()
    {
        m_failObj.SetActive(true);
    }
}
