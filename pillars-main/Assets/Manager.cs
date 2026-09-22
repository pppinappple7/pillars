using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    
    [SerializeField] GameObject pause;
    [SerializeField] float spawnTime;
    [SerializeField]float spawnTimer;
    [SerializeField]GameObject[] pillarPull;
    public List<Vector3> usedPos;
    public List<char> usedChars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            pause.SetActive(true);
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            SpawnPillar();
            spawnTimer = 0;
        }
    }
    void SpawnPillar()
    {
        foreach(GameObject pillarObj in pillarPull)
        {
            if(!pillarObj.activeSelf)
            {
                pillarObj.SetActive(true);
                return;
                
            }
        }
    }
}
