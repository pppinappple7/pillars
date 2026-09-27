using UnityEngine;

public class spotlight : MonoBehaviour
{
    Manager manager;
    [SerializeField] int direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.Find("spawnManager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f,0,0)*Quaternion.AngleAxis( Mathf.Sin(Time.time*0.8f*manager.speedMult ) * 30f, Vector3.up*direction);
    }
}
