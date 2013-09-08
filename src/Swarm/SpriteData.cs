namespace Swarm
{
    using UnityEngine;

    /// <summary>
    /// Describes a single sprite. A sprite is represented by a quad with UV coordinates mapping the texture.
    /// </summary>
    public class SpriteData
    {
        // The local vertices that are transformed into client space
        private Vector3[] localVerts;

        // The UV data
        private Vector2 uvPosition;
        private Vector2 uvSize;

        /// <summary>
        /// Initialises a new instance of the SpriteData class.
        /// </summary>
        /// <param name="index">The sprite index.</param>
        public SpriteData(int index)
        {
            this.Index = index;
            this.VertIndices = new int[] {
                this.Index * 4,
                this.Index * 4 + 1,
                this.Index * 4 + 2,
                this.Index * 4 + 3 };
            this.UVIndices = new int[] { 
                this.Index * 4,
                this.Index * 4 + 1,
                this.Index * 4 + 2,
                this.Index * 4 + 3 };
            this.Clear();
        }

        /// <summary>
        /// Gets the sprite index.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Gets the indices for the vertex data.
        /// </summary>
        public int[] VertIndices { get; private set; }

        /// <summary>
        /// Gets the indices for the UV data.
        /// </summary>
        public int[] UVIndices { get; private set; }

        /// <summary>
        /// Gets the game object to which this sprite belongs.
        /// </summary>
        public GameObject Client { get; private set; }

        /// <summary>
        /// Set the data for the sprite.
        /// </summary>
        /// <param name="client">The game object to which this sprite belongs.</param>
        /// <param name="size">The size of the sprite quad.</param>
        /// <param name="uvPosition">The lower-left UV position.</param>
        /// <param name="uvSize">The size of the UV.</param>
        public void Initialise(GameObject client, Vector2 size, Vector2 uvPosition, Vector2 uvSize)
        {
            this.Client = client;
            this.uvSize = uvSize;
            this.uvPosition = uvPosition;
            this.localVerts = new Vector3[] {
                new Vector3(0, 0),
                new Vector3(size.x, 0),
                new Vector3(size.x, size.y),
                new Vector3(0, size.y) };
        }

        /// <summary>
        /// Reset the sprite data.
        /// </summary>
        public void Clear()
        {
            this.Initialise(null, Vector2.zero, Vector2.zero, Vector2.zero);
        }

        /// <summary>
        /// Gets the vertices.
        /// </summary>
        /// <returns>The vertices.</returns>
        public Vector3[] GetVertices()
        {
            if (this.Client != null)
            {
                return new Vector3[] {
                    this.Client.transform.TransformPoint(this.localVerts[0]),
                    this.Client.transform.TransformPoint(this.localVerts[1]),
                    this.Client.transform.TransformPoint(this.localVerts[2]),
                    this.Client.transform.TransformPoint(this.localVerts[3])};
            }
            else
            {
                return this.localVerts;
            }
        }

        /// <summary>
        /// Gets the UV coordinates for this sprite.
        /// </summary>
        /// <returns>The UV coordinates.</returns>
        public Vector2[] GetUVs()
        {
            return new Vector2[] {
                new Vector2(this.uvPosition.x, this.uvPosition.y),
                new Vector2(this.uvPosition.x + this.uvSize.x, this.uvPosition.y),
                new Vector2(this.uvPosition.x + this.uvSize.x, this.uvPosition.y + this.uvSize.y),
                new Vector2(this.uvPosition.x, this.uvPosition.y + this.uvSize.y)};
        }
    }
}