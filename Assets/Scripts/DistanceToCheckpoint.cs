using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceToCheckpoint : MonoBehaviour
{
    [SerializeField] private Transform checkpoint, player;
    [SerializeField] private TMP_Text distanceText;
    private float distance;
    // Update is called once per frame
    void Update()
    {
        distance = (checkpoint.transform.position - player.transform.position).magnitude;
        distanceText.text = "Distance: " + distance.ToString("F1") + "m";
    }
}
