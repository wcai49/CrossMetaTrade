using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSystem : MonoBehaviour
{
    #region Singleton
    public static BackpackSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public List<Item> backpackItems = new List<Item>();
    public int space = 9;
    public void getItem (Item item)
    {
        if (backpackItems.Count >= space)
        {
            Debug.Log("Not enough room");
            return;
        }
        if (!item.isDefaultItem)
        {
            backpackItems.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }

    public void removeItem (Item item)
    {
        backpackItems.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
