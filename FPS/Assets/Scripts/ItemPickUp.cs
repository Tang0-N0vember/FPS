using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;
    

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("picking up " + item.name);
        //bool wasPickedUP = true; //Inventory.instance.Add(item);
        //if(wasPickedUP)Destroy(gameObject);

    }
}
