using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas Instance { get; private set; }
    public MainCanvas() => Instance = this;
}