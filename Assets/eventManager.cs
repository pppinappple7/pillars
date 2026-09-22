using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eventManager : MonoBehaviour
{
    Player player;
    [Serializable]class Event
    {
        
        [SerializeField]public float initialTime;
        public float time;
        public float timer;
        [SerializeField] public UnityEvent eventVoid;
    }
    [SerializeField] List<Event> events;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Player>();
        foreach (Event e in events)
        {
            e.time = e.initialTime + UnityEngine.Random.Range(-5f, 5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Event e in events)
        {
            e.timer += Time.deltaTime;
            if(e.timer>=e.time)
            {
                e.eventVoid?.Invoke();
                e.timer= 0f;
                e.time = e.initialTime + UnityEngine.Random.Range(-5f, 5f);
            }
        }

    }
    public void Camera()
    {
        GameObject[] pillars = GameObject.FindGameObjectsWithTag("pillar");
        int i = UnityEngine.Random.Range(0, pillars.Length);
        if (pillars[i].transform.position.x > -4)
        {
            pillars[i].GetComponent<pillar>().Shoot();
        }
        else
        {
            player.curentPillar.GetComponent<pillar>().Shoot();
        }
    }
    public void Train()
    {

    }
    public void Projectors()
    {
        
    }
}
