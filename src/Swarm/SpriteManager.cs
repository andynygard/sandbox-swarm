namespace Swarm
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Manages the game sprites such that they all share the same mesh.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class SpriteManager : MonoBehaviour
    {
        /// <summary>
        /// Unity Setting: The number of sprites to allocate space for at a time.
        /// </summary>
        public int AllocationSize;

        // The mesh
        private Mesh mesh;

        // The sprites
        private SpriteData[] sprites;
        private List<int> availableSprites;
        private List<int> activeSprites;

        // The mesh data
        private Vector3[] vertices;
        private int[] triangles;
        private Vector2[] uvs;

        // Indicates whether the mesh needs to update
        private bool vertCountChanged;
        private bool vertValuesChanged;
        private bool uvValuesChanged;

        /// <summary>
        /// Initialises the component prior Start being called on any components.
        /// </summary>
        public void Awake()
        {
            this.mesh = this.GetComponent<MeshFilter>().mesh;
            this.availableSprites = new List<int>();
            this.activeSprites = new List<int>();
            this.sprites = new SpriteData[0];
            this.vertices = new Vector3[0];
            this.triangles = new int[0];
            this.uvs = new Vector2[0];

            this.GrowSprites(this.AllocationSize);
        }

        /// <summary>
        /// Called once per frame after Update has been called on all components.
        /// </summary>
        public void LateUpdate()
        {
            if (this.vertCountChanged)
            {
                this.mesh.Clear();
                this.mesh.vertices = this.vertices;
                this.mesh.triangles = this.triangles;
                this.mesh.uv = this.uvs;

                this.vertCountChanged = false;
                this.vertValuesChanged = false;
                this.uvValuesChanged = false;
            }
            else
            {
                if (this.vertValuesChanged)
                {
                    this.mesh.vertices = this.vertices;
                    this.vertValuesChanged = false;
                }

                if (this.uvValuesChanged)
                {
                    this.mesh.uv = this.uvs;
                    this.uvValuesChanged = false;
                }
            }
        }

        /// <summary>
        /// Adds a sprite.
        /// </summary>
        /// <param name="client">The game object.</param>
        /// <param name="size">The sprite size.</param>
        /// <param name="uv">The UV position.</param>
        /// <param name="uvSize">The UV size.</param>
        /// <returns>The sprite that was added.</returns>
        public SpriteData AddSprite(GameObject client, Vector2 size, Vector2 uv, Vector2 uvSize)
        {
            // Grow the sprite arrays if necessary
            if (this.availableSprites.Count == 0)
            {
                this.GrowSprites(this.AllocationSize);
            }
            
            // Get the index of the next available sprite
            int spriteIndex = this.availableSprites[0];
            this.availableSprites.RemoveAt(0);

            // Get the sprite and set the data
            SpriteData sprite = this.sprites[spriteIndex];
            sprite.Initialise(client, size, uv, uvSize);

            // Add this to the active list
            this.activeSprites.Add(spriteIndex);

            // Set the vertex and UV data
            Vector3[] vertices = sprite.GetVertices();
            Vector2[] uvs = sprite.GetUVs();
            for (int i = 0; i < 4; i++)
            {
                this.vertices[sprite.VertIndices[i]] = vertices[i];
                this.uvs[sprite.UVIndices[i]] = uvs[i];
            }

            this.vertValuesChanged = true;
            this.uvValuesChanged = true;

            return sprite;
        }

        /// <summary>
        /// Removes a sprite.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        public void RemoveSprite(SpriteData sprite)
        {
            // Clear all sprite data
            sprite.Clear();

            // Zero out the sprite vertices
            foreach (int vertIndex in sprite.VertIndices)
            {
                this.vertices[vertIndex] = Vector3.zero;
            }

            // Update the tracking lists
            this.availableSprites.Add(sprite.Index);
            this.activeSprites.Remove(sprite.Index);

            this.vertValuesChanged = true;
        }

        /// <summary>
        /// Updates a sprite transform.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        public void UpdateTransform(SpriteData sprite)
        {
            Vector3[] vertices = sprite.GetVertices();
            for (int i = 0; i < 4; i++)
            {
                this.vertices[sprite.VertIndices[i]] = vertices[i];
            }

            this.vertValuesChanged = true;
        }

        /// <summary>
        /// Increases the size of the arrays to accommodate the given number of sprites.
        /// </summary>
        /// <param name="count">The additional number of sprites to allocate.</param>
        private void GrowSprites(int count)
        {
            int firstNewElement = this.sprites.Length;

            SpriteData[] tempSprites = this.sprites;
            this.sprites = new SpriteData[this.sprites.Length + count];
            tempSprites.CopyTo(this.sprites, 0);

            Vector3[] tempVertices = this.vertices;
            this.vertices = new Vector3[this.vertices.Length + count * 4];
            tempVertices.CopyTo(this.vertices, 0);

            int[] tempTriangles = this.triangles;
            this.triangles = new int[this.triangles.Length + count * 6];
            tempTriangles.CopyTo(this.triangles, 0);

            Vector2[] tempUVs = this.uvs;
            this.uvs = new Vector2[this.uvs.Length + count * 4];
            tempUVs.CopyTo(this.uvs, 0);

            // Initialise the new sprites
            for (int i = firstNewElement; i < this.sprites.Length; i++)
            {
                var sprite = new SpriteData(i);
                this.sprites[i] = sprite;
                this.availableSprites.Add(i);

                // Initialise the triangles
                //   3 __ 2
                //    | /|
                //    |/_|
                //   0    1
                this.triangles[i * 6 + 0] = sprite.VertIndices[0];
                this.triangles[i * 6 + 1] = sprite.VertIndices[3];
                this.triangles[i * 6 + 2] = sprite.VertIndices[2];
                this.triangles[i * 6 + 3] = sprite.VertIndices[0];
                this.triangles[i * 6 + 4] = sprite.VertIndices[2];
                this.triangles[i * 6 + 5] = sprite.VertIndices[1];
            }

            this.vertCountChanged = true;
            this.vertValuesChanged = true;
            this.uvValuesChanged = true;
        }
    }
}