using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(Instance);
        
    }

    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;

    private int Coins;

    public bool AddItem(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); 
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();  
        inventoryItem.initialiseItem(item);
        UIController.Instance.UpdateUI();
    }

    public bool SearchForItem(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (itemInSlot.Item == item)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (itemInSlot.Item == item)
                {
                    DestroyImmediate(itemInSlot.gameObject);
                    break;
                }
            }
        }

        UIController.Instance.UpdateUI();
        //Debug.Log("hhh");
    }

    public void AddCoin()
    {
        Coins++;
        UIController.Instance.UpdateUI();
    }

    public int GetCoins()
    {
        return Coins;
    }

    public int GetKeys()
    {
        int keyCount = 0;

        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (itemInSlot.Item.Type == Item.ItemType.SmallKey 
                    || itemInSlot.Item.Type == Item.ItemType.BigKey)
                {
                    keyCount++;
                    //Debug.Log("GGG");
                }
            }
        }

        return keyCount;
    }
}