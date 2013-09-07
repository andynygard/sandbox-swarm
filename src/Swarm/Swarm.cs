namespace Swarm
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Represents the swarm.
    /// </summary>
    public class Swarm : MonoBehaviour
    {
        /// <summary>
        /// Unity Setting: The initial number of entities in the swarm.
        /// </summary>
        public int InitialEntities;

        /// <summary>
        /// The agents within the swarm.
        /// </summary>
        private List<SwarmAgent> agents;

        /// <summary>
        /// Initialises the component.
        /// </summary>
        public void Start()
        {
            this.agents = new List<SwarmAgent>();
            for (int i = 0; i < this.InitialEntities; i++)
            {
                this.agents.Add(this.CreateAgent());
            }
        }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        public void Update()
        {
            // The swarm follows the Boids algorithm as described by Reynolds (http://www.red3d.com/cwr/)
            // There are three rules that each agent follows:
            //   1. Flock towards center of swarm.
            //   2. Keep a small distance from neighbours and other obstacles.
            //   3. Try to match the velocity of nearby neighbours.
            Vector3 toCenter, toAvoid, toMatch;
            foreach (SwarmAgent agent in this.agents)
            {
                toCenter = this.CalculateToCenter(agent);
                toAvoid = this.CalculateToAvoid(agent);
                toMatch = this.CalculateToMatch(agent);

                agent.Velocity += toCenter + toAvoid + toMatch;
                agent.transform.position += agent.Velocity;
            }
        }

        /// <summary>
        /// Calculates the vector to flock towards the center of the swarm.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>The vector.</returns>
        private Vector3 CalculateToCenter(SwarmAgent agent)
        {
            // TODO
            return Vector3.zero;
        }

        /// <summary>
        /// Calculates the vector to keep a small distance from neighbours and other obstacles.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>The vector.</returns>
        private Vector3 CalculateToAvoid(SwarmAgent agent)
        {
            // TODO
            return Vector3.zero;
        }

        /// <summary>
        /// Calculates the vector to match velocity with neighbours.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>The vector.</returns>
        private Vector3 CalculateToMatch(SwarmAgent agent)
        {
            // TODO
            return Vector3.zero;
        }

        /// <summary>
        /// Creates a swarm agent.
        /// </summary>
        /// <returns>The agent.</returns>
        private SwarmAgent CreateAgent()
        {
            var gameObject = new GameObject("An angry bee");
            gameObject.transform.parent = this.transform;
            gameObject.transform.localPosition = Vector3.zero;
            return gameObject.AddComponent<SwarmAgent>();
        }
    }
}