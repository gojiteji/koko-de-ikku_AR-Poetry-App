using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paint: MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject drawingPrefab;
    private bool first=false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject drawing = Instantiate(drawingPrefab);
            lineRenderer = drawing.GetComponent<LineRenderer>();
        }

        if (Input.GetMouseButton(0))
        {
            FreeDraw();
            first=true;
        }
        if(Input.GetMouseButtonUp(0)){
            first=false;
        }
    }

    void FreeDraw()
    {
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        lineRenderer.positionCount++;
        if(first){
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, Camera.main.ScreenToWorldPoint(mousePos));
        }
    }
}