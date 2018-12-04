using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory cl_Inventory;

    private void Awake()
    {
        if (cl_Inventory != null)
        {
            Debug.Log("More than one instance of inventory found!");
            return;
        }
        cl_Inventory = this;
    }

    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChangedCallBack;

    public int space = 12;

    public List<Item> items = new List<Item>();


    public bool Add(Item item)
    {
        if (!item.b_IsDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room in inventory");
                return false;
            }
            items.Add(item);

            if(onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
