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

            unsafe
            {
                // be sure to allocate these buffers with Raylib.New or Raylib.MemAlloc
                // Raylib will delete them so it must allocate them. Your code should not allocate this memory
                MeshRef.AllocVertices();
                MeshRef.AllocNormals();
                MeshRef.AllocTexCoords();
                MeshRef.AllocColors();

                MeshRef.AnimNormals = null;
                MeshRef.AnimVertices = null;
                MeshRef.BoneIds = null;
                MeshRef.BoneWeights = null;
                MeshRef.Tangents = null;
                MeshRef.Indices = null;
                MeshRef.TexCoords2 = null;
            }
        }

        public void SetNormal(Vector3 value) { Normal = value; }
        public void SetNormal(float x, float y, float z) { Normal = new Vector3(x, y, z); }
        public void SetSetUV(Vector2 value) { UV = value; }
        public void SetSetUV(float x, float y) { UV = new Vector2(x, y); }

        public void PushVertex(Vector3 vertex, float xOffset = 0, float yOffset = 0, float zOffset = 0)
        {
            int index = 0;
            unsafe
            {
                if (MeshRef.Colors != null)
                {
                    index = TriangleIndex * 12 + VertIndex * 4;

                    MeshRef.Colors[index] = VertColor.R;
                    MeshRef.Colors[index + 1] = VertColor.G;
                    MeshRef.Colors[index + 2] = VertColor.B;
                    MeshRef.Colors[index + 3] = VertColor.A;
                }

                if (MeshRef.TexCoords != null)
                {
                    index = TriangleIndex * 6 + VertIndex * 2;
                    MeshRef.TexCoords[index] = UV.X;
                    MeshRef.TexCoords[index + 1] = UV.Y;
                }

                if (MeshRef.Normals != null)
                {
                    index = TriangleIndex * 9 + VertIndex * 3;
                    MeshRef.Normals[index] = Normal.X;
                    MeshRef.Normals[index + 1] = Normal.Y;
                    MeshRef.Normals[index + 2] = Normal.Z;
                }

                index = TriangleIndex * 9 + VertIndex * 3;
                MeshRef.Vertices[index] = vertex.X + xOffset;
                MeshRef.Vertices[index + 1] = vertex.Y + yOffset;
                MeshRef.Vertices[index + 2] = vertex.Z + zOffset;

                VertIndex++;
                if (VertIndex > 2)
                {
                    TriangleIndex++;
                    VertIndex = 0;
                }
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
