using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsRenderer : Graphic
{
    public struct PlaneMatrix
    {
        public Vector3 p0;
        public Vector3 p1;
        public Vector3 p2;
        public Vector3 p3;

        public PlaneMatrix(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
    }
    public struct LineMatrix
    {
        public Vector3 p0;
        public Vector3 p1;

        public LineMatrix(Vector3 p0, Vector3 p1)
        {
            this.p0 = p0;
            this.p1 = p1;
        }
    }

    public static void RenderPlane(VertexHelper vh, PlaneMatrix matrix, Color color)
    {
        UIVertex vertex = UIVertex.simpleVert;

        vertex.color = color;
        vertex.position = matrix.p0;
        vh.AddVert(vertex);

        vertex.color = color;
        vertex.position = matrix.p1;
        vh.AddVert(vertex);

        vertex.color = color;
        vertex.position = matrix.p2;
        vh.AddVert(vertex);

        vh.AddTriangle(0, 1, 2);

        vertex.color = color;
        vertex.position = matrix.p3;
        vh.AddVert(vertex);

        vh.AddTriangle(2, 3, 0);
    }

    public static void RenderLine(VertexHelper vh, LineMatrix matrix, Color color, float thickness)
    {
        Vector3 lineDirection = (matrix.p1 - matrix.p0).normalized;
        Vector3 sizeDirection = new Vector3(lineDirection.x, -lineDirection.y, lineDirection.z);
        Vector3 offset = sizeDirection * thickness / 2;

        Vector3 p0 = matrix.p0 - offset;
        Vector3 p1 = matrix.p0 + offset;

        Vector3 p2 = matrix.p1 + offset;
        Vector3 p3 = matrix.p1 - offset;

        PlaneMatrix planeMatrix = new PlaneMatrix(p0, p1, p2, p3);

        RenderPlane(vh, planeMatrix, color);
    }
}
