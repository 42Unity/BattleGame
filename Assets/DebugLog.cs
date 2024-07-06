using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class DebugLog : MonoBehaviour
{
    public static DebugLog Instance { get; private set; }
    private ScrollRect scroller;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        scroller = GetComponent<ScrollRect>();
        Instance = this;
    }

    public static void Print(string message)
    {
        Instance.Print_(message);
        Debug.Log(message);
    }

    private void Print_(string message)
    {
        text.text += message + '\n';
        scroller.normalizedPosition = new Vector2(0, 0);
    }
}
