using System;
using System.Drawing; //import bibleoteca standard pentru lucrul cu imagini si culori
using System.IO;
using OpenTK; //import bibleoteca OpenTk pentru grafica 3D
using OpenTK.Graphics; //subcomponenta pentru grafica
using OpenTK.Graphics.OpenGL; //import functionalitati grafice OpenGL pentru OpenTk
using OpenTK.Input;


namespace GHIDOARCA_PETRU_CRISTI_Piramida.c
{

    class SimpleWindow3D : GameWindow
    {

        float rotation_speed1 = 0.0f;
        float rotation_speed2 = 0.0f;
        float angle1;
        float angle2;
        bool showPyramid = true;
        private bool axesControl = true;
        private const int XYZ_SIZE = 75;
        private int transStep = 0;
        private int radStep = 0;
        private int attStep = 0;
        private int alpha = 0;
        private int ok = 1;
        Color triangleColor = Color.White;

        private bool newStatus = false;
        private float[] vec = new float[12];
        // Constructor.
        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        private void CreateAndSave()
        {
            using (StreamWriter sw = new StreamWriter("date.txt"))
            {
                sw.WriteLine("0.0, 1.0, 0.0");
                sw.WriteLine("-1.0, -1.0, 0.0");
                sw.WriteLine("1.0, -1.0, 0.0");
            }
        }

        //Setarea Imaginii de fundal
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Cyan);
            GL.Enable(EnableCap.DepthTest);

            //conditie care verifica data fisierul date.txt exista

            if (!File.Exists("date.txt"))
            {
                CreateAndSave();
            }

            string linie;
            char[] sep = { ',' }; //definire separator pentru citirea din fisier
            int i = 0;

