using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RespawnUI : MonoBehaviour
{
    [SerializeField] private CollisionDetection collide;
    public TMP_Text countdownText;

    // Update is called once per frame
    public void Update()
    {
        if (collide.Dead)
        {
            float seconds = Mathf.FloorToInt(collide.countdown % 60);
            countdownText.text = seconds.ToString();
            collide.countdown -= Time.deltaTime * 0.5f;
        }

        else countdownText.text = " ";
    }
}
