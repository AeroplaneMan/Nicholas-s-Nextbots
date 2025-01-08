using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is the NextBot AI for a single-player game.
/// The bot will follow the player character until they die.
/// </summary>
public class NextBot : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    /// <summary>
    /// Sets up the agent and disables the GameObject for activation later.
    /// </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //gameObject.SetActive(false);
    }

    /// <summary>
    /// Initializes the player reference when the bot is enabled.
    /// </summary>
    private void OnEnable()
    {
        // Assuming there's only one player with the "Player" tag
        player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found.");
        }
    }

    /// <summary>
    /// Updates the bot's behavior. It follows the player if they are not dead.
    /// </summary>
    private void Update()
    {
        if (player != null)
        {
            // Check if the player is not dead
            var playerCollision = player.GetComponent<CollisionDetection>();
            if (playerCollision != null && !playerCollision.Dead)
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }
}

