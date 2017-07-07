using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Collections.Concurrent;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace HeroesCollisionGeneratorII
{
    class CLFileMethods
    {
        public static void GenerateCollisionFileHeaderProperties()
        {
            float MaxX = 0; float MaxY = 0; float MaxZ = 0; float MinX = 0; float MinY = 0; float MinZ = 0;

            /* Multithreaded version, likely unsafe, no improvement in performance | one thread just happens to overwrite a value which is lower while another thread tries the same. etc.
            Parallel.For(0, Program.OBJ_Vertex.Count,
                index =>
                {
                    if (Program.OBJ_Vertex[index].PositionX > MaxX) { MaxX = Program.OBJ_Vertex[index].PositionX; }
                    if (Program.OBJ_Vertex[index].PositionY > MaxY) { MaxY = Program.OBJ_Vertex[index].PositionY; }
                    if (Program.OBJ_Vertex[index].PositionZ > MaxZ) { MaxZ = Program.OBJ_Vertex[index].PositionZ; }

                    if (Program.OBJ_Vertex[index].PositionX < MinX) { MinX = Program.OBJ_Vertex[index].PositionX; }
                    if (Program.OBJ_Vertex[index].PositionY < MinY) { MinY = Program.OBJ_Vertex[index].PositionY; }
                    if (Program.OBJ_Vertex[index].PositionZ < MinZ) { MinZ = Program.OBJ_Vertex[index].PositionZ; }
                }
            );
            */

            // Calculate maximums and minimums of all vertices.
            for (int x = 0; x < Program.OBJ_Vertex.Count; x++)
            {
                if (Program.OBJ_Vertex[x].PositionX > MaxX) { MaxX = Program.OBJ_Vertex[x].PositionX; }
                if (Program.OBJ_Vertex[x].PositionY > MaxY) { MaxY = Program.OBJ_Vertex[x].PositionY; }
                if (Program.OBJ_Vertex[x].PositionZ > MaxZ) { MaxZ = Program.OBJ_Vertex[x].PositionZ; }

                if (Program.OBJ_Vertex[x].PositionX < MinX) { MinX = Program.OBJ_Vertex[x].PositionX; }
                if (Program.OBJ_Vertex[x].PositionY < MinY) { MinY = Program.OBJ_Vertex[x].PositionY; }
                if (Program.OBJ_Vertex[x].PositionZ < MinZ) { MinZ = Program.OBJ_Vertex[x].PositionZ; }
            }

            // Calculate center
            Program.FileHeaderX.QuadTreeCenter = new float[3];
            Program.FileHeaderX.QuadTreeCenter[0] = Convert.ToSingle((MaxX + MinX) / 2.0);
            Program.FileHeaderX.QuadTreeCenter[1] = Convert.ToSingle((MaxY + MinY) / 2.0);
            Program.FileHeaderX.QuadTreeCenter[2] = Convert.ToSingle((MaxZ + MinZ) / 2.0);

            // Calculate Width & Height of Quad.
            Program.FileHeaderX.QuadTreeSize = MaxX - MinX; // Assume X is dominant.
            if (Program.FileHeaderX.QuadTreeSize < MaxZ - MinZ) { Program.FileHeaderX.QuadTreeSize = MaxZ - MinZ; } // But if it is not, set Z.

            // Set the base power of the header
            Program.FileHeaderX.BasePower = Program.OffsetPowerLevel;

            // This aligns the value of the size of the quadtree to be divisible by 1024, allowing for integers to be used for node generations up to 10 levels.
            // This is a great speedup to the intersection checking since it prevents floating point checks and lets the processor compare integers instead!

            if (Program.OptimizeIntersectionMaths == true)
            {
                uint RoundedToEight = Convert.ToUInt32((((int)Program.FileHeaderX.QuadTreeSize / Program.IntersectionOptimizationLevel) + 1) * Program.IntersectionOptimizationLevel);
                Program.FileHeaderX.QuadTreeSize = (float)RoundedToEight;
            }

            if (Program.OptimizeIntersectionMathsAlternate == true)
            {
                uint RoundedToEight = Convert.ToUInt32((((int)Program.FileHeaderX.QuadTreeSize / Program.OffsetPowerLevel)) * Program.OffsetPowerLevel);
                Program.FileHeaderX.QuadTreeSize = (float)RoundedToEight;
            }

            // Set triangle counts!
            Program.FileHeaderX.NumberOfTriangles = (ushort)Program.OBJ_TriangleList.Count;
            Program.FileHeaderX.NumberOfVertices = (ushort)Program.OBJ_Vertex.Count;
        }

        public static void ProcessAllTriangles()
        {
            // Ayy lmao
            Program.OBJ_CopyOfTriangleList = Program.OBJ_TriangleList.ToArray();

            // Multithreading is slower here, do not use.
            for (int x = 0; x < Program.OBJ_CopyOfTriangleList.Length - 1; x++)
            {
                Program.OBJ_CopyOfTriangleList[x] = CalculateTriangleNormalAndCentre(Program.OBJ_CopyOfTriangleList[x]);
            }

            // Finally something multithreaded!


            Parallel.For(0, Program.OBJ_TriangleList.Count, Program.ParallelZ,
            //for (int index = 0; index < Program.OBJ_TriangleList.Count; index++)
            index =>
                {
                    Program.OBJ_CopyOfTriangleList[index] = CalculateAdjacentTriangles(Program.OBJ_CopyOfTriangleList[index]); // Return triangle to list!
                }
            );

            /*
            for (int x = 0; x < Program.OBJ_TriangleList.Count - 1; x++)
            {
                CollisionStructs.CL_TriangleStruct TempTriangle = Program.OBJ_TriangleList[x]; // Get unique triangle.
                TempTriangle = CalculateAdjacentTriangles(TempTriangle); // Calculate and append normals & centres!
                Program.OBJ_TriangleList[x] = TempTriangle; // Return triangle to list!
            }*/
        }

        public static CollisionStructs.CL_TriangleStruct CalculateAdjacentTriangles(CollisionStructs.CL_TriangleStruct TempTriangleX)
        {
            // Set defaults, will be this value unless found.
            TempTriangleX.AdjacentTriangleIndexONE = 0xFFFF;
            TempTriangleX.AdjacentTriangleIndexTWO = 0xFFFF;
            TempTriangleX.AdjacentTriangleIndexTHREE = 0xFFFF;

            /*
            // Get list of triangles with a commmon vertex.                                                          
            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesOnes = Program.OBJ_TriangleList.FindAll(x => x.VertexOneID == TempTriangleX.VertexOneID).ToArray();
            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesOnes_Two = Program.OBJ_TriangleList.FindAll(x => x.VertexOneID == TempTriangleX.VertexTwoID).ToArray();
            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesOnes_Three = Program.OBJ_TriangleList.FindAll(x => x.VertexOneID == TempTriangleX.VertexThreeID).ToArray();

            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesTwos = Program.OBJ_TriangleList.FindAll(x => x.VertexTwoID == TempTriangleX.VertexTwoID).ToArray();
            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesTwos_One = Program.OBJ_TriangleList.FindAll(x => x.VertexTwoID == TempTriangleX.VertexOneID).ToArray();
            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesTwos_Three = Program.OBJ_TriangleList.FindAll(x => x.VertexTwoID == TempTriangleX.VertexThreeID).ToArray();

            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesThrees_One = Program.OBJ_TriangleList.FindAll(x => x.VertexThreeID == TempTriangleX.VertexOneID).ToArray();
            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesThrees = Program.OBJ_TriangleList.FindAll(x => x.VertexThreeID == TempTriangleX.VertexThreeID).ToArray();
            CollisionStructs.CL_TriangleStruct[] CommonVertexTrianglesThrees_Two = Program.OBJ_TriangleList.FindAll(x => x.VertexThreeID == TempTriangleX.VertexTwoID).ToArray();
            
            // Union All Arrays
            // Where the triangle is not this current triangle.
            CollisionStructs.CL_TriangleStruct[] TrianglesWithCommonVertices = CommonVertexTrianglesOnes.Union(CommonVertexTrianglesTwos).Union(CommonVertexTrianglesThrees)
                                                                               .Union(CommonVertexTrianglesOnes_Two).Union(CommonVertexTrianglesOnes_Three)
                                                                               .Union(CommonVertexTrianglesTwos_One).Union(CommonVertexTrianglesTwos_Three)
                                                                               .Union(CommonVertexTrianglesThrees_One).Union(CommonVertexTrianglesThrees_Two)
                                                                               .Where(x => x.ExtraProperties.TempTriangleIndex != TempTriangleX.ExtraProperties.TempTriangleIndex)
                                                                               .GroupBy(x=> x.ExtraProperties.TempTriangleIndex)
                                                                               .Select(g=> g.First())
                                                                               .ToArray();

            //List of adjacent triangles.
            List<CollisionStructs.CL_TriangleStruct> AdjacentTriangles = new List<CollisionStructs.CL_TriangleStruct>();

            // For each triangle with a common vertex.
            for (int x = 0; x < TrianglesWithCommonVertices.Length; x++)
            {
                byte NumberOfMatches = 0;

                // Time for a ridiculous if statement.
                if // If vertex One matches in any configuration 
                (
                    (
                        (TrianglesWithCommonVertices[x].VertexOneID == TempTriangleX.VertexOneID)
                        || (TrianglesWithCommonVertices[x].VertexOneID == TempTriangleX.VertexTwoID)
                        || (TrianglesWithCommonVertices[x].VertexOneID == TempTriangleX.VertexThreeID)
                    )
                ) { NumberOfMatches += 1; }
                if  // If vertex Two matches in any configuration 
                (
                    (
                        (TrianglesWithCommonVertices[x].VertexTwoID == TempTriangleX.VertexOneID)
                        || (TrianglesWithCommonVertices[x].VertexTwoID == TempTriangleX.VertexTwoID)
                        || (TrianglesWithCommonVertices[x].VertexTwoID == TempTriangleX.VertexThreeID)
                    )

                ) { NumberOfMatches += 1; }
                if // If vertex Three matches in any configuration 
                (
                    (
                        (TrianglesWithCommonVertices[x].VertexThreeID == TempTriangleX.VertexOneID)
                        || (TrianglesWithCommonVertices[x].VertexThreeID == TempTriangleX.VertexTwoID)
                        || (TrianglesWithCommonVertices[x].VertexThreeID == TempTriangleX.VertexThreeID)
                    )
                )
                { NumberOfMatches += 1; }

                // If two matching vertices exist by concatenating the boolean result, add to the list.
                if (NumberOfMatches >= 2) { AdjacentTriangles.Add(TrianglesWithCommonVertices[x]); }
            }

            switch (AdjacentTriangles.Count)
            {
                case 3:
                    TempTriangleX.AdjacentTriangleIndexONE = (ushort)AdjacentTriangles[0].ExtraProperties.TempTriangleIndex;
                    TempTriangleX.AdjacentTriangleIndexTWO = (ushort)AdjacentTriangles[1].ExtraProperties.TempTriangleIndex;
                    TempTriangleX.AdjacentTriangleIndexTHREE = (ushort)AdjacentTriangles[2].ExtraProperties.TempTriangleIndex;
                    break;
                case 2:
                    TempTriangleX.AdjacentTriangleIndexONE = (ushort)AdjacentTriangles[0].ExtraProperties.TempTriangleIndex;
                    TempTriangleX.AdjacentTriangleIndexTWO = (ushort)AdjacentTriangles[1].ExtraProperties.TempTriangleIndex;
                    break;
                case 1:
                    TempTriangleX.AdjacentTriangleIndexONE = (ushort)AdjacentTriangles[0].ExtraProperties.TempTriangleIndex;
                    break;
            }
            */

            return TempTriangleX;
        }

        // The following function/method averages the XYZ coordinates of Vectors 2 - 1 and Vectors 3 - 1
        // The cross product is then calculated.
        // It then calculates the normals by multiplying the positions.
        // Then we calculate the moduli of the array.
        // Divide the vector values by the moduli.
        public static CollisionStructs.CL_TriangleStruct CalculateTriangleNormalAndCentre(CollisionStructs.CL_TriangleStruct TempTriangleX)
        {
            float Vector1PositionX = Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionX - Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionX;
            float Vector1PositionY = Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionY - Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionY;
            float Vector1PositionZ = Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionZ - Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionZ;

            float Vector2PositionX = Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionX - Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionX;
            float Vector2PositionY = Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionY - Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionY;
            float Vector2PositionZ = Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionZ - Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionZ;

            float VectorNormalPositionX = (Vector1PositionY * Vector2PositionZ) - (Vector1PositionZ * Vector2PositionY); // (ad - bc)i 
            float VectorNormalPositionY = (Vector1PositionZ * Vector2PositionX) - (Vector1PositionX * Vector2PositionZ); // (ad - bc)j 
            float VectorNormalPositionZ = (Vector1PositionX * Vector2PositionY) - (Vector1PositionY * Vector2PositionX); // (ad - bc)k 

            // Note: Vector 2 and Vector 3 will be perpendicular, thus Θ = 1/2pi, sin(Θ) = 1

            float VectorModulus = (float)Math.Pow(Math.Pow(VectorNormalPositionX, 2.0) + Math.Pow(VectorNormalPositionY, 2.0) + Math.Pow(VectorNormalPositionZ, 2.0), 0.5);

            // TempTriangleX.NormalUnitVector.PositionX = 1F;
            // TempTriangleX.NormalUnitVector.PositionY = 1F;
            // TempTriangleX.NormalUnitVector.PositionZ = 1F;

            TempTriangleX.NormalUnitVector.PositionX = (float)(VectorNormalPositionX / VectorModulus);
            TempTriangleX.NormalUnitVector.PositionY = (float)(VectorNormalPositionY / VectorModulus);
            TempTriangleX.NormalUnitVector.PositionZ = (float)(VectorNormalPositionZ / VectorModulus);

            // Get Centre and Radius
            // TempTriangleX.ExtraProperties.TriangleCenterX = (Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionX + Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionX + Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionX) / 3;
            // TempTriangleX.ExtraProperties.TriangleCenterZ = (Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionZ + Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionZ + Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionZ) / 3;

            // Get min and max.
            float MaxX = Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionX;
            if (Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionX > MaxX) { MaxX = Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionX; }
            else if (Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionX > MaxX) { MaxX = Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionX; }

            float MinX = Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionX;
            if (Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionX < MinX) { MinX = Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionX; }
            else if (Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionX < MinX) { MinX = Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionX; }

            float MaxZ = Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionZ;
            if (Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionZ > MaxZ) { MaxZ = Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionZ; }
            else if (Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionZ > MaxZ) { MaxZ = Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionZ; }

            float MinZ = Program.OBJ_Vertex[TempTriangleX.VertexOneID].PositionZ;
            if (Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionZ < MinZ) { MinZ = Program.OBJ_Vertex[TempTriangleX.VertexTwoID].PositionZ; }
            else if (Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionZ < MinZ) { MinZ = Program.OBJ_Vertex[TempTriangleX.VertexThreeID].PositionZ; }

            // Set min and max.
            TempTriangleX.ExtraProperties.TriangleWidth = (MaxX - MinX);
            TempTriangleX.ExtraProperties.TriangleHeight = (MaxZ - MinZ);
            TempTriangleX.ExtraProperties.MinimumX = MinX;
            TempTriangleX.ExtraProperties.MinimumZ = MinZ;

            return TempTriangleX;
        }

        // This will generate a quadtree structure from scratch.
        public static void GenerateQuadtree()
        {
            InitializeRootNode(); // Generate the root node!
            GenerateChildNodes(); // Generate all of the child nodes down to the very bottom level.

        }

        // Generates all of the child nodes below of the parent node.
        public static void GenerateChildNodes()
        {
            // For each node down deep.
            // Depth level starts at 0, because the already generated root node is of depth 0;
            for (int DepthLevel = 0; DepthLevel < Program.NodeLevel; DepthLevel++)
            {
                // Foreach is not happy when it comes to performing loops which change during the loop's runtime, so we extract what we want here first.
                CollisionStructs.Node[] NodesInDepthLevel = Program.QuadNodes.Where(x => x.DepthLevel == DepthLevel).ToArray();

                // For each node where the level is set to the current node level investigated.
                // Basically iterating the depth levels to be generated and the depth levels in the list of nodes, will let us do actions to every node
                // at a certain depth level on iteration, which is how we generate the child nodes.
                foreach (CollisionStructs.Node IndividualNode in NodesInDepthLevel)
                {
                    GenerateIndividualChildNode(IndividualNode, 1, true); // Generate top left node.
                    GenerateIndividualChildNode(IndividualNode, 2, false); // Generate top right node
                    GenerateIndividualChildNode(IndividualNode, 3, false); // Generate bottom left node
                    GenerateIndividualChildNode(IndividualNode, 4, false); // Generate a bottom right node
                }
            }
        }

        /// Node Relative Positions
        /// 1 = Top Left
        /// 2 = Top Right
        /// 3 = Bottom Left
        /// 4 = Bottom Right
        /// Used in calculation of the LR and TB offsets!

        // IsFirstNode: If true, set child node of parent to this node index.

        public static void GenerateIndividualChildNode(CollisionStructs.Node ParentNode, byte NodeRelativePosition, bool IsFirstNode)
        {
            // New Node Item
            CollisionStructs.Node CurrentNode = new CollisionStructs.Node();

            // Get new size of node.
            CurrentNode.NodeParent = ParentNode.NodeIndex; // Set index
            CurrentNode.NodeChild = 0; // Children will be calculated later!
            CurrentNode.DepthLevel = (byte)(ParentNode.DepthLevel + 1); // Set depth level!

            CurrentNode.CalculationProperties.NodeSize = ParentNode.CalculationProperties.NodeSize / 2; // Get radius of node :)

            CurrentNode.PositioningOffsetValueLR = ParentNode.PositioningOffsetValueLR;
            CurrentNode.PositioningOffsetValueTB = ParentNode.PositioningOffsetValueTB;

            switch (NodeRelativePosition)
            {
                case 1:
                    // Limits of node!
                    CurrentNode.CalculationProperties.NodeMaximumX = ParentNode.CalculationProperties.NodeCenterX; // Node is 1/4 size in corner of original
                    CurrentNode.CalculationProperties.NodeMaximumZ = ParentNode.CalculationProperties.NodeCenterZ; // Node is 1/4 size in corner of original

                    CurrentNode.CalculationProperties.NodeMinimumX = ParentNode.CalculationProperties.NodeMinimumX; // Left Edge
                    CurrentNode.CalculationProperties.NodeMinimumZ = ParentNode.CalculationProperties.NodeMinimumZ; // Top Edge
                    break;
                case 2:
                    // This is additive to the parent node!
                    CurrentNode.PositioningOffsetValueLR += (ushort)Math.Pow(2, (Program.FileHeaderX.BasePower - CurrentNode.DepthLevel)); // Calculate horizontal positioning offset

                    // Limits of node!
                    CurrentNode.CalculationProperties.NodeMaximumX = ParentNode.CalculationProperties.NodeMaximumX; // Node is 1/4 size in corner of original
                    CurrentNode.CalculationProperties.NodeMaximumZ = ParentNode.CalculationProperties.NodeCenterZ; // Node is 1/4 size in corner of original

                    CurrentNode.CalculationProperties.NodeMinimumX = ParentNode.CalculationProperties.NodeCenterX; // Left Edge
                    CurrentNode.CalculationProperties.NodeMinimumZ = ParentNode.CalculationProperties.NodeMinimumZ; // Top Edge
                    break;
                case 3:
                    // This is additive to the parent node!
                    CurrentNode.PositioningOffsetValueTB += (ushort)Math.Pow(2, (Program.FileHeaderX.BasePower - CurrentNode.DepthLevel)); // Calculate vertical positioning offset

                    // Limits of node!
                    CurrentNode.CalculationProperties.NodeMaximumX = ParentNode.CalculationProperties.NodeCenterX; // Node is 1/4 size in corner of original
                    CurrentNode.CalculationProperties.NodeMaximumZ = ParentNode.CalculationProperties.NodeMaximumZ; // Node is 1/4 size in corner of original

                    CurrentNode.CalculationProperties.NodeMinimumX = ParentNode.CalculationProperties.NodeMinimumX; // Left Edge
                    CurrentNode.CalculationProperties.NodeMinimumZ = ParentNode.CalculationProperties.NodeCenterZ; // Top Edge
                    break;
                case 4:
                    // This is additive to the parent node!
                    CurrentNode.PositioningOffsetValueLR += (ushort)Math.Pow(2, (Program.FileHeaderX.BasePower - CurrentNode.DepthLevel)); // Calculate horizontal positioning offset
                    CurrentNode.PositioningOffsetValueTB += (ushort)Math.Pow(2, (Program.FileHeaderX.BasePower - CurrentNode.DepthLevel)); // Calculate vertical positioning offset

                    // Limits of node!
                    CurrentNode.CalculationProperties.NodeMaximumX = ParentNode.CalculationProperties.NodeMaximumX; // Node is 1/4 size in corner of original
                    CurrentNode.CalculationProperties.NodeMaximumZ = ParentNode.CalculationProperties.NodeMaximumZ; // Node is 1/4 size in corner of original

                    CurrentNode.CalculationProperties.NodeMinimumX = ParentNode.CalculationProperties.NodeCenterX; // Left Edge
                    CurrentNode.CalculationProperties.NodeMinimumZ = ParentNode.CalculationProperties.NodeCenterZ; // Top Edge
                    break;
            }

            // Get centers of node
            CurrentNode.CalculationProperties.NodeCenterX = (CurrentNode.CalculationProperties.NodeMaximumX + CurrentNode.CalculationProperties.NodeMinimumX) / 2;
            CurrentNode.CalculationProperties.NodeCenterZ = (CurrentNode.CalculationProperties.NodeMaximumZ + CurrentNode.CalculationProperties.NodeMinimumZ) / 2;
            CurrentNode.NodeExtraProperties.TriangleList = new List<ushort>(); // Aww yis. // 
            // Initialize list with 1/10 the triangle count of all of the triangles. Why?
            // Because when you add to a list and need to resize, the whole list needs to be discarded, we can speed the program up with a small approximation here.
            CurrentNode.CalculationProperties.NodeVerified = true; // Set verified flag to true

            CurrentNode.NodeIndex = (uint)(Program.QuadNodes.Count()); // The potential obstacle to multithreading! | No adding +1 because actual index is zero indexed, .Count() is 1 indexed.

            if (IsFirstNode)
            {
                CollisionStructs.Node TemporaryNode = Program.QuadNodes[(int)ParentNode.NodeIndex];
                TemporaryNode.NodeChild = CurrentNode.NodeIndex;
                Program.QuadNodes[(int)ParentNode.NodeIndex] = TemporaryNode;
            } // Set child node index to appropriate parent if first node.

            Program.QuadNodes.Add(CurrentNode);                    // Add node to complete list of verified nodes
        }

        // Node info
        // Root index : 0 (0 indexed)
        // Node parent : 0
        // Node child: 1
        // Node right neighbour: 0
        // Node left neighbour: 0
        // Node bottom neighbour: 0
        // Node top neighbour: 0
        // Node numberoftriangles: 0
        // Node offsettrianglelist: 0
        // Node positioningoffsetvaluelr: 0
        // Node positioningoffsetvaluetb: 0
        // Node depthlevel: 0

        // This creates an initial real starting node from scratch.
        public static void InitializeRootNode()
        {
            CollisionStructs.Node RootNode = new CollisionStructs.Node();
            RootNode.NodeIndex = 0;
            RootNode.NodeParent = 0;
            RootNode.NodeChild = 1;
            RootNode.RightNeighbourNode = 0;
            RootNode.LeftNeighbourNode = 0;
            RootNode.BottomNeighbourNode = 0;
            RootNode.TopNeighbourNode = 0;
            RootNode.NodeTriangleCount = 0;
            RootNode.OffsetToTriangleList = 0;
            RootNode.PositioningOffsetValueLR = 0;
            RootNode.PositioningOffsetValueTB = 0;
            RootNode.DepthLevel = 0;

            // Copy node centers
            RootNode.CalculationProperties.NodeCenterZ = Program.FileHeaderX.QuadTreeCenter[2];
            RootNode.CalculationProperties.NodeCenterX = Program.FileHeaderX.QuadTreeCenter[0];

            // Calculate sizing properties!
            RootNode.CalculationProperties.NodeSize = Program.FileHeaderX.QuadTreeSize;

            RootNode.CalculationProperties.NodeMinimumX = RootNode.CalculationProperties.NodeCenterX - (RootNode.CalculationProperties.NodeSize / 2);
            RootNode.CalculationProperties.NodeMinimumZ = RootNode.CalculationProperties.NodeCenterZ - (RootNode.CalculationProperties.NodeSize / 2);

            RootNode.CalculationProperties.NodeMaximumX = RootNode.CalculationProperties.NodeCenterX + (RootNode.CalculationProperties.NodeSize / 2);
            RootNode.CalculationProperties.NodeMaximumZ = RootNode.CalculationProperties.NodeCenterZ + (RootNode.CalculationProperties.NodeSize / 2);

            // Aww yes!
            RootNode.NodeExtraProperties.TriangleList = new List<ushort>(); // Program.OBJ_TriangleList.Count / 10
            // Initialize list with 1/10 the triangle count of all of the triangles. Why? This number seems reasonable...
            // Because when you add to a list and need to resize, the whole list needs to be discarded, we can speed the program up with a small approximation here.
            RootNode.CalculationProperties.NodeVerified = true;

            Program.QuadNodes.Add(RootNode);
        }


        // Struct used for storing triangle to node mappings for the multithreaded operation.
        public struct AddTriangleToNodeHelper
        {
            public uint NodeToAddTo;
            public uint TriangleToAdd;
        }

        public static CollisionStructs.Node[] BottomLevelNodes;

        // Calculates whether triangles intersect with individual nodes, highly multithreaded!
        public static void CalculateTriangleIntersects()
        {
            Program.CopyOfNodes = Program.QuadNodes.ToArray(); // Array allows me to change any embedded element, also faster to access etc.
            BottomLevelNodes = Program.QuadNodes.Where(x => x.DepthLevel == Program.NodeLevel).ToArray(); // Get nodes at the bottom level.



            // The intersection scalar intersects making overlaps in nodes such that triangles are found inbetween nodes.
            // This is so such the the player does not meet inevitable doom when going between nodes.
            // + 200% might be crazy, but with the current algorhithm you're not reaching level 10 anyway
            if (Program.NodeLevel > 7) { Program.IntersectScalar += (Program.NodeLevel - 7)*200.0F; }

            for (int TriangleIndex = 0; TriangleIndex < Program.OBJ_CopyOfTriangleList.Length; TriangleIndex++)
            {
                // Get area of individual triangle, but as a square for faster calculation!
                RectangleF TriangleAreaRectangle = new RectangleF(Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MinimumX, Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MinimumZ, Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.TriangleWidth, Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.TriangleHeight);

                //Console.WriteLine("Size Of Triangle| H: " + SizeOfTriangle.Height + " W: " + SizeOfTriangle.Width);
                /*Console.WriteLine
                    (
                    "Coordinates of Triangle | Max X: " + 
                    Convert.ToString(Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MaximumX) + 
                    " Min X | " + 
                    Convert.ToString(Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MinimumX)
                    );
                */

               Parallel.For(0, BottomLevelNodes.Length, Program.ParallelZ,
               //for (int z = 0; z < BottomLevelNodes.Length; z++)
                  z =>
                    {
                        // This is the increase in node size due to the intersectscalar
                        float NodeIntersectPositionOffset = (float)(((BottomLevelNodes[z].CalculationProperties.NodeSize / 100.0) * Program.IntersectScalar) - BottomLevelNodes[z].CalculationProperties.NodeSize) / 2;
                        // Get current node as floating point rectangle.
                        RectangleF CurrentNode = new RectangleF(BottomLevelNodes[z].CalculationProperties.NodeMinimumX - NodeIntersectPositionOffset, BottomLevelNodes[z].CalculationProperties.NodeMinimumZ - NodeIntersectPositionOffset, (float)((BottomLevelNodes[z].CalculationProperties.NodeSize/100.0)* Program.IntersectScalar), (float)((BottomLevelNodes[z].CalculationProperties.NodeSize / 100.0)* Program.IntersectScalar));
                        // Console.WriteLine("Minimum X: " + (int)BottomLevelNodes[z].CalculationProperties.NodeMinimumX + " Minimum Z: " + (int)BottomLevelNodes[z].CalculationProperties.NodeMinimumZ);
                        // Use the .NET Builtin for determine if intersection exists, add index of triangle to list, though it may be a better idea to potentially make own for performance.
                        // We are adding tirangles to nodes :)
                        if (TriangleAreaRectangle.IntersectsWith(CurrentNode))
                        {
                            // For the index of the current bottom level node in CopyOfNodes, add to triangle list => Added triangle is the one we are comparing, i.e. TriangleList_TriangleIndex
                            Program.CopyOfNodes[BottomLevelNodes[z].NodeIndex].NodeExtraProperties.TriangleList.Add(Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.TempTriangleIndex);
                        }
                    }
               );


            }
        /*
         // Iterate over all of the bottom level nodes!
         for (int z = 0; z < BottomLevelNodes.Length; z++)
         {
         // Get area fpr mpce/
         Rectangle CurrentNodeArea = new Rectangle((int)BottomLevelNodes[z].CalculationProperties.NodeMinimumX, (int)BottomLevelNodes[z].CalculationProperties.NodeMinimumZ, (int)BottomLevelNodes[z].CalculationProperties.NodeSize, (int)BottomLevelNodes[z].CalculationProperties.NodeSize);
         // Use the .NET Builtin for determine if intersection exists, add index of triangle to list, though it may be a better idea to potentially make own for performance.
         // We are adding tirangles to nodes :)
         if (TriangleAreaRectangle.IntersectsWith(CurrentNodeArea))
         {
         // For the index of the current bottom level node in CopyOfNodes, add to triangle list => Added triangle is the one we are comparing, i.e. TriangleList_TriangleIndex
         Program.CopyOfNodes[BottomLevelNodes[z].NodeIndex].NodeExtraProperties.TriangleList.Add(Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.TempTriangleIndex);
         }
        */

        // Multithreading Options
        // ParallelOptions ParallelZ = new ParallelOptions(); // Create a new configuration for parallelism.
        // ParallelZ.MaxDegreeOfParallelism = Environment.ProcessorCount; // Run parallel for across the count of threads on the CPU.

        // Iterate over each triangle (Multithreaded - Race Condition??)
        /*
        Parallel.For(0, Program.OBJ_TriangleList.Count, ParallelZ,
            TriangleIndex =>
                {
                    // Get area of individual triangle, but as a square for faster calculation!
                    Rectangle TriangleAreaRectangle = new Rectangle((int)Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumX, (int)Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumZ, (int)(Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MaximumX - Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumX), (int)(Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MaximumZ - Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumZ));

                    // Iterate over all of the bottom level nodes!
                    for (int z = 0; z < BottomLevelNodes.Length; z++)
                    {
                        // Get area fpr mpce/
                        Rectangle CurrentNodeArea = new Rectangle((int)BottomLevelNodes[z].CalculationProperties.NodeMinimumX, (int)BottomLevelNodes[z].CalculationProperties.NodeMinimumZ, (int)BottomLevelNodes[z].CalculationProperties.NodeSize, (int)BottomLevelNodes[z].CalculationProperties.NodeSize);
                        // Use the .NET Builtin for determine if intersection exists, add index of triangle to list, though it may be a better idea to potentially make own for performance.
                        // We are adding tirangles to nodes :)
                        if (TriangleAreaRectangle.IntersectsWith(CurrentNodeArea))
                        {
                            // For the index of the current bottom level node in CopyOfNodes, add to triangle list => Added triangle is the one we are comparing, i.e. TriangleList_TriangleIndex
                            Program.CopyOfNodes[BottomLevelNodes[z].NodeIndex].NodeExtraProperties.TriangleList.Add(Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.TempTriangleIndex);
                        }
                    }
                }
        );
        */

        // Multithreaded (Enhanced)
        // Executes a for loop, executing every alternate item on a new thread.
        // E.g. 2 threaded.
        // Thread 1 executes 1,3,5,7 (2n - 1)th
        // This is an optimization :)

        /*
        byte AmountOfAlternateIterationsPerThread = (byte)Environment.ProcessorCount; 
        if (AmountOfAlternateIterationsPerThread > 16) { AmountOfAlternateIterationsPerThread = 16; }

        List<Thread> ParallelThreads = new List<Thread>(AmountOfAlternateIterationsPerThread);
        for (int x = 0; x < AmountOfAlternateIterationsPerThread - 1; x++)
        {
            Thread WorkerThread = new Thread
            (
                () => ProcessNETAddition(x, AmountOfAlternateIterationsPerThread)
            );
            WorkerThread.Start();
            ParallelThreads.Add(WorkerThread);
        }

        // Wait for le threads.
        foreach (Thread thread in ParallelThreads)
        {
            thread.Join();
        }
        */

        /*
        Parallel.For
        (0, AmountOfAlternateIterationsPerThread,

            WorkerID =>
            {                                    
                int MaximumIteration = Program.OBJ_TriangleList.Count * (WorkerID + 1) / AmountOfAlternateIterationsPerThread;
                List<AddTriangleToNodeHelper> TrianglesToAdd = new List<AddTriangleToNodeHelper>();

                for (int TriangleIndex = Program.OBJ_TriangleList.Count * WorkerID / AmountOfAlternateIterationsPerThread; TriangleIndex < MaximumIteration; TriangleIndex++)
                {
                    // Get area of individual triangle, but as a square for faster calculation!
                    Rectangle TriangleAreaRectangle = new Rectangle((int)Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumX, (int)Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumZ, (int)(Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MaximumX - Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumX), (int)(Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MaximumZ - Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.MinimumZ));

                    // Iterate over all of the bottom level nodes!
                    for (int z = 0; z < BottomLevelNodes.Length; z++)
                    {
                        // Get area fpr mpce/
                        Rectangle CurrentNodeArea = new Rectangle((int)BottomLevelNodes[z].CalculationProperties.NodeMinimumX, (int)BottomLevelNodes[z].CalculationProperties.NodeMinimumZ, (int)BottomLevelNodes[z].CalculationProperties.NodeSize, (int)BottomLevelNodes[z].CalculationProperties.NodeSize);
                        // Use the .NET Builtin for determine if intersection exists, add index of triangle to list, though it may be a better idea to potentially make own for performance.
                        // We are adding tirangles to nodes :)
                        if (TriangleAreaRectangle.IntersectsWith(CurrentNodeArea))
                        {
                            AddTriangleToNodeHelper Helper; Helper.NodeToAddTo = BottomLevelNodes[z].NodeIndex; Helper.TriangleToAdd = Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.TempTriangleIndex;
                            TrianglesToAdd.Add(Helper);
                        }
                    }
                }
            }
        );
        */
    }

    /*
    public static void AttachTriangleToNode(int NodeID, ushort TriangleID)
    {
        Program.CopyOfNodes[NodeID].NodeExtraProperties.TriangleList.Add(TriangleID);
    }

    public static void ProcessNETAddition(int WorkerID, byte AmountOfAlternateIterationsPerThread)
    {
        int MaximumIteration = Program.OBJ_CopyOfTriangleList.Length * (WorkerID + 1) / AmountOfAlternateIterationsPerThread;
        List<AddTriangleToNodeHelper> TrianglesToAdd = new List<AddTriangleToNodeHelper>();

        for (int TriangleIndex = Program.OBJ_CopyOfTriangleList.Length * WorkerID / AmountOfAlternateIterationsPerThread; TriangleIndex < MaximumIteration; TriangleIndex++)
        {
            // Get area of individual triangle, but as a square for faster calculation!
            Rectangle TriangleAreaRectangle = new Rectangle((int)Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MinimumX, (int)Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MinimumZ, (int)(Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MaximumX - Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MinimumX), (int)(Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MaximumZ - Program.OBJ_CopyOfTriangleList[TriangleIndex].ExtraProperties.MinimumZ));

            // Iterate over all of the bottom level nodes!
            for (int z = 0; z < BottomLevelNodes.Length; z++)
            {
                // Get area fpr mpce/
                Rectangle CurrentNodeArea = new Rectangle((int)BottomLevelNodes[z].CalculationProperties.NodeMinimumX, (int)BottomLevelNodes[z].CalculationProperties.NodeMinimumZ, (int)BottomLevelNodes[z].CalculationProperties.NodeSize, (int)BottomLevelNodes[z].CalculationProperties.NodeSize);
                // Use the .NET Builtin for determine if intersection exists, add index of triangle to list, though it may be a better idea to potentially make own for performance.
                // We are adding tirangles to nodes :)
                if (TriangleAreaRectangle.IntersectsWith(CurrentNodeArea))
                {
                    //AddTriangleToNodeHelper Helper; Helper.NodeToAddTo = BottomLevelNodes[z].NodeIndex; Helper.TriangleToAdd = Program.OBJ_TriangleList[TriangleIndex].ExtraProperties.TempTriangleIndex;
                    //TrianglesToAdd.Add(Helper);
                }
            }
        }
    }
    */

    //static List<int> EmptyNodeList = new List<int>(); // Create list of empty nodes.
    //static List<int> ParentRemoveIndexList = new List<int>(); // Parents To Remove Index From

    /// Finds child nodes of a parent node and checks if they have any triangles, if all child nodes have no triangles, they are nuked.
    public static void FindEmptyNodes()
        {
            // Reverse for loop? Kansei dorifto? Been forever since I've needed one!
            FindEmptyChildNodesAndNukeThemXD(Program.NodeLevel - 1); // This method wants parents as it looks for the children of each element, thus the -1.

            // Rewrites the entire list minus those which are 'unverified', being the ones I found to be empty.
            // This is to prevent a memory bottleneck!

            List<CollisionStructs.Node> NewNodes = new List<CollisionStructs.Node>(Program.CopyOfNodes.Length);
            // List<CollisionStructs.Node> AllDepthNodes = Program.CopyOfNodes.Where(z=> z.DepthLevel == Program.NodeLevel - 1).ToList(); // DEBUG 
            // List<CollisionStructs.Node> LeftOverParentNodes = AllDepthNodes.Where(z => z.NodeChild != 0).ToList(); // DEBUG
            // List<CollisionStructs.Node> DebugTest = Program.CopyOfNodes.ToList(); // DEBUG 
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                // Add offset here in the 'else' statement to add +4 to offset.
                if (Program.CopyOfNodes[x].CalculationProperties.NodeVerified)
                {
                    CollisionStructs.Node NewNode = new CollisionStructs.Node();
                    NewNode.BottomNeighbourNode = Program.CopyOfNodes[x].BottomNeighbourNode;
                    NewNode.CalculationProperties = Program.CopyOfNodes[x].CalculationProperties;
                    NewNode.NodeExtraProperties = Program.CopyOfNodes[x].NodeExtraProperties;
                    NewNode.DepthLevel = Program.CopyOfNodes[x].DepthLevel;
                    NewNode.LeftNeighbourNode = Program.CopyOfNodes[x].LeftNeighbourNode;
                    NewNode.NodeChild = Program.CopyOfNodes[x].NodeChild;
                    NewNode.NodeExtraProperties = Program.CopyOfNodes[x].NodeExtraProperties;
                    NewNode.NodeIndex = Program.CopyOfNodes[x].NodeIndex;
                    NewNode.NodeParent = Program.CopyOfNodes[x].NodeParent;
                    NewNode.PositioningOffsetValueLR = Program.CopyOfNodes[x].PositioningOffsetValueLR;
                    NewNode.PositioningOffsetValueTB = Program.CopyOfNodes[x].PositioningOffsetValueTB;
                    NewNode.RightNeighbourNode = Program.CopyOfNodes[x].RightNeighbourNode;
                    NewNode.TopNeighbourNode = Program.CopyOfNodes[x].TopNeighbourNode;
                    NewNodes.Add(NewNode);
                }
            }
            Program.CopyOfNodes = NewNodes.ToArray(); // Reassign nodos!

            // Iterate over all depth levels but depth level 0.
            // Starts at Program.NodeLevel - 1 because we have already quickly checked the bottom most node level.
            // Ends at x > 1 (Before Depth Level 1) because method checks parent, thus the parent node of given x, so tops at level 1, not 2.
            for (int x = Program.NodeLevel - 1; x > 1; x--)
            {
                // Get all of the nodes at specified depth level.
                CollisionStructs.Node[] AllDepthLevelNodes = Program.QuadNodes.Where( z=> z.DepthLevel == x ).ToArray();

                // Array for storing node indexes of parents.
                uint[] ParentNodeIndex = new uint[AllDepthLevelNodes.Length];

                // Multithreading too expensive, creating threads costs time!
                for (int index = 0; index < AllDepthLevelNodes.Length; index++)  // Copy all parents to ParentNodeIndex
                { ParentNodeIndex[index] = AllDepthLevelNodes[index].NodeParent; }
                ParentNodeIndex = ParentNodeIndex.Distinct().ToArray(); // Get unique parents.

                // HUGE NOTE, SINCE WE REMOVED NODES ALREADY THE CHILDREN'S INDEX IN ARRAY WILL NOT BE ACCURATE.
                // DO NOT CHANGE CHILD INDEX, CHANGE AFTER ALL EMPTY NODES ARE REMOVED, USE CHILD INDEX TO CONNECT PARENTS AND CHILDREN
                // PARENT INDEX: Program.QuadNodes.NodeIndex
                // CHILD INDEX IN PARENT: Program.QuadNodes.NodeChild
                // FIND CHILD OF PARENT BY LOOKING FOR .NodeIndex of CHILD, which should equal NodeChild.
                // DO NOT FIX INDEXES UNTIL ALL EMPTY NODES ARE NUKED!

                // EXTRA NOTE: Child index relations will be kept, since we only have nuked all empties in a layer lower.
                // AS GREATER DEPTHS =>> LOWER INTO QUAD ARRAY, ALL INDEXES OF PARENTS ARE UNAFFECTED AS THEIR LAYER WAS UNTOUCHED, ONLY ITEMS AFTER THEM WERE REMOVED!

                // Array for storing node indexes of children.
                uint[] ChildNodeIndex = new uint[ParentNodeIndex.Length];

                // Copy all children index from parents. 
                // These should also be unaffected, only layer lower was touched.
                for (int index = 0; index < ParentNodeIndex.Length; index++)
                { ChildNodeIndex[index] = Program.QuadNodes[(int)ParentNodeIndex[index]].NodeChild; }

                // ChildNodeIndex represents the first index of four nodes which act as subnodes to a parent.

                // Now verify if the group of four nodes of children are empty, if they are, nuke them, same as we processed the bottom most node.
                // Go over all child nodes first!
                for (int index = 0; index < ChildNodeIndex.Length; index++)
                {
                    bool ShallWeNukeChildren = CheckAllChildNodesChildIndex((int)ChildNodeIndex[index]); // Decide if I shall go boom boom!
                    if (ShallWeNukeChildren)
                    {
                        // Set verification status of all nodes to false such as that they can be wiped.
                        Program.CopyOfNodes[(int)ChildNodeIndex[index]].CalculationProperties.NodeVerified = false;
                        Program.CopyOfNodes[(int)ChildNodeIndex[index] + 1].CalculationProperties.NodeVerified = false;
                        Program.CopyOfNodes[(int)ChildNodeIndex[index] + 2].CalculationProperties.NodeVerified = false;
                        Program.CopyOfNodes[(int)ChildNodeIndex[index] + 3].CalculationProperties.NodeVerified = false;
                    }
                    else { }
                }

                // Now rewrite the array to remove the unverified children (i.e. nodes to remove).
                RewriteUnverifiedList();

                // Console.WriteLine("All Depth Level Nodes: " + AllDepthLevelNodes.Length + " | NodeLevel: " + x + " | All Parent Nodes: " + ParentNodeIndex.Length);
            }

            // Unverify all of the nodes!
            // We will need to reuse this flag for the following action about to happen.
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                Program.CopyOfNodes[x].CalculationProperties.NodeVerified = false;
            }


            // This time we are going to fix indexes which are bound within each individual node.
            // Node.NodeIndex is no longer valid, it will not updated at this point but will be used in order to match children and parents once again, the array position will be the true node index.
            // The parents' new correct NodeIndex will be their index inside the array simply from now on.
            // What we want to do now is get all of the node 'Parents' from the children. 
            // | Using these Parents' old NodeChildren, and the old also now invalid NodeIndex of the children, we will rewrite the NodeParent property of the children. || 
            // Our struct will hold the parent, children mapping, if a child is already mapped, do not map a child. (Given our iteration from start, this will ensure that we always will map the first child)

            // Let x be the current index.
            // Invalid (Needs fixed): NodeParent
            // Invalid (Needs fixed): NodeChild
            // Valid (No fix needed): Array index

            // Iterate the node array.
            // Program.PeepingTom(); //. DEBUG
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                uint ParentNodeIndex = Program.CopyOfNodes[x].NodeIndex; // Get parent's nodeindex.
                Program.PeepingTom();
                bool NodeHasChild = false;

                // Iterate over nodes again until we find the child of the parent.
                // Start at the parent's array index!
                for (int z = x; z < Program.CopyOfNodes.Length; z++)
                {
                    // If the parent node was originally assigned to the node we are currently investigating.
                    // And if the node was not already verified, i.e. has not been altered here...
                    if ((Program.CopyOfNodes[z].NodeParent == ParentNodeIndex) && Program.CopyOfNodes[z].CalculationProperties.NodeVerified == false)
                    {
                        Program.CopyOfNodes[x].NodeChild = (uint)z; // Set the child node of the parent node to the rediscovered first child.
                        Program.CopyOfNodes[z].NodeParent = (uint)x; // Set the parent node of child 1 accordingly.
                        Program.CopyOfNodes[z + 1].NodeParent = (uint)x; // Set the parent node of child 2 accordingly.
                        Program.CopyOfNodes[z + 2].NodeParent = (uint)x; // Set the parent node of child 3 accordingly.
                        Program.CopyOfNodes[z + 3].NodeParent = (uint)x; // Set the parent node of child 4 accordingly.

                        Program.CopyOfNodes[z].CalculationProperties.NodeVerified = true; // Verify Child Node 1
                        Program.CopyOfNodes[z + 1].CalculationProperties.NodeVerified = true; // Verify Child Node 2
                        Program.CopyOfNodes[z + 2].CalculationProperties.NodeVerified = true; // Verify Child Node 3
                        Program.CopyOfNodes[z + 3].CalculationProperties.NodeVerified = true; // Verify Child Node 4
                        NodeHasChild = true;
                        // Program.CopyOfNodes[x].CalculationProperties.NodeVerified = true; // Verify the parent node, though it's only the children || Performance: This is no longer necessary

                        break; // Break out of the 'for loop', go to next parent node to fix, we found what we wanted :)
                    }
                    else { }
                }
                // If no child found, I bid you adieu!
                if (NodeHasChild == false) { Program.CopyOfNodes[x].NodeChild = 0; }
            }

            // Forgive what I said above, we will need NodeIndex after all.
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                Program.CopyOfNodes[x].NodeIndex = (uint)x;
            }

            // Hacky: But this method will strip the child node from the first node, we need to restore it.
            // TODO: Maybe make it less hacky

            Program.CopyOfNodes[0].NodeChild = 1;

            // Debug
            /*
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                if (Program.CopyOfNodes[x].NodeChild > Program.CopyOfNodes.Length)
                {
                    Console.WriteLine("Node without child: " + Program.CopyOfNodes[x].NodeIndex);
                }
            }
            */
        }

        // True = Nuke the nodes!
        // False = Trump CNN Fake News
        public static bool CheckAllChildNodesChildIndex(int FirstChildIndex)
        {
            if
            (
                    NodeHasNoChildren(Program.CopyOfNodes[(int)FirstChildIndex]) &&
                    NodeHasNoChildren(Program.CopyOfNodes[(int)FirstChildIndex + 1]) &&
                    NodeHasNoChildren(Program.CopyOfNodes[(int)FirstChildIndex + 2]) &&
                    NodeHasNoChildren(Program.CopyOfNodes[(int)FirstChildIndex + 3])
            )
            {
                return true; // If all nodes have no index then we shall nuke children :)
            }
            else { return false; } // But if a single node has a child then we cannot nuke :(
        }

        // Verify if a node has a child.
        public static bool NodeHasNoChildren(CollisionStructs.Node PassedNode)
        {
            if (PassedNode.NodeChild == 0) { return true; }
            else { return false; }
        }

        public static void RewriteUnverifiedList()
        {
            // Rewrites the entire list minus those which are 'unverified', being the ones I found to be empty.
            // This is to prevent a memory bottleneck!

            List<CollisionStructs.Node> NewNodes = new List<CollisionStructs.Node>(Program.CopyOfNodes.Length);
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                // Add offset here in the 'else' statement to add +4 to offset.
                if (Program.CopyOfNodes[x].CalculationProperties.NodeVerified)
                {
                    CollisionStructs.Node NewNode = new CollisionStructs.Node();
                    NewNode.BottomNeighbourNode = Program.CopyOfNodes[x].BottomNeighbourNode;
                    NewNode.CalculationProperties = Program.CopyOfNodes[x].CalculationProperties;
                    NewNode.NodeExtraProperties = Program.CopyOfNodes[x].NodeExtraProperties;
                    NewNode.DepthLevel = Program.CopyOfNodes[x].DepthLevel;
                    NewNode.LeftNeighbourNode = Program.CopyOfNodes[x].LeftNeighbourNode;
                    NewNode.NodeChild = Program.CopyOfNodes[x].NodeChild;
                    NewNode.NodeExtraProperties = Program.CopyOfNodes[x].NodeExtraProperties;
                    NewNode.NodeIndex = Program.CopyOfNodes[x].NodeIndex;
                    NewNode.NodeParent = Program.CopyOfNodes[x].NodeParent;
                    NewNode.PositioningOffsetValueLR = Program.CopyOfNodes[x].PositioningOffsetValueLR;
                    NewNode.PositioningOffsetValueTB = Program.CopyOfNodes[x].PositioningOffsetValueTB;
                    NewNode.RightNeighbourNode = Program.CopyOfNodes[x].RightNeighbourNode;
                    NewNode.TopNeighbourNode = Program.CopyOfNodes[x].TopNeighbourNode;
                    NewNodes.Add(NewNode);
                }
            }
            Program.CopyOfNodes = NewNodes.ToArray(); // Reassign nodos!
        }

        // Nodelevel should be PARENT of the nodes we are investigating!
        public static void FindEmptyChildNodesAndNukeThemXD(int NodeLevel)
        {
            // All nodes at specified depth level.
            CollisionStructs.Node[] AllNodesAtDepthLevel = Program.CopyOfNodes.Where(x => x.DepthLevel == NodeLevel).ToArray();
            // CollisionStructs.Node[] CopyOfNodeStruct = Program.CopyOfNodes; // DEBUG
            // uint TempDebug = 0; // DEBUG

            // For all of the nodes. (SingleThreaded) || Multithreaded was removed because it was not thread safe. Extreme slim chance of error.
            for (int x = 0; x < AllNodesAtDepthLevel.Length; x++)
            {
                uint ChildStartIndex = AllNodesAtDepthLevel[x].NodeChild; // Child node #1 of selected node.
                // uint ParentIndex = AllNodesAtDepthLevel[x].NodeIndex; // DEBUG
                // The other nodes which have this parent as node are at Child Node #1 Index +1, +2, +3.
                if
                (
                    NodeHasTriangles(Program.CopyOfNodes[(int)ChildStartIndex]) ||
                    NodeHasTriangles(Program.CopyOfNodes[(int)ChildStartIndex + 1]) ||
                    NodeHasTriangles(Program.CopyOfNodes[(int)ChildStartIndex + 2]) ||
                    NodeHasTriangles(Program.CopyOfNodes[(int)ChildStartIndex + 3])
                )
                {
                    // TempDebug += 4; // DEBUG 
                } // Do nothing if any of the children have triangles.
                else
                {
                    /*EmptyNodeList.Add(BaseIndex);
                    EmptyNodeList.Add(BaseIndex + 1);
                    EmptyNodeList.Add(BaseIndex + 2);
                    EmptyNodeList.Add(BaseIndex + 3);
                    ParentRemoveIndexList.Add(AllNodesAtDepthLevel[x].NodeIndex);
                    */

                    // Remove child nodes if all empty
                    //Program.QuadNodes.RemoveAt((int)BaseIndex);
                    //Program.QuadNodes.RemoveAt((int)BaseIndex +1);
                    //Program.QuadNodes.RemoveAt((int)BaseIndex +2);
                    //Program.QuadNodes.RemoveAt((int)BaseIndex +3);

                    // See above for the call to this method
                    Program.CopyOfNodes[ChildStartIndex].CalculationProperties.NodeVerified = false;
                    Program.CopyOfNodes[ChildStartIndex + 1].CalculationProperties.NodeVerified = false;
                    Program.CopyOfNodes[ChildStartIndex + 2].CalculationProperties.NodeVerified = false;
                    Program.CopyOfNodes[ChildStartIndex + 3].CalculationProperties.NodeVerified = false;

                    // Set child of node without children with triangles to 0.
                    uint Index = AllNodesAtDepthLevel[x].NodeIndex;
                    Program.CopyOfNodes[Index].NodeChild = 0;
                    // Console.WriteLine("Child Removed, Index: " + Index);
                    //CollisionStructs.Node TemporaryNode = new CollisionStructs.Node(); TemporaryNode = Program.QuadNodes[(int)Index];
                    //TemporaryNode.NodeChild = 0; Program.QuadNodes[(int)Index] = TemporaryNode;
                }
            }

            // Console.WriteLine("Kept nodes at Base Level: " + TempDebug); // DEBUG 
        }

        // Simply checks whether there are any triangles assigned to a node.
        public static bool NodeHasTriangles(CollisionStructs.Node PassedNode)
        {
            if (PassedNode.NodeExtraProperties.TriangleList.Count > 0) { return true; }
            else { return false; }
        }

        // Validates if there are any neighbours for all nodes.
        public static void FindNeighbours()
        {
            // FindNeighboursSafe();
            FindNeighboursMultithreaded();
           // FindNeighboursAlternative(); // NOT TESTED
        }

        public static void FindNeighboursAlternative()
        {
            // Igor's solution in C#
            for (int QuadNode = 0; QuadNode < Program.CopyOfNodes.Length; QuadNode++)
            {
                // Obtain our quadnode!
                CollisionStructs.Node TemporaryNode = Program.CopyOfNodes[QuadNode];

                // For each depth level!
                for (int DepthLevel = 0; DepthLevel < Program.NodeLevel; DepthLevel++)
                {
                    if (TemporaryNode.RightNeighbourNode == 0) // Right will cover left of another node
                    {
                        for (int Index = 0; Index < Program.CopyOfNodes.Length - 1; Index++)
                        {
                            if ( (TemporaryNode.NodeChild != Program.CopyOfNodes[Index].NodeIndex) && (TemporaryNode.NodeChild + 1 != Program.CopyOfNodes[Index].NodeIndex) && (TemporaryNode.NodeChild + 2 != Program.CopyOfNodes[Index].NodeIndex) && (TemporaryNode.NodeChild + 3 != Program.CopyOfNodes[Index].NodeIndex))
                            {
                                if ((Program.CopyOfNodes[Index].DepthLevel - 1 == TemporaryNode.DepthLevel) && (Program.CopyOfNodes[Index].PositioningOffsetValueTB == TemporaryNode.PositioningOffsetValueTB))
                                {
                                    if ( Math.Pow( (TemporaryNode.PositioningOffsetValueLR + 2.0F), (float)(Program.OffsetPowerLevel - TemporaryNode.DepthLevel - 1) ) == Program.CopyOfNodes[Index].PositioningOffsetValueLR )
                                    {
                                        TemporaryNode.RightNeighbourNode = (ushort)Program.CopyOfNodes[Index].NodeIndex;
                                        Program.CopyOfNodes[Index].LeftNeighbourNode = (ushort)TemporaryNode.NodeIndex;
                                    }
                                }
                            }
                        }
                    }
                    
                    if (TemporaryNode.BottomNeighbourNode == 0) // Right will cover top of another node
                    {
                        for (int Index = 0; Index < Program.CopyOfNodes.Length - 1; Index++)
                        {
                            if ((TemporaryNode.NodeChild != Program.CopyOfNodes[Index].NodeIndex) && (TemporaryNode.NodeChild + 1 != Program.CopyOfNodes[Index].NodeIndex) && (TemporaryNode.NodeChild + 2 != Program.CopyOfNodes[Index].NodeIndex) && (TemporaryNode.NodeChild + 3 != Program.CopyOfNodes[Index].NodeIndex))
                            {


                                if ((Program.CopyOfNodes[Index].DepthLevel - 1 == TemporaryNode.DepthLevel) && (Program.CopyOfNodes[Index].PositioningOffsetValueLR == TemporaryNode.PositioningOffsetValueLR))
                                {

                                    if (Math.Pow((TemporaryNode.PositioningOffsetValueTB + 2.0F), (float)(Program.OffsetPowerLevel - TemporaryNode.DepthLevel - 1)) == Program.CopyOfNodes[Index].PositioningOffsetValueTB)
                                    {

                                        TemporaryNode.BottomNeighbourNode = (ushort)Program.CopyOfNodes[Index].NodeIndex;
                                        Program.CopyOfNodes[Index].TopNeighbourNode = (ushort)TemporaryNode.NodeIndex;

                                    }


                                }


                            }
                        }
                    }
                }
            }
        }

        public static void FindNeighboursSafe()
        {
            // Use each core instead of each thread.
            Program.ParallelZ.MaxDegreeOfParallelism = Program.ParallelZ.MaxDegreeOfParallelism / 2;

            // Iterate over all nodelevels.
            // Since each nodelevel is on a separate thread, race conditions will not be met, as places will not be written at once to same memory offsets, stuff added to lists etc.
            // Parallel.For(0, Program.NodeLevel, Program.ParallelZ, x=>
            for (int x = 0; x < Program.NodeLevel + 1; x++)
            {
                // Get all of the nodes at the particular level investigated.
                CollisionStructs.Node[] AllNodesAtLevel = Program.CopyOfNodes.Where(z => z.DepthLevel == x).ToArray();

                // For each of the nodes in the level
                for (int z = 0; z < AllNodesAtLevel.Length; z++)
                {
                    RectangleF CurrentNode;
                    ushort NeighbourNode;

                    // This top left corner is offset by -2, when intersecting 
                    CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX - 2, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ, AllNodesAtLevel[z].CalculationProperties.NodeSize, AllNodesAtLevel[z].CalculationProperties.NodeSize);
                    NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                    Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].LeftNeighbourNode = NeighbourNode;

                    CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ - 2, AllNodesAtLevel[z].CalculationProperties.NodeSize, AllNodesAtLevel[z].CalculationProperties.NodeSize);
                    NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                    Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].TopNeighbourNode = NeighbourNode;

                    CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX + 2, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ, AllNodesAtLevel[z].CalculationProperties.NodeSize, AllNodesAtLevel[z].CalculationProperties.NodeSize);
                    NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                    Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].RightNeighbourNode = NeighbourNode;

                    CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ + 2, AllNodesAtLevel[z].CalculationProperties.NodeSize, AllNodesAtLevel[z].CalculationProperties.NodeSize);
                    NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                    Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].BottomNeighbourNode = NeighbourNode;
                }

            }
            // );
        }

        public static void FindNeighboursMultithreaded()
        {
            // Use each core instead of each thread.
            Program.ParallelZ.MaxDegreeOfParallelism = Program.ParallelZ.MaxDegreeOfParallelism / 2;

            // Iterate over all nodelevels.
            // Since each nodelevel is on a separate thread, race conditions will not be met, as places will not be written at once to same memory offsets, stuff added to lists etc.
            Parallel.For(0, Program.NodeLevel, Program.ParallelZ, x=>
            //for (int x = 0; x < Program.NodeLevel + 1; x++)
            {
                // Get all of the nodes at the particular level investigated.
                CollisionStructs.Node[] AllNodesAtLevel = Program.CopyOfNodes.Where(z => z.DepthLevel == x).ToArray();

                // For each of the nodes in the level
                for (int z = 0; z < AllNodesAtLevel.Length; z++)
                {
                    // If there is no left neighbour || Try finding one
                    // The if statements below will work in such a fashion that they will take the location of the node and the size, and try to look for intersections with the other nodes.
                    // If we offset the individual X and Z axis by two on any direction, we should get a node intersection with an another node intersection.
                    // Program.PeepingTom(); // DEBUG
                    float ThreeQuarterNode = (float)(AllNodesAtLevel[z].CalculationProperties.NodeSize - (AllNodesAtLevel[z].CalculationProperties.NodeSize / 4.0));
                    float HalfNode = (float)(AllNodesAtLevel[z].CalculationProperties.NodeSize / 2.0);
                    if (Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].LeftNeighbourNode == 0)
                    {
                        // This top left corner is offset by -2, when intersecting 
                        RectangleF CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX - HalfNode, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ, ThreeQuarterNode, ThreeQuarterNode);

                        ushort NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                        Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].LeftNeighbourNode = NeighbourNode;
                        if (NeighbourNode != 0) { Program.CopyOfNodes[NeighbourNode].RightNeighbourNode = (ushort)AllNodesAtLevel[z].NodeIndex; }    
                        // Program.PeepingTom(); // DEBUG
                    }
                    // If there is no top neighbour || Try finding one
                    if (Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].TopNeighbourNode == 0)
                    {
                        // This top left corner is offset by -2 vertically, when intersecting 
                        RectangleF CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ - HalfNode, ThreeQuarterNode, ThreeQuarterNode);

                        ushort NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                        Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].TopNeighbourNode = NeighbourNode;
                        if (NeighbourNode != 0) { Program.CopyOfNodes[NeighbourNode].BottomNeighbourNode = (ushort)AllNodesAtLevel[z].NodeIndex; }      
                        // Program.PeepingTom(); // DEBUG
                    }
                    // If there is no right neighbour || Try finding one
                    if (Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].RightNeighbourNode == 0)
                    {
                        // This top left corner is offset by +2, when intersecting 
                        RectangleF CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX + HalfNode, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ, ThreeQuarterNode, ThreeQuarterNode);

                        ushort NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                        Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].RightNeighbourNode = NeighbourNode;
                        if (NeighbourNode != 0) { Program.CopyOfNodes[NeighbourNode].LeftNeighbourNode = (ushort)AllNodesAtLevel[z].NodeIndex; }
                        // Program.PeepingTom(); // DEBUG
                    }
                    // If there is no bottom neighbour || Try finding one
                    if (Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].BottomNeighbourNode == 0)
                    {
                        // This top left corner is offset by +2 vertically, when intersecting 
                        RectangleF CurrentNode = new RectangleF(AllNodesAtLevel[z].CalculationProperties.NodeMinimumX, AllNodesAtLevel[z].CalculationProperties.NodeMinimumZ + HalfNode, ThreeQuarterNode, ThreeQuarterNode);

                        ushort NeighbourNode = CheckForNeighbourGeneric(AllNodesAtLevel, CurrentNode, AllNodesAtLevel[z].NodeIndex);
                        Program.CopyOfNodes[AllNodesAtLevel[z].NodeIndex].BottomNeighbourNode = NeighbourNode;
                        if (NeighbourNode != 0) { Program.CopyOfNodes[NeighbourNode].TopNeighbourNode = (ushort)AllNodesAtLevel[z].NodeIndex; }
                        // Program.PeepingTom(); // DEBUG
                    }
                }

            }
            );
        }

        // This will check for neighbour by checking intersections between nodes.
        public static ushort CheckForNeighbourGeneric(CollisionStructs.Node[] AllNodesAtLevel, RectangleF NodeToTest, uint NodeIndex)
        {
            for (int x = 0; x < AllNodesAtLevel.Length; x++)
            {
                // TODO: Potential Optimization Using Own Solution Rather than InsersectsWith

                // Get area of current node;
                RectangleF CurrentNode = new RectangleF(AllNodesAtLevel[x].CalculationProperties.NodeMinimumX, AllNodesAtLevel[x].CalculationProperties.NodeMinimumZ, AllNodesAtLevel[x].CalculationProperties.NodeSize, AllNodesAtLevel[x].CalculationProperties.NodeSize);

                if (NodeToTest.IntersectsWith(CurrentNode) && NodeIndex != AllNodesAtLevel[x].NodeIndex)
                {
                    return (ushort)AllNodesAtLevel[x].NodeIndex;
                }

            }
            return 0;
        }

        public static void WriteCLFile()
        {
            // Allocate a buffer for the collision file.
            // The buffer size is 4MiB
            List<byte> CLFile = new List<byte>(4194304);

            // First we will calculate offsets.
            // Header section.
            CollisionStructs.FileHeader FileHeader = Program.FileHeaderX;
            CollisionStructs.Node[] CopyOfNodes = Program.CopyOfNodes; // Debug
            CollisionStructs.CL_TriangleStruct[] CopyOfTriangles = Program.OBJ_CopyOfTriangleList;
            CollisionStructs.VertexStruct[] CopyOfVertices = Program.OBJ_Vertex.ToArray();
            const byte HeaderLength = 0x28;

            // Finish Writing Header
            FileHeader.BasePower = Program.OffsetPowerLevel;
            FileHeader.NumberOfNodes = (ushort)Program.CopyOfNodes.Length;

            // Build up Sections 1 (Triangle References) and 2 (Nodes)
            List<ushort> TriangleReferenceList = new List<ushort>(100000); // 100,000 is my estimate for a size of triangle references, this is on the upper bound.
            int CurrentPointerOffset = HeaderLength; // This is the current offset as if writing the file, will increment to calculate triangle references.

            // Iterate over every node, on each node, set the pointer to the triangle list to the currentpointeroffset.
            // Add to the current pointer offset to a new value as each triangle is added to build the post header section.
            // Add the triangle index to TriangleReferenceList.
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                Program.CopyOfNodes[x].NodeTriangleCount = (ushort)Program.CopyOfNodes[x].NodeExtraProperties.TriangleList.Count;
                if (Program.CopyOfNodes[x].NodeTriangleCount != 0) { Program.CopyOfNodes[x].OffsetToTriangleList = (uint)CurrentPointerOffset; }
                for (int z = 0; z < Program.CopyOfNodes[x].NodeExtraProperties.TriangleList.Count; z++)
                {
                    TriangleReferenceList.Add(Program.CopyOfNodes[x].NodeExtraProperties.TriangleList[z]);
                    CurrentPointerOffset += 0x2; // Move the pointer to new location after done with triangles.
                }
            }
            FileHeader.Offset_QuadtreeSection = (uint)CurrentPointerOffset; // Add the newly obtained offset to the header.

            // Calculating Offsets!
            CurrentPointerOffset += Program.CopyOfNodes.Length * 0x20; // Get offset of Triangle Section by incrementing offset by length of node.
            FileHeader.Offset_TriangleSection = (uint)CurrentPointerOffset; // Add the newly obtained offset to the header.

            CurrentPointerOffset += Program.OBJ_CopyOfTriangleList.Length * 0x20; // Get offset of Vertex Section by incrementing offset by length of triangle.  
            FileHeader.Offset_VertexSection = (uint)CurrentPointerOffset; // Add the newly obtained offset to the header.

            CurrentPointerOffset += Program.OBJ_Vertex.Count * 0x0C; // Get offset of End of File by incrementing offset by length of vertex. 
            FileHeader.NumberOfBytes = (uint)CurrentPointerOffset; // Add the newly obtained offset to the header.

            /////
            /// Write the Actual file.
            /////
            byte NullByte = 0x00; // Used for writing 0x00
            ushort EmptyCollisionNeighbour = 0x0000; // Used for writing 0x0000

            /// Header!
            CLFile.AddRange(SwitchEndian(FileHeader.NumberOfBytes));
            CLFile.AddRange(SwitchEndian(FileHeader.Offset_QuadtreeSection));
            CLFile.AddRange(SwitchEndian(FileHeader.Offset_TriangleSection));
            CLFile.AddRange(SwitchEndian(FileHeader.Offset_VertexSection));
            CLFile.AddRange(SwitchEndian(FileHeader.QuadTreeCenter[0]));
            CLFile.AddRange(SwitchEndian(FileHeader.QuadTreeCenter[1]));
            CLFile.AddRange(SwitchEndian(FileHeader.QuadTreeCenter[2]));
            CLFile.AddRange(SwitchEndian(FileHeader.QuadTreeSize));
            CLFile.AddRange(SwitchEndian(FileHeader.BasePower));
            CLFile.AddRange(SwitchEndian(FileHeader.NumberOfTriangles));
            CLFile.AddRange(SwitchEndian(FileHeader.NumberOfVertices));
            CLFile.AddRange(SwitchEndian(FileHeader.NumberOfNodes));

            /// Triangle References!
            for (int x = 0; x < TriangleReferenceList.Count; x++)
            {
                CLFile.AddRange(SwitchEndian(TriangleReferenceList[x]));
            }

            /// Quadnodes
            for (int x = 0; x < Program.CopyOfNodes.Length; x++)
            {
                CLFile.AddRange(SwitchEndian( (ushort)Program.CopyOfNodes[x].NodeIndex ));
                CLFile.AddRange(SwitchEndian( (ushort)Program.CopyOfNodes[x].NodeParent ));
                CLFile.AddRange(SwitchEndian( (ushort)Program.CopyOfNodes[x].NodeChild));

                /*
                CLFile.AddRange(SwitchEndian((ushort)Program.CopyOfNodes[x].RightNeighbourNode));
                CLFile.AddRange(SwitchEndian((ushort)Program.CopyOfNodes[x].LeftNeighbourNode));
                CLFile.AddRange(SwitchEndian((ushort)Program.CopyOfNodes[x].BottomNeighbourNode));
                CLFile.AddRange(SwitchEndian((ushort)Program.CopyOfNodes[x].TopNeighbourNode));
                */
                CLFile.AddRange(BitConverter.GetBytes(EmptyCollisionNeighbour));
                CLFile.AddRange(BitConverter.GetBytes(EmptyCollisionNeighbour));
                CLFile.AddRange(BitConverter.GetBytes(EmptyCollisionNeighbour));
                CLFile.AddRange(BitConverter.GetBytes(EmptyCollisionNeighbour));

                CLFile.AddRange(SwitchEndian((ushort)Program.CopyOfNodes[x].NodeExtraProperties.TriangleList.Count));
                CLFile.AddRange(SwitchEndian(Program.CopyOfNodes[x].OffsetToTriangleList));

                CLFile.AddRange(SwitchEndian((ushort)Program.CopyOfNodes[x].PositioningOffsetValueLR));
                CLFile.AddRange(SwitchEndian((ushort)Program.CopyOfNodes[x].PositioningOffsetValueTB));

                CLFile.Add(Program.CopyOfNodes[x].DepthLevel);

                CLFile.Add(NullByte); // 8

                CLFile.Add(NullByte); // 8
                CLFile.Add(NullByte); // 16

                CLFile.Add(NullByte); // 8
                CLFile.Add(NullByte); // 16
                CLFile.Add(NullByte); // 24
                CLFile.Add(NullByte); // 32
            }

            /// Triangles
            for (int x = 0; x < Program.OBJ_CopyOfTriangleList.Length; x++)
            {
                CLFile.AddRange(SwitchEndian((ushort)Program.OBJ_CopyOfTriangleList[x].VertexOneID));
                CLFile.AddRange(SwitchEndian((ushort)Program.OBJ_CopyOfTriangleList[x].VertexTwoID));
                CLFile.AddRange(SwitchEndian((ushort)Program.OBJ_CopyOfTriangleList[x].VertexThreeID));
                CLFile.AddRange(SwitchEndian((ushort)Program.OBJ_CopyOfTriangleList[x].AdjacentTriangleIndexONE));
                CLFile.AddRange(SwitchEndian((ushort)Program.OBJ_CopyOfTriangleList[x].AdjacentTriangleIndexTWO));
                CLFile.AddRange(SwitchEndian((ushort)Program.OBJ_CopyOfTriangleList[x].AdjacentTriangleIndexTHREE));
                CLFile.AddRange(SwitchEndian(Program.OBJ_CopyOfTriangleList[x].NormalUnitVector.PositionX));
                CLFile.AddRange(SwitchEndian(Program.OBJ_CopyOfTriangleList[x].NormalUnitVector.PositionY));
                CLFile.AddRange(SwitchEndian(Program.OBJ_CopyOfTriangleList[x].NormalUnitVector.PositionZ));

                CLFile.Add(NullByte); // 8
                CLFile.Add(NullByte); // 16
                CLFile.Add(NullByte); // 24
                CLFile.Add(NullByte); // 32

                CLFile.Add(NullByte); // 8
                CLFile.Add(NullByte); // 16
                CLFile.Add(NullByte); // 24
                CLFile.Add(NullByte); // 32
            }

            /// Vertices
            for (int x = 0; x < Program.OBJ_Vertex.Count; x++)
            {
                CLFile.AddRange(SwitchEndian(Program.OBJ_Vertex[x].PositionX));
                CLFile.AddRange(SwitchEndian(Program.OBJ_Vertex[x].PositionY));
                CLFile.AddRange(SwitchEndian(Program.OBJ_Vertex[x].PositionZ));
            }

            File.WriteAllBytes(Program.FilePath + ".cl", CLFile.ToArray());
        }

        public static List<byte> SwitchEndian(uint Value) { return BitConverter.GetBytes(Value).Reverse().ToList(); }
        public static List<byte> SwitchEndian(int Value) { return BitConverter.GetBytes(Value).Reverse().ToList(); }
        public static List<byte> SwitchEndian(float Value) { return BitConverter.GetBytes(Value).Reverse().ToList(); }
        public static List<byte> SwitchEndian(short Value) {  return BitConverter.GetBytes(Value).Reverse().ToList(); }
        public static List<byte> SwitchEndian(ushort Value) { return BitConverter.GetBytes(Value).Reverse().ToList(); }
    }
}
