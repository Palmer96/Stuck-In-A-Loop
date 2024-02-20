using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Image m_backgroundImage;
    [SerializeField] private GameObject m_successObj;
    [SerializeField] private GameObject m_failObj;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GameplayManager.Instance.gameSuceed += GameSuccess;
        GameplayManager.Instance.gameFail += GameFail;
    }

    public void GameSuccess()
    {
        anim.SetTrigger("Fade");
        m_successObj.SetActive(true);
    }
    public void GameFail()
    {
        anim.SetTrigger("Fade");
        m_failObj.SetActive(true);
    }
}
