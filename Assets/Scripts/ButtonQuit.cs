using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonQuit : ButtonBase
{

    public override void ButtonActivate()
    {
        base.ButtonActivate();
        Application.Quit();
    }
}
