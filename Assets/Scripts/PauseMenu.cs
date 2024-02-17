using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : PanelBase
{
    public override void PanelStart()
    {
        base.PanelStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                OnClose();
            }
            else
            {
                OnOpen();
            }
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public override void OnClose()
    {
        base.OnClose();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

}
