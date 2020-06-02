using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorTools : Editor
{
    [MenuItem("Tools/Reset Player Prefs")]
    public static void ResetPlayerPref()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("<b> **** Player Prefs Deleted **** </b>");
    }
}
