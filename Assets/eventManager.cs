using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eventManager : MonoBehaviour
{
    float trainTime;
    [SerializeField] GameObject trainObj;
    [SerializeField] LineRenderer trainLine;
    [SerializeField] AudioSource shootSfx;
    [SerializeField] GameObject globalLight;
    [SerializeField] GameObject projectors;
    Manager manager;
    float projectorsTime=0;
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
        manager = GameObject.Find("spawnManager").GetComponent<Manager>();
        player = GameObject.Find("player").GetComponent<Player>();
        foreach (Event e in events)
        {
            e.time = e.initialTime + UnityEngine.Random.Range(-5f, 5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(trainTime>0)
        {
            trainTime += Time.deltaTime;
        }
        
        if (trainTime>6)
        {
            trainObj.SetActive(false);
            trainTime = 0;
            trainLine.gameObject.SetActive(false);
        }
        if(projectors.activeSelf)
        {
            projectorsTime += Time.deltaTime * manager.speedMult;
            if(projectorsTime>=8)
            {
                projectors.SetActive(false);
                globalLight.SetActive(true);
            }
        }
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
    public void Guns()
    {
        shootSfx.Play();
        GameObject[] pillars = GameObject.FindGameObjectsWithTag("pillar");
        int i = UnityEngine.Random.Range(0, pillars.Length);
        if (pillars[i].GetComponent<pillar>().shootTime==0)
        {
            pillars[i].GetComponent<pillar>().Gun();
        }
        else
        {
            i = UnityEngine.Random.Range(0, pillars.Length);
            pillars[i].GetComponent<pillar>().Gun();
        }
        i = UnityEngine.Random.Range(0, pillars.Length);
        if (pillars[i].GetComponent<pillar>().shootTime == 0)
        {
            pillars[i].GetComponent<pillar>().Gun();
        }
        else
        {
            i = UnityEngine.Random.Range(0, pillars.Length);
            pillars[i].GetComponent<pillar>().Gun();
        }
    }
    public void Projectors()
    {
        projectors.SetActive(true);
        globalLight.SetActive(false);
    }
    public void train()
    {
        trainTime += Time.deltaTime;
        List<GameObject> pillars = new List<GameObject>(GameObject.FindGameObjectsWithTag("pillar"));
        int i = UnityEngine.Random.Range(0, pillars.Count);
        Vector3 point1 = pillars[i].transform.position;
        point1.y = 0;
        pillars.Remove(pillars[1]);
        i = UnityEngine.Random.Range(0, pillars.Count);
        Vector3 point2 = pillars[i].transform.position;
        point2.y = 0;
        Vector3 truePoint1 = (point2 - point1) * 11;
        Vector3 truePoint2 = (point1 - point2) * 11;
        trainLine.gameObject.SetActive(true);
        trainLine.SetPosition(0, truePoint1);
        trainLine.SetPosition(1, truePoint2);
        trainObj.SetActive(true);
        trainObj.transform.position = truePoint1;
        trainObj.transform.LookAt(truePoint2);
        //trainObj.transform.eulerAngles=new Vector3(0,transform.eulerAngles.y,0);
    }
}
