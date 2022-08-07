using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDrag : MonoBehaviour
{
    public int time;
    public BoxCollider box;
    public int dist;
    public GameObject CatPrefab;
    public GameObject canv;
    public void StartScreenDrag()
    {
        StartCoroutine("Drag");
    }

    IEnumerator Drag()
    {
        GameObject temp = Instantiate(CatPrefab, canv.transform);
        Vector3 startLoc = new Vector3(
            Random.Range(box.bounds.min.x, box.bounds.max.x),
            Random.Range(box.bounds.min.y, box.bounds.max.y),
            0);
        temp.transform.position = startLoc;
        Vector3 destination = startLoc + Vector3.left * dist;

        float elapsed = 0;

        while (elapsed < time)
        {
            temp.transform.position = Vector3.Lerp(startLoc, destination, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(temp);
    }
}
