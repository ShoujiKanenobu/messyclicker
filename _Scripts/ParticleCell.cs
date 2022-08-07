using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum particleType { empty, full, laundry, cat, roomie, takeout, mold }

[RequireComponent(typeof(Image))]
public class ParticleCell : MonoBehaviour
{
    public particleType state = particleType.empty;
    private Image imgref;

    public Color empty;
    public Color full;
    public Color cat;
    public Color laundry;
    public Color roomie;
    public Color takeout;
    public Color mold;


    public void Start()
    {
        imgref = this.GetComponent<Image>();
    }

    public void UpdateColor()
    {
        if (state == particleType.empty)
            imgref.color = empty;
        else if (state == particleType.full)
            imgref.color = full;
        else if (state == particleType.laundry)
            imgref.color = laundry;
        else if (state == particleType.cat)
            imgref.color = cat;
        else if (state == particleType.roomie)
            imgref.color = roomie;
        else if (state == particleType.takeout)
            imgref.color = takeout;
        else if (state == particleType.mold)
            imgref.color = mold;
    }
}
