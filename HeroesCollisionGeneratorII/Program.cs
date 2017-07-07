using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

// This is a .NET Core application, not a .NET Framework application.
// If you have compilation tools, ensure that you have the adequate prereqisites installed for MonoDevelop/XamarinStudio, VisualStudio or whatever your IDE or Compiler is.

namespace HeroesCollisionGeneratorII
{
    class Program
    {
        public static string FilePath; // The path to the file provided by the user.
        public static byte Action = 0; // What am I going to do?
        public static Stopwatch PerformanceWatch = new Stopwatch(); // Benchmarks every action.

        public static string[] OBJ_File; // Wavefront object .obj file here.
        public static List<CollisionStructs.VertexStruct> OBJ_Vertex = new List<CollisionStructs.VertexStruct>(); // All vertices in .obj file.
        public static List<CollisionStructs.VertexStruct> OBJ_VertexNormals = new List<CollisionStructs.VertexStruct>(); // All vertices in .obj file.
        public static List<CollisionStructs.CL_TriangleStruct> OBJ_TriangleList = new List<CollisionStructs.CL_TriangleStruct>(65535); // All triangles in file.
        public static CollisionStructs.CL_TriangleStruct[] OBJ_CopyOfTriangleList; // All triangles in file.

        public static List<CollisionStructs.Node> QuadNodes = new List<CollisionStructs.Node>(); // All quadnodes in new file.
        public static CollisionStructs.Node[] CopyOfNodes; // Used sometimes where working with arrays is more efficient to prevent memory rewrites.
        public static List<CollisionStructs.NodeCalculationProperties> CalculationProperties = new List<CollisionStructs.NodeCalculationProperties>();
        public static List<CollisionStructs.NodeExtraProperties> NodeExtraProperties = new List<CollisionStructs.NodeExtraProperties>();
        public static byte NodeLevel = 8; // The number of nodes that will be used in the application.
        public static byte OffsetPowerLevel = 0x0C; // Offset power level Kawaii!
        public static bool OptimizeIntersectionMaths = false;
        public static bool OptimizeIntersectionMaths2 = false;
        public static bool OptimizeIntersectionMathsAlternate = false;
        public static bool FindNeighbours = false;
        public static ParallelOptions ParallelZ = new ParallelOptions(); // Create a new configuration for parallelism.

        public static CollisionStructs.FileHeader FileHeaderX; // Header to the new .CL file.
        public static uint IntersectionOptimizationLevel = 2048;

        // I do not know when the game transitions from node to node exactly, so just to be safe, the nodes will intersect 62.5% of other surrounding nodes.
        public static float IntersectScalar = 150.0F;

        /// 
        /// GRAND NOTE
        /// SONIC HEROES USES Y AS THE VERTICAL AXIS!
        /// 

        static void Main(string[] args)
        {
            // Check Arguments
            for (int x = 0; x < args.Length; x++)
            {
                if (args[x] == ("-f") | args[x] == ("--file")) { FilePath = args[x + 1]; Action = 1; } // File
                else if (args[x] == ("-n") | args[x] == ("--nodelevel")) { NodeLevel = Convert.ToByte(args[x + 1]); } // NodeCount
                else if (args[x] == ("-l") | args[x] == ("--offsetlevel")) { OffsetPowerLevel = Convert.ToByte(args[x + 1]); } 
                else if (args[x] == ("--optimizeintersections")) { IntersectionOptimizationLevel = Convert.ToUInt32(args[x + 1]); if (IntersectionOptimizationLevel == 0) { OptimizeIntersectionMathsAlternate = true; } else { OptimizeIntersectionMaths = true; } } // NodeCount
                else if (args[x] == ("--optimizeintersectionsalternate")) { IntersectionOptimizationLevel = Convert.ToUInt32(args[x + 1]); OptimizeIntersectionMaths2 = true;  } // NodeCount
                else if (args[x] == ("--intersectscalarlevel")) { IntersectScalar = Convert.ToSingle(args[x + 1]); } // NodeCount
                else if (args[x] == ("--findneighbours")) { FindNeighbours = true; } // NodeCount
            }

            // Get Number of Threads
            ParallelZ.MaxDegreeOfParallelism = Environment.ProcessorCount; // Run parallel for across the count of threads on the CPU.

            if (Action == 0) { DisplayHelp(); Environment.Exit(0); }
            else if (Action == 1) { LetsDoThis(); }

        }

