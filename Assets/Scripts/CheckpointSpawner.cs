using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] private bool levelCompleted = false;
    [SerializeField] private GameObject checkpoint; // The checkpoint prefab
    [SerializeField] private float minXPos; // Minimum X position for spawn
    [SerializeField] private float maxXPos; // Maximum X position for spawn
    [SerializeField] private float minZPos; // Minimum Z position for spawn
    [SerializeField] private float maxZPos; // Maximum Z position for spawn
    [SerializeField] private LayerMask buildingLayer; // LayerMask for buildings or obstacles
    [SerializeField] private float raycastDistance = 5f; // Distance of the raycast
    [SerializeField] private int checkpointsToComplete;
    [SerializeField] private Animator doorAnimation;
    private int numCheckpoints;

    private void Start()
    {
        doorAnimation.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 randomPosition;
        int maxAttempts = 10; // Maximum attempts to find a valid position
        bool validPositionFound = false;

        for (int i = 0; i < maxAttempts; i++)
        {
            // Generate a random position
            randomPosition = new Vector3(Random.Range(minXPos, maxXPos), 0.58f, Random.Range(minZPos, maxZPos));

            // Cast a ray forward to check for obstacles
            if (!Physics.Raycast(randomPosition, Vector3.forward, raycastDistance, buildingLayer))
            {
                // Debug line for raycast
                Debug.DrawLine(randomPosition, randomPosition + Vector3.forward * raycastDistance, Color.green, 2f);

                // No obstacle detected, spawn checkpoint
                Instantiate(checkpoint, randomPosition, Quaternion.identity);

                numCheckpoints++;
                // Debug line for new checkpoint position
                Debug.DrawLine(Vector3.zero, randomPosition, Color.cyan, 60f); // Line from origin to checkpoint for visibility

                validPositionFound = true;
                break;
            }
            else
            {
                // Debug line if raycast hit an obstacle
                Debug.DrawLine(randomPosition, randomPosition + Vector3.forward * raycastDistance, Color.red, 2f);
            }
        }

        if (!validPositionFound)
        {
            Debug.LogWarning("Could not find a valid position for the checkpoint.");
        }

        Destroy(checkpoint); // Destroy the spawner object after spawning
        if (numCheckpoints == checkpointsToComplete)
        {
            levelCompleted = true;
            doorAnimation.enabled = true;
            doorAnimation.Play("Scene");
        }
    }

    private void LevelComplete(int checkpoints)
    {

    }
}
