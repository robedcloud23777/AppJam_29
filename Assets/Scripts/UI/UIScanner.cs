using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScanner : MonoBehaviour
{
    static UIScanner instance;
    List<RaycastResult> hits = new();
    private void LateUpdate()
    {
        hits.Clear();
    }
    public static List<RaycastResult> ScanUI()
    {
        if(instance == null)
        {
            instance = new GameObject().AddComponent<UIScanner>();
            DontDestroyOnLoad(instance);
        }
        EventSystem current = EventSystem.current;
        current.RaycastAll(new PointerEventData(current) { position = Input.mousePosition }, instance.hits);
        return instance.hits;
    }
}