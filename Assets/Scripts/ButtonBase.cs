﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// base script for button scripts to inherit from
// contains basic functionality to be overridden
public class ButtonBase : MonoBehaviour
{
    [SerializeField] private Button m_button;
    protected Button button { get { return m_button; } }


    private void Start()
    {
        SetRef();
        m_button.onClick.AddListener(ButtonActivate);
    }
    private void Reset()
    {
        SetRef();
    }

    protected virtual void SetRef()
    {
        m_button = GetComponent<Button>();
    }


    public virtual void ButtonActivate()
    {

    }
}
