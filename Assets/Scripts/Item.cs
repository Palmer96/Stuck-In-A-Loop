using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int m_itemID;
    public int itemID { get { return m_itemID; } }
    
    [SerializeField]
    private Sprite m_lostIcon;
    public Sprite lostIcon { get { return m_lostIcon; } }

    [SerializeField]
    private Sprite m_foundIcon;
    public Sprite foundIcon { get { return m_foundIcon; } }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ItemManager.Instance.ItemFound(this);
        }
    }
}
