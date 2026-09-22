using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject curentPillar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curentPillar != null)
        {
            transform.position = curentPillar.transform.position + 9.2f * Vector3.up;
        }
    }
}
