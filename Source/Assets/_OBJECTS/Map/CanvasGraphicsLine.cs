using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGraphicsLine : Graphic
{
    public RectTransform m_Start;
    public RectTransform m_End;

    protected override void OnPopulateMesh(VertexHelper _vh)
    {
        _vh.Clear();
        if (m_Start != null && m_End != null)
        {
            GraphicsRenderer.LineMatrix line = new GraphicsRenderer.LineMatrix(new Vector3(m_Start.localPosition.x, m_Start.localPosition.y), new Vector3(m_End.localPosition.x, m_End.localPosition.y));
            GraphicsRenderer.RenderLine(_vh, line, color, 0.1f);
        }
    }
}
