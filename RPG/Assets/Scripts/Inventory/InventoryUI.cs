﻿using UnityEngine;

public class InventoryUI : MonoBehaviour {

    Inventory inventory;
    InventorySlot[] slots;

    public Transform itemsParent;
    
    public GameObject inventoryUI;


    void Start () {

        inventory = Inventory.cl_Inventory;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
	

	void Update () {

        if (Input.GetKeyDown(KeyCode.I))
        {
            TurnOnOffUI();
        }

    }

    void TurnOnOffUI()
    {

        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    void UpdateUI()
    {
        
        for (int i =0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }


    }
}
