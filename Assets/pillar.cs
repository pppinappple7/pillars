using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pillar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI letterText;
    GameObject player;
    Manager manager;
    private static readonly System.Random random = new System.Random();
    char charecter;
    KeyCode keyCode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager=GameObject.Find("manager").GetComponent<Manager>();
        charecter = UnusedChar();
        manager.usedChars.Add(UnusedChar());
        transform.position = RandomStartPos();
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y< -11.5)
        {
            transform.Translate(Vector3.up *5* Time.deltaTime);
            
        }
        else if(transform.position.y> -11.5)
        {
            transform.position = new Vector3(transform.position.x, -11.5f, transform.position.z);
        }


        else
        {
            if(Input.GetKeyDown(keyCode))
            {
                player.transform.position=new Vector3(transform.position.x,player.transform.position.y,transform.position.z);
            }
        }
    }
    Vector3 RandomStartPos()
    {
        float x = Random.Range(-2, 3) * 4;
        float z = Random.Range(0,3) * 2;
        if(manager.usedPos.Contains(new Vector3(x, -15.7f, z)))
        {
            return RandomStartPos();
        }
        else
        {
            manager.usedPos.Add(new Vector3(x, -15.7f, z));
            return new Vector3(x, -15.7f, z);
        }
            
    }
    char UnusedChar()
    {
        char ch = (char)random.Next('A', 'Z' + 1);
        if (manager.usedChars.Contains(ch))
        {
            return UnusedChar();
        }
        else
        {
            keyCode=(KeyCode)System.Enum.Parse(typeof(KeyCode),ch.ToString());
            letterText.text = ch.ToString();
            return ch;
        }
    }
}
