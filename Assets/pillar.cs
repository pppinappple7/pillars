using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pillar : MonoBehaviour
{
    [SerializeField]Mesh initialMesh;
    MeshFilter meshFilter;
    public float moveSpeedpeed;
    float verticalOffset;
    [SerializeField] TextMeshProUGUI letterText;
    Player player;
    Manager manager;
    private static readonly System.Random random = new System.Random();
    char charecter;
    KeyCode keyCode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }
    private void OnEnable()
    {
        verticalOffset = Random.Range(-0.7f, 0.7f);
        meshFilter = GetComponent<MeshFilter>();
        player = GameObject.Find("player").GetComponent<Player>();
        manager = GameObject.Find("spawnManager").GetComponent<Manager>();
        
   
        charecter = UnusedChar();
        manager.usedChars.Add(charecter);
        
        transform.position = new Vector3(10,-25,RandomStartPos());
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*Time.deltaTime*moveSpeedpeed);
        if(transform.position.x>=-9)
        {
            if (transform.position.y < -11.5+verticalOffset)
            {
                transform.Translate(Vector3.up * 15 * Time.deltaTime);

            }
            
            else
            {
                if (Input.GetKeyDown(keyCode))
                {
                    player.curentPillar = gameObject;
                }
            }
        }
        else
        {
            transform.Translate(Vector3.up * -15 * Time.deltaTime);
            if(transform.position.y<=-25)
            {
                manager.usedChars.Remove(charecter);
                manager.usedPos.Remove((int)transform.position.z);
                gameObject.SetActive(false);
            }
        }
    }
    int RandomStartPos()
    {
        
        if (manager != null && manager.usedPos.Count >= 15)
        {
            manager.usedPos.Clear();
        }

        int maxAttempts = 100;
        int currentAttempt = 0;

        while (currentAttempt < maxAttempts)
        {
            
            
            int targetPos = Random.Range(0, 6);

            bool isOccupied = false;

            if (manager != null)
            {
                foreach (int pos in manager.usedPos)
                {
                    if (pos==targetPos)
                    {
                        isOccupied = true;
                        break;
                    }
                }
            }

            if (!isOccupied)
            {
                if (manager != null)
                {
                    manager.usedPos.Add(targetPos);
                }
                return targetPos;
            }

            currentAttempt++;
        }

        manager.usedPos.Clear();
        return Random.Range(0, 6);
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
