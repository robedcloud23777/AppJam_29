using UnityEngine;

[RequireComponent(typeof(DroppedItem))]
public class ItemPickInteraction : Interaction
{
    DroppedItem origin;
    Player_Inventory player;
    private void OnEnable()
    {
        if(origin == null) origin = GetComponent<DroppedItem>();
        if(player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
    }
    public override void OnInteract()
    {
        origin.slot.count = player.Insert(origin.slot.item.Copy(), origin.slot.count);
    }
}