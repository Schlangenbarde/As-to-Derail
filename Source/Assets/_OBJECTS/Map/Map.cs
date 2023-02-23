using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject linePrefab;

    public List<RectTransform> stationPoints = new List<RectTransform>();

    void Start()
    {
        CreateConnection(0, 1, Color.black);
    }

    public void CreateConnection(int stationPointID_start, int stationPointID_end, Color color)
    {
        GameObject lineRef;
        lineRef = Instantiate(linePrefab);
        lineRef.transform.SetParent(transform);
        lineRef.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;

        CanvasGraphicsLine graphicsLine;
        graphicsLine = lineRef.GetComponent<CanvasGraphicsLine>();
        graphicsLine.m_Start = stationPoints[stationPointID_start];
        graphicsLine.m_End = stationPoints[stationPointID_end];
        graphicsLine.color = color;
    }
}
