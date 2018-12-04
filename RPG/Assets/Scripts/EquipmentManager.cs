using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager cl_EquipmentManager;


        private void Awake()
    {
        cl_EquipmentManager = this;
    }
    #endregion

    Equipment[] currentEquipment;

    public delegate void OnequipmentChanged(Equipment newItem, Equipment oldItem);
    public OnequipmentChanged onEquipmentChanged;


    Inventory inventoryCash; // For cashing and storing the Inventory instance

    private void Start()
    {
        inventoryCash = Inventory.cl_Inventory;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventoryCash.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        
        //Adding Helmet level3 on player's skeleton
        if (newItem.name == "Helmetlvl3")
        {
            Player.cl_Player.helmet_level3.SetActive(true);
            Player.cl_Player.helmet_level2.SetActive(false);

        }
        //Adding Helmet level2 on player's skeleton
        if (newItem.name == "Helmetlvl2")
        {
            Player.cl_Player.helmet_level2.SetActive(true);
            Player.cl_Player.helmet_level3.SetActive(false);
        }

        //Adding Impulse Rifle on player's skeleton
        if (newItem.name == "ImpulseRifle")
        {
            Player.cl_Player.impulseRifle.SetActive(true);
        }

    }


    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventoryCash.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            //Remove Helmet level3 on player's skeleton
            if (oldItem.name == "Helmetlvl3")
            {
                Player.cl_Player.helmet_level3.SetActive(false);
            }
            //Remove Helmet level2 on player's skeleton
            if (oldItem.name == "Helmetlvl2")
            {
                Player.cl_Player.helmet_level2.SetActive(false);
            }

            //Removing Impulse Rifle on player's skeleton
            if (oldItem.name == "ImpulseRifle")
            {
                Player.cl_Player.impulseRifle.SetActive(false);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
