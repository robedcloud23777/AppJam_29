using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tab : MonoBehaviour
{
    public abstract void OpenTab();
    public abstract void CloseTab();
}