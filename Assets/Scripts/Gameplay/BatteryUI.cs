using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryUI : MonoBehaviour
{
    [SerializeField] private BatteryManager battery;
    public TMP_Text countdownText;

    // Update is called once per frame
    public void Update()
    {
        if (battery.batteryUsed)
        {
            float seconds = Mathf.FloorToInt(battery.batteryStamina % 60);
            countdownText.text = "Battery: " + seconds.ToString() + "s";
            battery.batteryStamina -= Time.deltaTime;
        }
        else {
            countdownText.text = " ";
            battery.batteryStamina = battery.batterySpawnInterval;
        }
    }
}
