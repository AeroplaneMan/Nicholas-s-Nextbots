using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class CollisionDetection : MonoBehaviour
{

    public bool Dead;
    Rigidbody playerRB;
    PlayerMovement playerMove;
    Sliding sliding;
    AudioSource audioSource;
    [SerializeField] private RespawnUI respawnUI;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private CamShakeTransform camShake;
    [SerializeField] private CamShakeEventData data;
    [SerializeField] private Camera cam;
    public float camFOVIn;
    [SerializeField] private float camFOVOut;
    [HideInInspector] public float countdown = 0f;
    [SerializeField] private GameObject restartScreen;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        sliding = GetComponent<Sliding>();
        playerMove = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bot" && !Dead)
        {
            Dead = true;
            restartScreen.SetActive(true);
            transform.position = spawnPos.position;
            StartCoroutine(ResetPlayer(other.transform));
            audioSource.Play();

        }
    }
    
    IEnumerator ResetPlayer(Transform enemy)
    {
        Dead = true;
        yield return null;
        playerMove.Die();
        sliding.Die();
        //playerRB.AddRelativeTorque(Random.insideUnitSphere * 1000, ForceMode.Impulse);
        //playerRB.AddExplosionForce(50, enemy.position, 10f, 1f, ForceMode.Impulse);

        float elapsed = 0;
        countdown = camFOVIn + 1;

        while (elapsed < camFOVIn)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 135f, elapsed / camFOVIn);
            elapsed += Time.deltaTime;
            respawnUI.Update();
            yield return null;
        }

        elapsed = 0;
        while (elapsed < camFOVOut || cam.fieldOfView != 60)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, elapsed / camFOVOut);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Dead = false;
        restartScreen.SetActive(false);
        playerMove.Respawn();
    }
}
