using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.MeshUtilities {
    public class MeshUtilityWrapper {
        private Mesh mesh;
        private MeshRenderer meshRenderer;
        private bool useVertColors;
        MeshCollider meshCollider;
        static List<Vector3> newVertices = new List<Vector3>();
        static List<int> newTriangles = new List<int>();
        static List<Vector2> newUV = new List<Vector2>();
        static List<Color> newColors = new List<Color>();

        // * Constructor
        public MeshUtilityWrapper(Mesh _mesh, MeshRenderer _meshRenderer, bool _useVertColors = false) {
            this.mesh = _mesh;
            this.meshRenderer = _meshRenderer;
            useVertColors = _useVertColors;
            if(useVertColors == true) {
                meshRenderer.material = new Material(Shader.Find("SadSapphicGames/MeshUtilities/VertColorShader"));
            }
        }

        // * Redraw the mesh
        public void UpdateMesh() {
            mesh.Clear();
            mesh.vertices = newVertices.ToArray();
            mesh.triangles = newTriangles.ToArray();
            mesh.uv = newUV.ToArray();
            if(useVertColors) mesh.colors = newColors.ToArray();
        }

        // * Reset the lists
        public void ResetMesh() {
            mesh.Clear();
            newVertices.Clear();
            newTriangles.Clear();
            newUV.Clear();
            newColors.Clear();
        }
        
        // * Adding shapes
        public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertIndex = newVertices.Count;

            newVertices.Add(v1);
            newVertices.Add(v2);
            newVertices.Add(v3);

            newTriangles.Add(vertIndex);
            newTriangles.Add(vertIndex + 1);
            newTriangles.Add(vertIndex + 2);
        }
        public void AddTriangleUVs(Vector2 v1, Vector2 v2, Vector2 v3) {
            newUV.Add(v1);
            newUV.Add(v2);
            newUV.Add(v3);
            CheckListLength(newUV);
        }
        public void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            int vertIndex = newVertices.Count;

            newVertices.Add(v1);
            newVertices.Add(v2);
            newVertices.Add(v3);
            newVertices.Add(v4);

            newTriangles.Add(vertIndex);
            newTriangles.Add(vertIndex + 1);
            newTriangles.Add(vertIndex + 2);

            newTriangles.Add(vertIndex);
            newTriangles.Add(vertIndex + 2);
            newTriangles.Add(vertIndex + 3);
        }
        public void AddQuadUV(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4) {
            newUV.Add(v1);
            newUV.Add(v2);
            newUV.Add(v3);

            newUV.Add(v1);
            newUV.Add(v3);
            newUV.Add(v4);
            CheckListLength(newUV);
        }
        public void AddLineSegment(Vector3 v1, Vector3 v2, Vector3 brush) { //? QOL function, this could easily be made as a quad
            AddQuad(v1 - brush, v1 + brush, v2 + brush, v2 - brush);
        }

        // * Adding Vertex Colors
        public void AddQuadColor(Color c1) {
            if(!useVertColors) return;
            newColors.Add(c1);
            newColors.Add(c1);
            newColors.Add(c1);
            newColors.Add(c1);
            CheckListLength(newColors);
        }
        public void AddQuadColor(Color c1, Color c2) {
            if(!useVertColors) return;
            newColors.Add(c1);
            newColors.Add(c1);
            newColors.Add(c2);
            newColors.Add(c2);
            CheckListLength(newColors);
        }
        public void AddQuadColor(Color c1, Color c2, Color c3, Color c4) {
            if(!useVertColors) return;
            newColors.Add(c1);
            newColors.Add(c2);
            newColors.Add(c3);
            newColors.Add(c4);
            CheckListLength(newColors);
        }
        public void AddTriangleColor(Color color) {
            if(!useVertColors) return;
            newColors.Add(color);
            newColors.Add(color);
            newColors.Add(color);
            CheckListLength(newColors);
        }
        public void AddTriangleColor(Color c1, Color c2, Color c3) {
            if(!useVertColors) return;
            newColors.Add(c1);
            newColors.Add(c2);
            newColors.Add(c3);
            CheckListLength(newColors);
        }
        private void CheckListLength(List<> list) {
            if (
                list.Count != 0
                && list.Count != newTriangles.Count //? i believe this is the correct comparison to make
            ) {
                throw new InvalidListLengthException($"{list.Name} isn't empty but does not match the length of newTriangles, are you missing a function call somewhere?");
            } else return;
        }
    }
    [System.Serializable]
    public class InvalidListLengthException : System.Exception
    {
        public InvalidListLengthException() { }
        public InvalidListLengthException(string message) : base(message) { }
        public InvalidListLengthException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidListLengthException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
