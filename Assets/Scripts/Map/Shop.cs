using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ItemDataIntPair[] m_sellingItem, m_buyingItem;
    public ItemDataIntPair[] sellingItem => m_sellingItem;
    public ItemDataIntPair[] buyingItem => m_buyingItem;
}