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
        images = GetComponentsInChildren<Image>();


        for (int i = 0; i < ItemManager.Instance.itemPrefabs.Length; ++i)
        {
            if (images.Length >= i)
            {
                images[i].sprite = ItemManager.Instance.itemPrefabs[i].lostIcon;
            }
        }

        //for (int i = 0; i < ItemManager.Instance.items.Length; ++i)
        //{
        //    images.Add(transform.GetChild(i).GetComponent<Image>());
        //    if (images.Count >= i)
        //    {
        //        images[i].sprite = ItemManager.Instance.items[i].lostIcon;
        //    }
        //}

        ItemManager.Instance.itemFound += ItemFound;
    }

    void ItemFound(int id)
    {
        if (images.Length >= id)
        {
            images[id].sprite = ItemManager.Instance.itemPrefabs[id].foundIcon;
        }
    }

  
}
