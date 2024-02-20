using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : PanelBase
{
    public override void PanelStart()
    {
        base.PanelStart();
        GameplayManager.Instance.gamePause += PauseToggled;
    }

    public void PauseToggled(bool paused)
    {
        if (paused)
        {
            OnOpen();
        }
        else
        {
            OnClose();
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public override void OnClose()
    {
        base.OnClose();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
