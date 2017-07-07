using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HeroesCollisionGeneratorII
{
    class GeneralMethods
    {
        static string[] OBJ_SplitStrings; // Used when splitting the current lines of .OBJ files.
        static CollisionStructs.VertexStruct OBJ_CurrentVertex; // Assign the vertex here as it is read.
        static CollisionStructs.CL_TriangleStruct OBJ_CurrentTriangle; // Assign the current triangle here as it is read.

        // This is a near copy of Igorseabra's VB.NET text parser, only minor tweaks.
        public static void ReadObjectFile() // Reads a Wavefront .obj file.
        {
            Program.OBJ_File = File.ReadAllLines(Program.FilePath); // Read all of the lines.

            // Go through all strings | This could be rewritten as a multithreaded operation but would be thread unsafe as triangles do reference vertex specific IDs.
            // This operation is already fast, thus not worth the effort of multithreading.
            for (int x = 0; x < Program.OBJ_File.Length; x++)
            {
                if (Program.OBJ_File[x].Length > 2) // Everything of lesser length is useless.
                {
                    OBJ_SplitStrings = Program.OBJ_File[x].Split(" "); // Remove spaces, place strings to array.
                    OBJ_SplitStrings = OBJ_SplitStrings.Where(ArrayEntry => ArrayEntry.Length != 0).ToArray(); // Remove empty entries. Danq Linq.
                    if (OBJ_SplitStrings[0] == "v") { ObjectFile_AddVertex(); } // If it is a vertex
                    // else if (OBJ_SplitStrings[0] == "vn") { ObjectFile_AddNormal(); } Disabled.
                    else if (OBJ_SplitStrings[0] == "f") { ObjectFile_AddTriangle(); } // If it is a triangle (or face)
                }
            }
        }

        // Adds a vertex to the current object file.
        public static void ObjectFile_AddVertex()
        {
            CollisionStructs.VertexStruct TempVertex;
            TempVertex.PositionX = Convert.ToSingle(OBJ_SplitStrings[1]);
            TempVertex.PositionY = Convert.ToSingle(OBJ_SplitStrings[2]);
            TempVertex.PositionZ = Convert.ToSingle(OBJ_SplitStrings[3]);
            Program.OBJ_Vertex.Add(TempVertex);
        }

        // Adds a vertex to the current object file.

        /* Disabled, for face normals.
        public static void ObjectFile_AddNormal()
        {
            CollisionStructs.VertexStruct TempVertex;
            TempVertex.PositionX = Convert.ToSingle(OBJ_SplitStrings[1]);
            TempVertex.PositionY = Convert.ToSingle(OBJ_SplitStrings[2]);
            TempVertex.PositionZ = Convert.ToSingle(OBJ_SplitStrings[3]);
            Program.OBJ_VertexNormals.Add(TempVertex);
        }
        */

        // Adds a triangle to the current object file.
        public static void ObjectFile_AddTriangle()
        {
            CollisionStructs.CL_TriangleStruct TempTriangle = new CollisionStructs.CL_TriangleStruct();
            
            TempTriangle.VertexOneID = Convert.ToUInt16( OBJ_SplitStrings[1].Split("/")[0]);  // Get all of the vertices for the triangles.
            TempTriangle.VertexTwoID = Convert.ToUInt16(OBJ_SplitStrings[2].Split("/")[0]); 
            TempTriangle.VertexThreeID = Convert.ToUInt16(OBJ_SplitStrings[3].Split("/")[0]); 
            TempTriangle.ExtraProperties.TempTriangleIndex = (ushort)Program.OBJ_TriangleList.Count; // Increment unique index!
            TempTriangle.VertexOneID = (ushort)(TempTriangle.VertexOneID - 1); TempTriangle.VertexTwoID = (ushort)(TempTriangle.VertexTwoID - 1); TempTriangle.VertexThreeID = (ushort)(TempTriangle.VertexThreeID - 1);
            Program.OBJ_TriangleList.Add(TempTriangle);
            Program.PeepingTom();
        }

        // Executes a for loop, executing every alternate item on a new thread.
        // E.g. 2 threaded.
        // Thread 1 executes 1,3,5,7 (2n - 1)th
        // This is an optimization :)
        /*
        public static void MultithreadedAlternateVariableForLoop(int InitialIndex, CollisionStructs.CL_TriangleStruct[] TriangleArray)
        {
            int AmountOfIterationsPerThread = Environment.ProcessorCount;

            Parallel.For
            (InitialIndex, AmountOfIterationsPerThread,
                
                WorkerID =>
                {                                    // WorkerID is One Indexed
                    var FirstIteration = (TriangleArray.Length / AmountOfIterationsPerThread) * (WorkerID);
                    var MaximumIteration = (TriangleArray.Length / AmountOfIterationsPerThread) * (WorkerID + 1);
                    
                    for (FirstIteration < MaximumIteration; FirstIteration++)
                    {
                        // Do stuff
                    }
                }
            )
        }
        */
    }
}
