using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "value scriptable")]
public class ValueScriptable : ScriptableObject
{
    public float value;
    public void Start()
    {
        value = 0;
    }
}
