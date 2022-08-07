using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ValueBinder : MonoBehaviour
{
    public ValueScriptable v;
    public string rootstring;
    public string modstring;
    public TextMeshProUGUI tmp;

    public void UpdateText()
    {
        tmp.text = rootstring + v.value + modstring;
    }
}
