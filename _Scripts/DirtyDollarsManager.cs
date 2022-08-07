using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyDollarsManager : MonoBehaviour
{
    public ValueScriptable currentCurrency;
    public ValueBinder vb;

    public void GetCurrency()
    {
        currentCurrency.value += 1;
        vb.UpdateText();
    }

    public bool TryRemoveCurrency(int cost)
    {
        if (cost <= currentCurrency.value)
        {
            currentCurrency.value -= cost;
            vb.UpdateText();
            return true;
        }
        return false;
        
    }
}
