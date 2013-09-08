namespace Swarm
{
    using UnityEngine;

    /// <summary>
    /// Represents a single angry bee / bird / etc within the swarm.
    /// </summary>
    public class SwarmAgent : MonoBehaviour
    {
        /// <summary>
        /// Gets or sets the velocity of the agent.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the swarm.
        /// </summary>
        public Swarm Swarm { get; set; }

        /// <summary>
        /// This function is called when the component will be destroyed.
        /// </summary>
        public void OnDestroy()
        {
            this.Swarm.RemoveAgent(this);
        }
    }
}