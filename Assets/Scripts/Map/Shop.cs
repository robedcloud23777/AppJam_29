using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] SellToShopElement[] m_sell;
    [SerializeField] BuyFromShopElement[] m_buy;
    public SellToShopElement[] sell => m_sell;
    public BuyFromShopElement[] buy => m_buy;
}
[System.Serializable]
public class SellToShopElement
{
    public ItemData item;
    public int buyingFor;
}
[System.Serializable]
public class BuyFromShopElement
{
    public ItemData item;
    public int count, sellingFor;
    public int requiredLevel = 0;
}