using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceToDoor : MonoBehaviour
{
    [SerializeField] private Transform door, player;
    [SerializeField] private GameObject levelCompleteCheck;
    [SerializeField] private TMP_Text distanceText;
    private float distance;
    // Update is called once per frame
    void Update()
    {
        distanceText.text = levelCompleteCheck.GetComponent<LevelCompleteCheck>().numCheckpoints.ToString() + " / " 
            + levelCompleteCheck.GetComponent<LevelCompleteCheck>().checkpointsToComplete.ToString() + " Checkpoints";
        if (levelCompleteCheck.GetComponent<LevelCompleteCheck>().levelCompleted)
        {
            distance = (door.transform.position - player.transform.position).magnitude;
            distanceText.text = "THE FINAL STRETCH: " + distance.ToString("F1") + "m";
        }
    }
}
