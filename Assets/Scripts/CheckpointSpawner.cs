using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject checkpoint;
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minZPos;
    [SerializeField] private float maxZPos;

    // Update is called once per frame
    void OnTriggerEnter()
    {
        Instantiate(checkpoint, new Vector3(Random.Range(minXPos, maxXPos), 0.58f, Random.Range(minZPos, maxZPos)), Quaternion.identity);
        Destroy(checkpoint);
    }
}
