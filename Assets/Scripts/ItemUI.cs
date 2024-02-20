using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
   public Image[] images;
    // Start is called before the first frame update
    void Start()
    {
        //prefill item icons
        images = GetComponentsInChildren<Image>();

        for (int i = 0; i < ItemManager.Instance.itemPrefabs.Length; ++i)
        {
            if (images.Length >= i)
            {
                images[i].sprite = ItemManager.Instance.itemPrefabs[i].lostIcon;
            }
        }
        ItemManager.Instance.itemFound += ItemFound;
    }

    // update item icon when an item is found
    void ItemFound(int id)
    {
        if (images.Length >= id)
        {
            images[id].sprite = ItemManager.Instance.itemPrefabs[id].foundIcon;
        }
    }  
}
