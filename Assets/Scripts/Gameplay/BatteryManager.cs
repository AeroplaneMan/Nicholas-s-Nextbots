using UnityEngine;
using System.Collections;
public class BatteryManager : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;   // Speed of rotation
    [SerializeField] private float hoverAmplitude = 0.1f; // Amount of hover movement
    [SerializeField] private float hoverSpeed = 2f;      // Speed of hover movement
    [SerializeField] private float batterySpawnInterval;
    [SerializeField] private GameObject battery;
    [SerializeField] private Transform spawnPos;
    private Vector3 startPosition;     // Initial position of the object
    private bool batteryUsed = false;
    private GameObject oldBattery;
    void Start()
    {
        // Record the initial position of the battery
        startPosition = spawnPos.transform.position;
        
    }

    void Update()
    {
        // Rotate the object continuously
        if (!batteryUsed) {
            battery.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); //Only running once for some reason?

            // Hover the object up and down
            float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;
            battery.transform.position = startPosition + new Vector3(0, hoverOffset, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Battery Trigger" && !batteryUsed)
        {
            batteryUsed = true;
            StartCoroutine(ResetBattery());
        }
    }

    public IEnumerator ResetBattery()
    {
        Vector3 spawnPosVector = spawnPos.position;
        oldBattery = battery;
        battery = Instantiate(battery, spawnPosVector, Quaternion.Euler(0, 0, 30));
        battery.SetActive(false);
        Destroy(oldBattery);
        yield return new WaitForSeconds(batterySpawnInterval);
        battery.SetActive(true);
        battery.transform.localScale = Vector3.one * 0.19f;
        batteryUsed = false;
    }
}