using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public float speedMult = 1f;
    float timeOffset;
    [SerializeField] GameObject pause;
    [SerializeField] float spawnTime;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject[] pillarPull;
    public List<int> usedPos;
    public List<char> usedChars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        speedMult += Time.deltaTime / 480;
        if (Input.GetKey(KeyCode.Escape))
        {
            pause.SetActive(true);
        }
        spawnTimer += speedMult*Time.deltaTime;
        if (spawnTimer > spawnTime+timeOffset)
        {
            
            
            SpawnPillar();
            spawnTimer = 0;
            timeOffset = Random.Range(-0.5f * spawnTime, 0.2f * spawnTime);
        }
    }
    void SpawnPillar()
    {
        foreach (GameObject pillarObj in pillarPull)
        {
            if (!pillarObj.activeSelf)
            {
                pillarObj.SetActive(true);
                return;

            }
        }
    }
}