namespace Swarm
{
    using UnityEngine;

    /// <summary>
    /// A 2D sprite.
    /// </summary>
    public class Sprite : MonoBehaviour
    {
        /// <summary>
        /// Unity Setting: The UV coordinates of the sprite.
        /// </summary>
        public Vector2 Size;

        /// <summary>
        /// Unity Setting: The UV coordinates of the sprite.
        /// </summary>
        public Vector2 UV;

        /// <summary>
        /// Unity Setting: The UV size of the sprite.
        /// </summary>
        public Vector2 UVSize;

        /// <summary>
        /// The sprite manager.
        /// </summary>
        private SpriteManager spriteManager;

        /// <summary>
        /// The sprite data.
        /// </summary>
        private SpriteData spriteData;

        /// <summary>
        /// Initialises the component.
        /// </summary>
        public void Start()
        {
            this.spriteManager = (SpriteManager)GameObject.FindObjectOfType(typeof(SpriteManager));
            this.spriteData = this.spriteManager.AddSprite(this.gameObject, this.Size, this.UV, this.UVSize);
        }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        public void Update()
        {
            this.spriteManager.UpdateTransform(this.spriteData);
        }

        /// <summary>
        /// This function is called when the component will be destroyed.
        /// </summary>
        public void OnDestroy()
        {
            this.spriteManager.RemoveSprite(this.spriteData);
            this.spriteData = null;
        }
    }
}