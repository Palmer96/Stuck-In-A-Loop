using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonBase<ItemManager>
{
    [Range(0, 100)]
    [SerializeField] private int spawnChance;
    public Item[] itemPrefabs;
    public List<Item> items;

    private int itemsFound;
    Vector3 itemSpawnPoint = new Vector3(0, -10, 0);

    // item found event
    public delegate void ItemFoundDelegate(int id);
    public ItemFoundDelegate itemFound;
       
    private void Start()
    {
        for (int i = 0; i < itemPrefabs.Length; ++i)
        {
            items.Add(Instantiate(itemPrefabs[i], itemSpawnPoint, Quaternion.identity).GetComponent<Item>());
        }
    }

    
    public void ItemFound(Item foundItem)
    {
        itemFound?.Invoke(foundItem.itemID);
        Destroy(foundItem.gameObject);

        itemsFound++;
        if (itemsFound >= itemPrefabs.Length)
        {
            GameplayManager.Instance.GameComplete(true);
        }
    }

    // returns a random item avaliable in the pool
    public Item GetRandomItem()
    {
        if (Random.Range(0, 100) < spawnChance)
        {
            if (items.Count > 0)
            {
                Item temp = items[Random.Range(0, items.Count)];
                items.Remove(temp);
                return temp;
            }
        }
        return null;
    }

    // returns an item to the pool and resets it's position
    public void ReturnItem(Item returnedItem)
    {
        if (returnedItem != null)
        {
            items.Add(returnedItem);
            returnedItem.transform.position = itemSpawnPoint;
        }
    }


}
