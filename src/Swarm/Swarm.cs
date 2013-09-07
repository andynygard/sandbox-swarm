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
        /// The initial radius of the swarm.
        /// </summary>
        public const int InitialRadius = 10;

        /// <summary>
        /// Unity Setting: The initial number of agents in the swarm.
        /// </summary>
        public int InitialAgents;

        /// <summary>
        /// Unity Setting: The minimum separation between agents.
        /// </summary>
        public float Separation = 1f;

        /// <summary>
        /// Unity Setting: The scalar for the velocity moving to the center.
        /// </summary>
        public float ScalarToCenter = 0.8f;

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
            for (int i = 0; i < this.InitialAgents; i++)
            {
                this.agents.Add(this.CreateAgent());
            }
        }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        public void Update()
        {
            // Sanity check
            if (this.agents.Count < 2)
            {
                return;
            }

            // The swarm follows the Boids algorithm as described by Reynolds (http://www.red3d.com/cwr/)
            // There are three rules that each agent follows:
            //   1. Flock towards center of swarm.
            //   2. Keep a small distance from neighbours and other obstacles.
            //   3. Try to match the velocity of nearby neighbours.
            foreach (SwarmAgent agent in this.agents)
            {
                // Now step through all other agents and perform rules 1, 2 and 3
                Vector3 sumPosition = Vector2.zero;
                Vector3 sumAvoidVector = Vector2.zero;
                foreach (SwarmAgent other in this.agents)
                {
                    if (other != agent)
                    {
                        sumPosition += other.transform.position;
                        if ((other.transform.position - agent.transform.position).magnitude < this.Separation)
                        {
                            sumAvoidVector += agent.transform.position - other.transform.position;
                        }
                    }
                }

                // Calculate the average position of all agents, not including this agent, such that this agent
                // will move towards what it perceives as the center
                Vector2 toCenter = (Vector2)((sumPosition / (this.agents.Count - 1)) - agent.transform.position);
                toCenter *= this.ScalarToCenter;

                // Calculate the avoidance vector. This moves the agent away from nearby boids by the current distance
                // that is has from them
                Vector2 toAvoid = (Vector2)sumAvoidVector;

                Vector2 toMatch = Vector2.zero;

                // Update the position of this agent
                agent.Velocity += toCenter + toAvoid + toMatch;
                agent.transform.position += (Vector3)agent.Velocity;
            }
        }

        /// <summary>
        /// Creates a swarm agent.
        /// </summary>
        /// <returns>The agent.</returns>
        private SwarmAgent CreateAgent()
        {
            var gameObject = new GameObject("An angry bee");
            gameObject.transform.parent = this.transform;
            gameObject.transform.localPosition = Random.insideUnitCircle * Swarm.InitialRadius;
            return gameObject.AddComponent<SwarmAgent>();
        }
    }
}