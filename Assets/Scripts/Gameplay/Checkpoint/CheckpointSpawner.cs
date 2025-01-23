using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    private GameObject oldCheckpoint;
    public GameObject checkpoint; // The checkpoint prefab
    [SerializeField] private float minXPos; // Minimum X position for spawn
    [SerializeField] private float maxXPos; // Maximum X position for spawn
    [SerializeField] private float minZPos; // Minimum Z position for spawn
    [SerializeField] private float maxZPos; // Maximum Z position for spawn
    [SerializeField] private LayerMask buildingLayer; // LayerMask for buildings or obstacles
    [SerializeField] private float raycastDistance; // Distance of the raycast
    [SerializeField] private LevelCompleteCheck levelComplete;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Trigger")
        {
            Vector3 randomPosition;
            int maxAttempts = 10; // Maximum attempts to find a valid position
            bool validPositionFound = false;

            for (int i = 0; i < maxAttempts; i++)
            {
                // Generate a random position
                randomPosition = new Vector3(
                    Random.Range(minXPos, maxXPos),
                    0.58f, // Ground height
                    Random.Range(minZPos, maxZPos)
                );

                // Check if the area is clear using Physics.OverlapSphere
                Collider[] colliders = Physics.OverlapSphere(randomPosition, 0.5f, buildingLayer);

                if (colliders.Length == 0)
                {
                    // No obstacles detected, spawn checkpoint
                    validPositionFound = true;

                    oldCheckpoint = checkpoint;

                    // Instantiate the checkpoint
                    checkpoint = Instantiate(checkpoint, randomPosition, Quaternion.identity);

                    levelComplete.numCheckpoints++;
                    // Debug visualizations
                    Debug.DrawLine(Vector3.zero, randomPosition, Color.cyan, 60f); // Line from origin to checkpoint for visibility

                    break;
                }
                else
                {
                    // Debug sphere if the position is invalid
                    Debug.DrawRay(randomPosition, Vector3.up * 5f, Color.red, 2f);
                }
            }

            if (!validPositionFound)
            {
                Debug.LogWarning("Could not find a valid position for the checkpoint.");
            }

            Destroy(oldCheckpoint); // Destroy the old checkpoint
        }
    }
}
