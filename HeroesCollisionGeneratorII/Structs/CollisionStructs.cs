using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesCollisionGeneratorII
{
    class CollisionStructs
    {
        // Header for the .CL file
        public struct FileHeader
        {
            public UInt32 NumberOfBytes; // Total bytes in the file, inclusive of this value.
            public UInt32 Offset_QuadtreeSection; // Second section of file, past header and triangle index list.
            public UInt32 Offset_TriangleSection; // Third section of file.
            public UInt32 Offset_VertexSection; // Last section of file.
            public float[] QuadTreeCenter; // Point very central to quadtree;
            public float QuadTreeSize; // Overall size of quadtree. // The quadtree is a square, this is both width and height.
            public UInt16 BasePower; // Base power of quadnode. This is pretty much useless... Except it needs to be used :/
            public UInt16 NumberOfTriangles; // Total Triangle Count
            public UInt16 NumberOfVertices; // Total Number of Verts
            public UInt16 NumberOfNodes; // Number of Quadnodes
        }

        // A singular Quadtree Node used in the collision data for Heroes. 
        public struct Node
        {
            public UInt32 NodeIndex; // Individual uniquely identifiable index.
            public UInt32 NodeParent; // Parent node to the current node in question.
            public UInt32 NodeChild; // The index of the first out of four children of the node. All four nodes must individually proceed each other.
            public UInt16 RightNeighbourNode; // The right neighbour to the current node. 0 if edge/none. It can be either at the at the same or at another depth level.
            public UInt16 LeftNeighbourNode; // The left neighbour to the current node. 0 if edge/none. It can be either at the at the same or at another depth level.
            public UInt16 BottomNeighbourNode; // The bottom neighbour to the current node. 0 if edge/none. It can be either at the at the same or at another depth level.
            public UInt16 TopNeighbourNode; // The top neighbouring node. 0 if edge/none. It can be either at the at the same or at another depth level.
            public UInt16 NodeTriangleCount; // Number of triangles in node.
            public UInt32 OffsetToTriangleList; // Offset to first triangle in triangle list for this node.
            public UInt16 PositioningOffsetValueLR; // Left/Right 
            public UInt16 PositioningOffsetValueTB; // Top/Bottom
            public Byte DepthLevel; // Depth level of collision set.

            // Three null bytes.

            public NodeCalculationProperties CalculationProperties; // Used only for generation.
            public NodeExtraProperties NodeExtraProperties; // Used for the list after the header.
        }

        // Everything that will be necessary to generate the entire node tree.
        public struct NodeCalculationProperties
        {
            public bool NodeVerified; // Decide whether the node has been processed.
            public float NodeCenterX; // Declare absolute center of node in X-Z space coordinates.
            public float NodeCenterZ; // Declare absolute center of node in X-Z space coordinates.
            public float NodeSize;     // Length and width of node!
            public float NodeMinimumX; // Declare min X of node.
            public float NodeMaximumX; // Declare max X of node.
            public float NodeMinimumZ; // Declare min Z of node.
            public float NodeMaximumZ; // Declare max Z of node.
        }

        // Any properties which will be necessary to calculate another part of the file.
        public struct NodeExtraProperties
        {
            public List<ushort> TriangleList; // List of triangles in this node.
        }

        // Cached copy of node sizes to be created at runtime.
        public struct NodeSizes
        {
            public float[] NodeSize; // Width & Height of Node at specified depth level. Depth level will be the index of this array. Zero indexed!
        }

        // Struct for individual vertex
        public struct VertexStruct
        {
            public float PositionX;
            public float PositionY;
            public float PositionZ;
        }

        // Struct for an individual triangle
        public struct CL_TriangleStruct
        {
            public UInt16 VertexOneID;
            public UInt16 VertexTwoID;
            public UInt16 VertexThreeID;
            public UInt16 AdjacentTriangleIndexONE; // adjacent triangle 1: vertex1 of current triangle is vertex3 of adjacent; vertex2 of current triangle is vertex2 of adjacent (unsure)
            public UInt16 AdjacentTriangleIndexTWO; // adjacent triangle 2: vertex3 of current triangle is vertex1 of adjacent; vertex2 of current triangle is vertex2 of adjacent (unsure)
            public UInt16 AdjacentTriangleIndexTHREE; // adjacent triangle 3: vertex1 of current triangle is vertex1 of adjacent; vertex3 of current triangle is vertex2 of adjacent (unsure)
            public VertexStruct NormalUnitVector; // normal unit vector X, Y, Z (can be calculated by doing cross product of vectors vertex(2-1) and vertex(3-1).
            public TriangleCalculationProperties ExtraProperties; // Used for Calculating Triangle Stuff
            // public CollisionFlags CollisionFlags;
        }

        // We will treat the triangle as a square for the purposes of adjacent triangle finding and node optimization.
        public struct TriangleCalculationProperties
        {
            public ushort TempTriangleIndex; // An unique index. Assigned when triangle is created.
            // public float TriangleCenterZ; // Heroes uses Y as vertical axis, we want horizontal.
            // public float TriangleCenterX; // Heroes uses Y as vertical axis, we want horizontal.
            public float TriangleHeight; // Defines the Height of the Triangle
            public float TriangleWidth; // Defines the Width of the Triangle
            public float MinimumX; // Heroes uses Y as vertical axis, we want horizontal.
            // public float MaximumX; // Heroes uses Y as vertical axis, we want horizontal.
            public float MinimumZ; // Heroes uses Y as vertical axis, we want horizontal. | Required for top corner identifier!
            // public float MaximumZ; // Heroes uses Y as vertical axis, we want horizontal. 
        }

        public struct CollisionFlags
        {
            public byte SurfaceFlags; // 0x20 Something possibly to do with friction | 0x40, Pinball | 0x80, Bingo 
            public byte ForwardMomentumPushback; // Used for slopes. Force with which Sonic's forward running movement is opposed.
            public byte VerticalMomentumPushback; // Used to roughen surfaces, stairs etc. Force with which Sonic's upward running movement is opposed.
            public byte SidewaysMomentumPushback; // Used for... rocks? Force with which Sonic's sideways running movement is opposed.
        }
    }
}
