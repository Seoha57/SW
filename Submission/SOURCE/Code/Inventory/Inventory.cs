﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        //Debug.LogWarning("More than one instance of Inventory found");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);

            if(onItemChangedCallback!=null)
                onItemChangedCallback.Invoke();
  
        }

        return true;
       
    }
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }     
   
    public List<Item> GetItemList()
    {
        return items;
    }

}
