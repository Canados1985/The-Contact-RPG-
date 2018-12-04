using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Button removeButton;
    Item item;

    public GameObject go_generator;
    public GameObject go_impulseRifle;
    public GameObject go_helmet_lvl3;
    public GameObject go_helmet_lvl2;

    private Transform playerTransform;


    private void Start()
    {

        playerTransform = GameObject.Find("Player").transform;
    }

    public void AddItem(Item newItem)
    {


        item = newItem;
            icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        //
        if (item.name == "Generator")
        {
            Instantiate(go_generator, playerTransform);
        }
        //
        if (item.name == "ImpulseRifle")
        {

            Instantiate(go_impulseRifle, playerTransform);
        }
        //
        if (item.name == "Helmetlvl3")
        {

            Instantiate(go_helmet_lvl3, playerTransform);
        }
        if (item.name == "Helmetlvl2")
        {

            Instantiate(go_helmet_lvl2, playerTransform);
        }

        Inventory.cl_Inventory.Remove(item);

    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