        // It all begins here.
        public static void LetsDoThis()
        {
            // Start the stopwatch for the first time.
            PerformanceWatch.Start();

            // Read the OBJ file.
            Console.Write("Reading Contents of .OBJ File | "); GeneralMethods.ReadObjectFile(); PostActionCleanup();
            Console.Write("Generating QuadTree Properties | "); CLFileMethods.GenerateCollisionFileHeaderProperties(); PostActionCleanup();
            Console.Write("Calculate Triangle Normals, Centers, Adjacent Triangles | "); Console.Write("Total Triangles: " + OBJ_TriangleList.Count + ", Total Vertices: " + Program.OBJ_Vertex.Count + " | "); CLFileMethods.ProcessAllTriangles(); PostActionCleanup();
            Console.Write("Generating QuadTree Nodes | "); CLFileMethods.GenerateQuadtree(); Console.Write("Current Total Nodes: " + Program.QuadNodes.Count + ", Depth Level: " + NodeLevel + " | "); PostActionCleanup();
            Console.Write("Finding Triangle Intersections with Nodes | "); CLFileMethods.CalculateTriangleIntersects(); PostActionCleanup();
            if (QuadNodes.Count > 65535) { Console.Write("Finding and Nuking Empty Nodes, Realigning Nodes, Fixing Indexes | "); CLFileMethods.FindEmptyNodes(); Console.Write("Current Final Nodes: " + CopyOfNodes.Length + " | "); PostActionCleanup(); }
            if (FindNeighbours) { Console.Write("Calculating Neighbours | "); CLFileMethods.FindNeighbours(); PostActionCleanup(); }
            Console.Write("Writing File | "); CLFileMethods.WriteCLFile(); PostActionCleanup();
            //Console.Write("Reindexing Nodes | "); CLFileMethods.ReIndexNodes(); Console.Write("Current Total Nodes: " + Program.QuadNodes.Count + " | "); PostActionCleanup();

            // Find Empty Nodes when not last logic:
            // Do an inverse for loop from top values:
            // For each node at depth level x + 1 until x = 2;  || x = 2, depth level 1, nodes 1-5.
            // Navigate the parent node, add parent node to array of nodes.
            //

            // PointF PointX = new PointF(2168.2333F, -33190.11719F);
            // SizeF PointSize = new SizeF(50F,50F);
            // RectangleF PointArea = new RectangleF(PointX, PointSize);

            // Debug
            /*
            foreach (CollisionStructs.Node IndividualNode in CopyOfNodes)
            {
                float NodeIntersectPositionOffset = (float)(((IndividualNode.CalculationProperties.NodeSize / 100.0) * Program.IntersectScalar) - IndividualNode.CalculationProperties.NodeSize) / 2;

                PointF NodePoint = new PointF(IndividualNode.CalculationProperties.NodeMinimumX - NodeIntersectPositionOffset, IndividualNode.CalculationProperties.NodeMinimumZ - NodeIntersectPositionOffset);
                SizeF NodeSize = new SizeF( (IndividualNode.CalculationProperties.NodeSize/100.0F)*IntersectScalar , (IndividualNode.CalculationProperties.NodeSize/100.0F)*IntersectScalar );
                RectangleF NodeArea = new RectangleF(NodePoint, NodeSize);

                if (PointArea.IntersectsWith(NodeArea))
                {
                    Console.WriteLine("Potential Node of Doom: " + IndividualNode.NodeIndex);
                }

            }
            */

            // Debug
            try
            {
                Console.WriteLine("\nDebug Statistics!");
                List<CollisionStructs.Node> OptimizedNodes = CopyOfNodes.Where(x => x.DepthLevel == NodeLevel).ToList();
                Console.WriteLine("Total Bottom Level Nodes: " + CopyOfNodes.Where(x => x.DepthLevel == NodeLevel).Count());
                Console.WriteLine("Min list triangles node level: " + OptimizedNodes.Min(x => x.NodeExtraProperties.TriangleList.Count));
                Console.WriteLine("Avg list triangles node level: " + OptimizedNodes.Average(x => x.NodeExtraProperties.TriangleList.Count));
                Console.WriteLine("Max list triangles node level: " + OptimizedNodes.Max(x => x.NodeExtraProperties.TriangleList.Count));
                Console.WriteLine("Smallest Node Size in Units: " + OptimizedNodes.Min(x => x.CalculationProperties.NodeSize));
            }
            catch { }
            Console.ReadLine();
        }

