using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResume : ButtonBase
{
    public override void ButtonActivate()
    {
        base.ButtonActivate();
        Debug.Log("Button Pressed");
        GameplayManager.Instance.PauseGame(false);
    }
}
