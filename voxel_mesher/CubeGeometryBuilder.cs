using Raylib_cs;
using System;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace VoxelMesherSharp
{
    // a simple class to help build up faces of a cube
    // can be made to be pure C and take the global data in a structure or global data
    public class CubeGeometryBuilder :  GeometryBuilder
    {
        static Rectangle[] BlockColors = { new Rectangle(0, 0, 0.25f, 1), new Rectangle(0.25f, 0, 0.5f, 1), new Rectangle(0.5f, 0, 0.75f, 1), new Rectangle(0.75f, 0, 1, 1) };

        // indexes for the 6 faces of a cube
        public const int SouthFace = 0;
        public const int NorthFace = 1;
        public const int WestFace = 2;
        public const int EastFace = 3;
        public const int UpFace = 4;
        public const int DownFace = 5;

        public void AddCube(Vector3 position, bool[] faces, int block)
        {
            Rectangle uvRect = BlockColors[block];
            SetSetUV(0, 0);
            //z-
            if (faces[NorthFace])
            {
                SetNormal(0, 0, -1);
                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 1, 0);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 1, 0, 0);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position);

                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 0, 1, 0);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 1, 1, 0);
            }

            // z+
            if (faces[SouthFace])
            {
                SetNormal(0, 0, 1);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 0, 0, 1);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 1, 0, 1);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 1, 1);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 0, 0, 1);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 1, 1);

                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 0, 1, 1);
            }

            // x+
            if (faces[WestFace])
            {
                SetNormal(1, 0, 0);
                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 1, 0, 1);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 1, 0, 0);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 1, 1, 0);

                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 1, 0, 1);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 1, 1, 0);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 1, 1);
            }

            // x-
            if (faces[EastFace])
            {
                SetNormal(-1, 0, 0);

                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 0, 0, 1);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 0, 1, 0);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 0, 0, 0);

                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 0, 0, 1);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 0, 1, 1);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 0, 1, 0);
            }

            if (faces[UpFace])
            {
                SetNormal(0, 1, 0);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 0, 1, 0);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 1, 1);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 1, 1, 0);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 0, 1, 0);

                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 0, 1, 1);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 1, 1);
            }

            SetSetUV(0, 0);
            if (faces[DownFace])
            {
                SetNormal(0, -1, 0);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 0, 0, 0);

                SetSetUV(uvRect.Width, uvRect.Y);
                PushVertex(position, 1, 0, 0);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 0, 1);

                SetSetUV(uvRect.X, uvRect.Y);
                PushVertex(position, 0, 0, 0);

                SetSetUV(uvRect.Width, uvRect.Height);
                PushVertex(position, 1, 0, 1);

                SetSetUV(uvRect.X, uvRect.Height);
                PushVertex(position, 0, 0, 1);
            }
        }
    }
}
