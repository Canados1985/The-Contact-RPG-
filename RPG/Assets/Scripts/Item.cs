using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool b_IsDefaultItem = false;


    public virtual void Use()
    {

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {

        Inventory.cl_Inventory.Remove(this);
    }
}
