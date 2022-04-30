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
        public void AddLineSegment(Vector3 v1, Vector3 v2, Vector3 brush) { //? QOL function, this could easily be made as a quad
            AddQuad(v1 - brush, v1 + brush, v2 + brush, v2 - brush);
        }

        // * Adding Vertex Colors
        void AddQuadColor(Color c1) {
            newColors.Add(c1);
            newColors.Add(c1);
            newColors.Add(c1);
            newColors.Add(c1);

        }
        void AddQuadColor(Color c1, Color c2) {
            newColors.Add(c1);
            newColors.Add(c1);
            newColors.Add(c2);
            newColors.Add(c2);

        }
        void AddQuadColor(Color c1, Color c2, Color c3, Color c4) {
            newColors.Add(c1);
            newColors.Add(c2);
            newColors.Add(c3);
            newColors.Add(c4);
        }
        void AddTriangleColor(Color color) {
            newColors.Add(color);
            newColors.Add(color);
            newColors.Add(color);
        }
        void AddTriangleColor(Color c1, Color c2, Color c3) {
            newColors.Add(c1);
            newColors.Add(c2);
            newColors.Add(c3);
        }
    }
}
