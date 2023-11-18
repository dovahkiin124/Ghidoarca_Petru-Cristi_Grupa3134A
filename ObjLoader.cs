using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;

public static class ObjLoader
{
    public static List<Vector3> Load(string filePath)
    {
        List<Vector3> vertices = new List<Vector3>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                string[] parts = line.Split(' ');

                if (parts.Length > 0)
                {
                    switch (parts[0])
                    {
                        case "v":
                            if (parts.Length >= 4 &&
                                float.TryParse(parts[1], out float x) &&
                                float.TryParse(parts[2], out float y) &&
                                float.TryParse(parts[3], out float z))
                            {
                                vertices.Add(new Vector3(x, y, z));
                            }
                            break;
                            // Add more cases if you need to handle other OBJ elements
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading vertices from OBJ file: " + ex.Message);
        }

        return vertices;
    }
}
