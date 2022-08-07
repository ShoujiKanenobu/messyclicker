using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickFill : MonoBehaviour, IPointerDownHandler//, IPointerClickHandler
{
    public ParticleCell p;
    public GameEvent clickedEvent;
    // Start is called before the first frame update
    void Start()
    {
        p = this.gameObject.GetComponent<ParticleCell>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(p.state == particleType.empty)
            p.state = particleType.full;
        clickedEvent.Raise();
    } 

    /*public void OnPointerClick(PointerEventData eventData)
    {
        if (p.state == particleType.empty)
            p.state = particleType.full;
    }*/
}
