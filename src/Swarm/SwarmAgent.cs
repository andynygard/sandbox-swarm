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
    }
}