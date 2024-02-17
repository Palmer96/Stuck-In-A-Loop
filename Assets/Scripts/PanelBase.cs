using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    [SerializeField] private GameObject m_basePanel;
    protected GameObject basePanel { get { return m_basePanel; } }

    [SerializeField] private bool m_isOpen;
    protected bool isOpen { get { return m_isOpen; } }

    public void Start()
    {
        PanelStart();
    }



    public virtual void PanelStart()
    {
        if (m_basePanel == null)
        {
            if (transform.childCount > 0)
            {
                m_basePanel = transform.GetChild(0).gameObject;
            }
            else
            {
                m_basePanel = gameObject;
            }
        }

        if (m_basePanel != null)
        {
            m_basePanel.SetActive(false);
        }
    }

    public virtual void OnOpen()
    {
        if (m_basePanel != null)
        {
            m_basePanel.SetActive(true);
        }
        m_isOpen = true;
    }

    public virtual void OnClose()
    {
        if (m_basePanel != null)
        {
            m_basePanel.SetActive(false);
        }
        m_isOpen = false;
    }
}