        // Because the visual studio debugger doesn't let you view watch items in another class under .NET Core, I just call this empty class when I debug to see some variables :P
        // Hence I'm a peeping tom, huehuehuehuehue
        public static void PeepingTom()
        {

        }

        // Prints Stopwatch time & Cleans Garbage
        public static void PostActionCleanup()
        {
            PerformanceWatch.Stop();
            GC.Collect();
            Console.WriteLine("Success " + PerformanceWatch.ElapsedMilliseconds + "ms");
            PerformanceWatch.Reset();
            PerformanceWatch.Start();
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("\n---------------------------------------------------------------------------------");
            Console.WriteLine("OBJ Collision Generator (HeroesCollisionGeneratorII.exe) by Sewer56lol, Igorseabra4");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("This tool is designed to write functional collision files for Sonic Heroes\n" +
                              "from an Wavefront .OBJ file. This is a <speed> optimized alternative to Igorseabra's\n" +
                              "VB.NET solution, quickly producing quadtree nodes and generating deep quadtrees\n" +
                              "at an greatly improved speed, accuracy is just barely sacrificed for performance <3.");

            Console.WriteLine("-----------------------");
            Console.WriteLine("Usage ( => .CL): OBJColGen.exe --file <OBJFile>");
            Console.WriteLine("Usage ( => .CL, w/ Node Level): OBJColGen.exe --file <OBJFile> --nodelevel <NodeLevelByte>");
            Console.WriteLine("Usage ( => .CL, w/ Node Level & Offset Base Power): OBJColGen.exe --file <OBJFile> --nodelevel <NodeLevelByte> --offsetlevel <offsetpowerlevel>");
            Console.WriteLine("Recommended Use: OBJColGen.exe --file <OBJFile> --nodelevel 5 --optimizeintersections 5 --intersectscalarlevel 100");
            Console.WriteLine("-----------------------\n");
            Console.WriteLine("Additional Arguments:");
            Console.WriteLine("--findneighbours : Will find and bake neighbours into the collision file, (may introduce/resolve crashes)");
            Console.WriteLine("--intersectscalarlevel : Default: 150; Sets the size of node, as percentage, relative to real size when looking for triangle intersections. This prevents gaps from being introduced in the exported collision on flat floors. The default of 150 means that each node will intersect 25% into another node.");
            Console.WriteLine("--optimizeintersections <Value>: Default: 2048 | Makes size of quadtree divisble by supplied value, improved collision file generation performance (may introduce or resolve crashes)");
            Console.WriteLine("--optimizeintersectionsalternate <Value>: Same as --optimizeintersections, but will round the value DOWN instead.");
            Console.WriteLine("--optimizeintersections <0>: Special case, will make value divisible by the power level.");
            Console.WriteLine("--findadjacents : Will find adjacent triangles (potentially but not necessarily buggy).\n");
            Console.WriteLine("The default offset power level is 0x0C, you can try changing it, wonder what effects it'll bring.");
            Console.WriteLine("The recommended node level is level 8 (default), there is a node limit, 65535.");
            Console.WriteLine("You must also watch out for the whole overall triangle limit at 65535.\n");
            Console.WriteLine("Warning: Depths above level 8 may trigger a possibility of overflow of the 65535 limit, especially if your level is very square....");
            Console.WriteLine("Note: If you experience craashes then try running with --optimizeintersections 0 (and with a depth divisible by 2?)");
            Console.WriteLine("Enjoy!");
            Console.ReadLine();
        }
    }
}
