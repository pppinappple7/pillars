using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]GameObject pillar;
    public List<Vector3> usedPos;
    public List<char> usedChars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnPillar()
    {
        Instantiate(pillar);
    }
}