            //citeste datele din fisier "date.txt"
            using (System.IO.StreamReader f = new System.IO.StreamReader("date.txt"))
            {
                while ((linie = f.ReadLine()) != null)
                {
                    string[] numere = linie.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string x in numere)
                    {
                        if (i < vec.Length)
                        {
                            float val;
                            if (float.TryParse(x, out val))
                            {
                                vec[i++] = val;
                            }
                            else
                            {
                                Console.WriteLine("Eroare la parsarea valorii: " + x);
                            }
                        }
                        else
                        {
                            // cand depasim dimensiunea tabloului
                            Console.WriteLine("A fost depasita dimensiunea!!");
                        }
                    }
                }
            }

        }

        //Setarea Marimii Obiectului afisat
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 500);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }
        //Modificarea pozitiei obiectului 
        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            base.OnUpdateFrame(e);

            var keyboard = OpenTK.Input.Keyboard.GetState();
            var mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            // Controlul obiectului prin tastele W (sus) si S (jos) si A (stanga) si D (dreapta).
            if (keyboard[Key.D])
            {
                rotation_speed1 += 10f / 2;
            }
            if (keyboard[Key.A])
            {
                rotation_speed1 -= 10f / 2;
            }

            if (keyboard[Key.W])
            {
                rotation_speed2 += 10f / 2;
            }

            if (keyboard[Key.S])
            {
                rotation_speed2 -= 10f / 2;
            }




            // Control obiect prin mișcarea mouse-ului.
            if (mouse[MouseButton.Left])
            {

                rotation_speed1 = (mouse.X - Width / 2) / 40.0f;
                rotation_speed2 = (mouse.Y - Height / 2) / 40.0f;
            }
            else
            {
                rotation_speed1 = 0.0f;
                rotation_speed2 = 0.0f;
            }
            // Adăugăm logica pentru modificarea culorii triunghiului la apăsarea tastelor
            if (keyboard[Key.R])
            {
                if (triangleColor.R < 255)
                {
                    triangleColor = Color.FromArgb(triangleColor.R + 1, triangleColor.G, triangleColor.B);
                }
            }
            if (keyboard[Key.G])
            {
                if (triangleColor.G < 255)
                {
                    triangleColor = Color.FromArgb(triangleColor.R, triangleColor.G + 1, triangleColor.B);
                }
            }
            if (keyboard[Key.B])
            {
                if (triangleColor.B < 255)
                {
                    triangleColor = Color.FromArgb(triangleColor.R, triangleColor.G, triangleColor.B + 1);
                }
            }







        }

        //Aplicare Modificarii asociate obiectului
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            DrawAxes();
            angle1 += rotation_speed1 * ((float)e.Time / 2);
            GL.Rotate(angle1, 0.0f, 1.0f, 0.0f);
            angle2 += rotation_speed2 * ((float)e.Time * 2);
            GL.Rotate(angle2, 1.0f, 0.0f, 0.0f);
            if (showPyramid)
            {

                DrawPyramid();

            }

            // Actualizăm culoarea triunghiului în funcție de valorile RGB si il afisam
            GL.Color3(triangleColor.R / 255.0f, triangleColor.G / 255.0f, triangleColor.B / 255.0f);

            // Afișăm valorile RGB în consolă
            Console.WriteLine($"R: {triangleColor.R}, G: {triangleColor.G}, B: {triangleColor.B}");

            SwapBuffers(); //afisare frame nou pe ecran
        }

        //Crearea obiectului

        private void DrawPyramid()
        {
            GL.Begin(PrimitiveType.Polygon);
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard[Key.Up])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(ok, 0, 0));
                Console.WriteLine(ok);
            }
            GL.Vertex3(vec[0], vec[1], vec[2]);

            //modifica culoare in functie de tastele introduse
            if (keyboard[Key.Down])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(0, ok, 0));
                Console.WriteLine(ok);
            }

            GL.Vertex3(vec[3], vec[4], vec[5]);

            if (keyboard[Key.Left])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(0, 0, ok));
                Console.WriteLine(ok);
            }
            GL.Vertex3(vec[6], vec[7], vec[8]);

            if (keyboard[Key.Right])
            {
                if (alpha == 255 || ok == 255)
                    alpha = ok = 0;
                alpha++;
                ok++;
                GL.Color3(Color.FromArgb(alpha, 0, 255, 0));
                Console.WriteLine(alpha);
            }
            if (keyboard[Key.Up])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(ok, 0, 0));
                Console.WriteLine(ok);
            }
            GL.Vertex3(vec[0], vec[1], vec[2]);

            //modifica culoare in functie de tastele introduse
            if (keyboard[Key.Down])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(0, ok, 0));
                Console.WriteLine(ok);
            }

            GL.Vertex3(vec[3], vec[4], vec[5]);

            if (keyboard[Key.Left])
            {
                if (ok == 255)
                    ok = 1;
                ok++;
                GL.Color3(Color.FromArgb(0, 0, ok));
                Console.WriteLine(ok);
            }
            GL.Vertex3(vec[6], vec[7], vec[8]);

            if (keyboard[Key.Right])
            {
                if (alpha == 255 || ok == 255)
                    alpha = ok = 0;
                alpha++;
                ok++;
                GL.Color3(Color.FromArgb(alpha, 0, 255, 0));
                Console.WriteLine(alpha);
            }


            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(-10.0f, -10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, 10.0f);

            GL.Color3(0.0, 0.0, 0.0);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, -10.0f);

            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(10.0f, -10.0f, -10.0f);
            GL.Vertex3(-10.0f, -10.0f, -10.0f);

            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(10.0f, 10.0f, 10.0f);
            GL.Vertex3(-10.0f, -10.0f, -10.0f);
            GL.Vertex3(-10.0f, -10.0f, 10.0f);



            GL.End();
        }


        private void DrawAxes()
        {
            GL.Begin(PrimitiveType.Lines);
            // X
            GL.Color3(Color.Red);
            GL.Vertex3(10, 10, 10);
            GL.Vertex3(100, 10, 10);

            // Y
            GL.Color3(Color.Blue);
            GL.Vertex3(10, 10, 10);
            GL.Vertex3(10, 100, 10);

            // Z
            GL.Color3(Color.Black);
            GL.Vertex3(10, 10, 10);
            GL.Vertex3(10, 10, 100);



            GL.LineWidth(20f);
            GL.PointSize(20f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {


            using (SimpleWindow3D example = new SimpleWindow3D())
            {


                example.Run(144.0, 0.0);
            }
        }
    }
}
