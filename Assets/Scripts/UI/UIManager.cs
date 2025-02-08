using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] InventoryTab inventoryTab;
    Tab openTab;
    public void OpenTab(Tab tab)
    {
        if(openTab == tab)
        {
            CloseTab(); return;
        }

        if (openTab != null) openTab.CloseTab();
        openTab = tab;
        if (openTab != null) openTab.OpenTab();
    }
    public void CloseTab()
    {
        if (openTab == null) return;
        openTab.CloseTab();
        openTab = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenTab(inventoryTab);
        }
    }
}