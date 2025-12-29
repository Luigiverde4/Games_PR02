using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughSpawner : MonoBehaviour
{
   public GameObject doughBall;
    public float spawnZ = -2f;

    void OnMouseDown()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.z = spawnZ;

        GameObject newDough = Instantiate(doughBall, spawnPos, Quaternion.identity);
        DraggableDough dd = newDough.GetComponent<DraggableDough>();
        if (dd != null) dd.StartDragging();
    }
}
