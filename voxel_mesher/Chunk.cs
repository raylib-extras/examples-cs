using Raylib_cs;
using System;
using System.Numerics;

using static Raylib_cs.Raylib;

namespace VoxelMesherSharp
{
    public class Chunk
    {
        public CubeGeometryBuilder Builder = new CubeGeometryBuilder();

        public static readonly int ChunkDepth = 16;
        public static readonly int ChunkSize = 16;

        sbyte[] VoxelChunk = new sbyte[16 * 16 * 16];

        public void Build()
        {
            // fill the chunk with layers of blocks
            for (int d = 0; d < ChunkDepth; d++)
            {
                sbyte block = 0;
                if (d > 6)
                {
                    block = 1;
                    if (d > 8)
                    {
                        block = 2;
                        if (d > 10)
                            block = -1;
                    }
                }

                for (int v = 0; v < ChunkSize; v++)
                {
                    for (int h = 0; h < ChunkSize; h++)
                    {
                        int index = GetIndex(h, v, d);

                        VoxelChunk[index] = block;
                    }
                }
            }

            // Remove some chunks 
            for (int i = 0; i < 500; i++)
            {
                int h = GetRandomValue(0, ChunkSize - 1);
                int v = GetRandomValue(0, ChunkSize - 1);
                int d = GetRandomValue(0, 10);

                int index = GetIndex(h, v, d);

                VoxelChunk[index] = -1;
            }

            // Add some gold
            for (int i = 0; i < 100; i++)
            {
                int h = GetRandomValue(0, ChunkSize - 1);
                int v = GetRandomValue(0, ChunkSize - 1);
                int d = GetRandomValue(0, 10);

                int index = GetIndex(h, v, d);

                VoxelChunk[index] = 3;
            }
        }

        public void GenerateMesh()
        {
            // figure out how many faces will be in this chunk and allocate a mesh that can store that many
            Builder.Allocate(GetChunkFaceCount());

            for (int d = 0; d < ChunkDepth; d++)
            {
                for (int v = 0; v < ChunkSize; v++)
                {
                    for (int h = 0; h < ChunkSize; h++)
                    {
                        if (!BlockIsSolid(h, v, d))
                            continue;

                        // build up the list of faces that this block needs
                        bool[] faces = new bool[6] { false, false, false, false, false, false };

                        if (!BlockIsSolid(h - 1, v, d))
                            faces[CubeGeometryBuilder.EastFace] = true;

                        if (!BlockIsSolid(h + 1, v, d))
                            faces[CubeGeometryBuilder.WestFace] = true;

                        if (!BlockIsSolid(h, v - 1, d))
                            faces[CubeGeometryBuilder.NorthFace] = true;

                        if (!BlockIsSolid(h, v + 1, d))
                            faces[CubeGeometryBuilder.SouthFace] = true;

                        if (!BlockIsSolid(h, v, d + 1))
                            faces[CubeGeometryBuilder.UpFace] = true;

                        if (!BlockIsSolid(h, v, d - 1))
                            faces[CubeGeometryBuilder.DownFace] = true;

                        // build the faces that hit open air for this voxel block
                        Builder.AddCube(new Vector3((float)h, (float)d, (float)v), faces, (int)VoxelChunk[GetIndex(h, v, d)]);
                    }
                }
            }
            UploadMesh(ref Builder.MeshRef, false);
        }

        public int GetIndex(int h, int v, int d)
        {
            return (d * (ChunkSize * ChunkSize)) + (v * ChunkSize) + h;
        }

        private bool BlockIsSolid(int h, int v, int d)
        {
            if (h < 0 || h >= ChunkSize)
                return false;

            if (v < 0 || v >= ChunkSize)
                return false;

            if (d < 0 || d >= ChunkDepth)
                return false;

            return VoxelChunk[GetIndex(h, v, d)] >= 0;
        }

        //check all the adjacent blocks to see if they are open, if they are, we need a face for that side of the block.
        private int GetChunkFaceCount()
        {
            int count = 0;
            for (int d = 0; d < ChunkDepth; d++)
            {
                for (int v = 0; v < ChunkSize; v++)
                {
                    for (int h = 0; h < ChunkSize; h++)
                    {
                        if (!BlockIsSolid(h, v, d))
                            continue;

                        if (!BlockIsSolid(h + 1, v, d))
                            count++;

                        if (!BlockIsSolid(h - 1, v, d))
                            count++;

                        if (!BlockIsSolid(h, v + 1, d))
                            count++;

                        if (!BlockIsSolid(h, v - 1, d))
                            count++;

                        if (!BlockIsSolid(h, v, d + 1))
                            count++;

                        if (!BlockIsSolid(h, v, d - 1))
                            count++;
                    }
                }
            }

            return count;
        }
    }
}
