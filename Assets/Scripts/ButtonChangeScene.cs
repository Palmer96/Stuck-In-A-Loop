using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : ButtonBase
{
    [SerializeField]
    private string m_sceneName;

    public override void ButtonActivate()
    {
        base.ButtonActivate();
        SceneManager.LoadScene(m_sceneName);
    }
}
