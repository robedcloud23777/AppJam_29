using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SellListElement : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text price;
    public void Set(SellToShopElement element)
    {
        itemIcon.sprite = element.item.itemIcon;
        price.text = $"{element.buyingFor}$";
    }
}