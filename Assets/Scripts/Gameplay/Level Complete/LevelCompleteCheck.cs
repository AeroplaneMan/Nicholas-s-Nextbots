using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelCompleteCheck : MonoBehaviour
{
    [SerializeField] public bool levelCompleted = false;
    public int checkpointsToComplete;
    public int numCheckpoints;
    [SerializeField] private Animator doorAnimation;
    [SerializeField] private GameObject player; 

    void Start()
    {
        doorAnimation.enabled = false;
        Update();
    }

    public void Update()
    {
        if (numCheckpoints == checkpointsToComplete)
        {
            levelCompleted = true;
            doorAnimation.enabled = true;
            doorAnimation.Play("Scene");
            Destroy(player.GetComponent<CheckpointSpawner>().checkpoint);
        }
    }
}
