using System;
using System.ComponentModel;
using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace VoxelMesherSharp
{
    internal class Program
    {
        static RenderTexture2D TileTexture;

        static void Main(string[] args)
        {
            // Setup
            const int screenWidth = 1280;
            const int screenHeight = 800;

            InitWindow(screenWidth, screenHeight, "Hello Voxel Chunk #");

            SetTargetFPS(240);

            BuildTexture();

            // Create a chunk mesh
            Chunk chunk = new Chunk();
            chunk.Build();
            chunk.GenerateMesh();

            // Define the camera to look into our 3d world
            Camera3D camera = new Camera3D();
            camera.Position = new Vector3(25.0f, 25.0f, 25.0f);
            camera.Target = new Vector3(0.0f, 0.0f, 0.0f);
            camera.Up = new Vector3(0.0f, 1.0f, 0.0f);
            camera.FovY = 45.0f;
            camera.Projection = CameraProjection.CAMERA_PERSPECTIVE;

            // Model drawing position
            Matrix4x4 transform = Matrix4x4.Transpose(Matrix4x4.CreateTranslation(-8, 0, -8));

            Material material = LoadMaterialDefault();
            SetMaterialTexture(ref material, MaterialMapIndex.MATERIAL_MAP_ALBEDO, TileTexture.Texture);

            // Main game loop
            while (!WindowShouldClose())
            {
                UpdateCamera(ref camera, CameraMode.CAMERA_ORBITAL);

                BeginDrawing();
                ClearBackground(Color.SKYBLUE);

                BeginMode3D(camera);

                DrawMesh(chunk.Builder.MeshRef, material, transform);

                DrawGrid(10, 1.0f);

                EndMode3D();

                EndDrawing();
            }

            // Cleanup
            UnloadMesh(ref chunk.Builder.MeshRef);

            CloseWindow();
        }

        private static void BuildTexture()
        {
            TileTexture = LoadRenderTexture(64, 16);
            BeginTextureMode(TileTexture);
            ClearBackground(Color.BLANK);
            DrawRectangle(0, 0, 16, 16, Color.DARKBROWN);
            DrawRectangle(16, 0, 16, 16, Color.BROWN);
            DrawRectangle(32, 0, 16, 16, Color.GREEN);
            DrawRectangle(48, 0, 16, 16, Color.GOLD);
            EndTextureMode();
        }
    }


}