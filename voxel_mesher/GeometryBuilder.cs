using Raylib_cs;
using System;
using System.Numerics;

using static Raylib_cs.Raylib;

namespace VoxelMesherSharp
{
    public class GeometryBuilder
    { 
        // setup the builder with the mesh it is going to fill out
        public GeometryBuilder()
        {
            MeshRef = new Mesh();
        }

        // we need to know how many triangles are going to be in the mesh before we start
        // this way we can allocate the correct buffer sizes for the mesh
        public void Allocate(int triangles)
        {
            // there are 
            MeshRef.VertexCount = triangles * 6;
            MeshRef.TriangleCount = triangles * 2;

            MeshRef.AllocVertices();
            MeshRef.AllocNormals();
            MeshRef.AllocTexCoords();
            MeshRef.AllocColors();
        }

        public void SetNormal(Vector3 value) { Normal = value; }
        public void SetNormal(float x, float y, float z) { Normal = new Vector3(x, y, z); }
        public void SetSetUV(Vector2 value) { UV = value; }
        public void SetSetUV(float x, float y) { UV = new Vector2(x, y); }

        public void PushVertex(Vector3 vertex, float xOffset = 0, float yOffset = 0, float zOffset = 0)
        {
            MeshRef.ColorsAs<Color>()[TriangleIndex * 3 + VertIndex] = VertColor;
            MeshRef.TexCoordsAs<Vector2>()[TriangleIndex * 3 + VertIndex] = UV;
            MeshRef.NormalsAs<Vector3>()[TriangleIndex * 3 + VertIndex] = Normal;
            MeshRef.VerticesAs<Vector3>()[TriangleIndex * 3 + VertIndex] = vertex + new Vector3(xOffset, yOffset, zOffset);

            VertIndex++;
            if (VertIndex > 2)
            {
                TriangleIndex++;
                VertIndex = 0;
            }
        }

        public Mesh MeshRef;

        protected int TriangleIndex = 0;
        protected int VertIndex = 0;

        Vector3 Normal = new Vector3(0, 0, 0);
        Color VertColor = Color.WHITE;
        Vector2 UV = new Vector2(0, 0);
    }
}
