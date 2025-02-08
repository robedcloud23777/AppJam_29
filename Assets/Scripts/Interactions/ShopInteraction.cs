using UnityEngine;

public class ShopInteraction : Interaction
{
    [SerializeField] Shop shop;

    public override void OnInteract()
    {
        UIManager.Instance.shopTab.Set(shop);
        UIManager.Instance.OpenTab(UIManager.Instance.shopTab);
    }
}