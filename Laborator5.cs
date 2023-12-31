using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
//Clasa ce se leaga de miscarea obiectului
public class CubeMovement
{
    private Vector3 position;
    private float speed = 2.0f;
    private List<Vector3> vertices;

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public CubeMovement(Vector3 startPosition)
    {
        position = startPosition;
        // Incarca Varfurile din fisier
        vertices = ObjLoader.Load("cube_vertices.txt");

    }

    public void OnUpdateFrame(FrameEventArgs e)
    {
        //Schimba pozitia obiectului in functie de tasta apasata
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Key.W))
        {
            Position = new Vector3(Position.X, Position.Y + speed * (float)e.Time, Position.Z);
        }
        if (keyboardState.IsKeyDown(Key.S))
        {
            Position = new Vector3(Position.X, Position.Y - speed * (float)e.Time, Position.Z);
        }
        if (keyboardState.IsKeyDown(Key.A))
        {
            Position = new Vector3(Position.X - speed * (float)e.Time, Position.Y, Position.Z);
        }
        if (keyboardState.IsKeyDown(Key.D))
        {
            Position = new Vector3(Position.X + speed * (float)e.Time, Position.Y, Position.Z);
        }

       

        // Desenare obiect la poziția curentă
        GL.Begin(PrimitiveType.Quads);
        foreach (var vertex in vertices)
        {
            GL.Vertex3(Position + vertex);
        }
        GL.End();
    }

    // Restul codului...
}
public class Camera
{
    private Vector3 position;
    private Vector3 aproapePosition = new Vector3(0, 0, 10);
    private Vector3 departePosition = new Vector3(0, 0, 20);
    private bool isAproape = true;


    public Camera(Vector3 startPosition)
    {
        position = startPosition;
    }

    public void OnUpdateFrame(FrameEventArgs e)
    {
        var keyboardState = Keyboard.GetState();

        //Schimba pozitia camerei din aproape si departe
        if (keyboardState.IsKeyDown(Key.Space))
        {
            isAproape = !isAproape;
        }

        // Moisca camera
        if (keyboardState.IsKeyDown(Key.Up))
        {
            position.Y += 2.0f * (float)e.Time;
        }
        if (keyboardState.IsKeyDown(Key.Down))
        {
            position.Y -= 2.0f * (float)e.Time;
        }
        if (keyboardState.IsKeyDown(Key.Left))
        {
            position.X -= 2.0f * (float)e.Time;
        }
        if (keyboardState.IsKeyDown(Key.Right))
        {
            position.X += 2.0f * (float)e.Time;
        }

        // Set the camera position based on the current state
        if (isAproape)
        {
            position = aproapePosition;
        }
        else
        {
            position = departePosition;
        }
    }
}

public class Game : GameWindow
{
    private CubeMovement cubeMovement;
    private Camera camera;

    public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Cube Movement App")
    {
        cubeMovement = new CubeMovement(new Vector3(0, 0, 0));
        camera = new Camera(new Vector3(0, 0, 10));
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        camera.OnUpdateFrame(e);
        cubeMovement.OnUpdateFrame(e);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        if (cubeMovement != null)
        {
            cubeMovement.OnUpdateFrame(e);  // Make sure to call OnUpdateFrame before rendering
            DrawCube(cubeMovement.Position);
        }

        SwapBuffers();
    }
    private void DrawCube(Vector3 position)
    {
        // Desenare cub la poziția curentă
        GL.Begin(PrimitiveType.Polygon);
        GL.Vertex3(position.X - 0.1f, position.Y - 0.1f, position.Z);
        GL.Vertex3(position.X + 0.1f, position.Y - 0.1f, position.Z);
        GL.Vertex3(position.X + 0.1f, position.Y + 0.1f, position.Z);
        GL.Vertex3(position.X - 0.1f, position.Y + 0.1f, position.Z);
        GL.End();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        GL.Enable(EnableCap.DepthTest);
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);

        
    }

    static void Main()
    {
        using (var game = new Game(800, 600))
        {
            game.Run(60.0);
        }
    }
}
