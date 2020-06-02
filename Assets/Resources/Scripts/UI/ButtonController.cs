using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private LanternController[] lc;
    [SerializeField] private Text text;
    [SerializeField] private Color onColor;
    [SerializeField] private Color outColor;

    void Start()
    {
        text.color = outColor;
    }

    void OnMouseOver()
    {
        if (lc.Length > 0)
            for (int i = 0; i < lc.Length; ++i)
                lc[i].Change(true);
        text.color = onColor;
    }

    void OnMouseExit()
    {
        if (lc.Length > 0)
            for (int i = 0; i < lc.Length; ++i)
            lc[i].Change(false);
        text.color = outColor;
    }
}
