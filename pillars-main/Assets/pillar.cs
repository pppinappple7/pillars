using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pillar : MonoBehaviour
{
    Material initialMaterial;
    [SerializeField] AudioSource shootSFX;
    [SerializeField] Material shotMaterial;
    float shotTime;
    [SerializeField] Mesh initialMesh;
    MeshRenderer meshRenderer;
    public float moveSpeed;
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
        initialMaterial = meshRenderer.material;

    }
    private void OnEnable()
    {
        shotTime = 0;
        verticalOffset = Random.Range(-0.7f, 0.7f);
        meshRenderer = GetComponent<MeshRenderer>();
        player = GameObject.Find("player").GetComponent<Player>();
        manager = GameObject.Find("spawnManager").GetComponent<Manager>();


        charecter = UnusedChar();
        manager.usedChars.Add(charecter);

        transform.position = new Vector3(11, -25, RandomStartPos());

    }
    // Update is called once per frame
    void Update()
    {
        if (shotTime > 0)
        {
            shotTime += Time.deltaTime;
            shotMaterial.SetColor("_EmissionColor", Color.white * shotTime / 1);
            if (shotTime > 1.5f)
            {
                if (player.curentPillar == gameObject)
                {
                    player.curentPillar = null;
                }

                manager.usedChars.Remove(charecter);
                manager.usedPos.Remove((int)transform.position.z);
                gameObject.SetActive(false);
                meshRenderer.material = initialMaterial;
            }
        }
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        if (transform.position.x >= -11)
        {
            if (transform.position.y < -11.5 + verticalOffset)
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
            if (player.curentPillar == gameObject)
            {
                player.curentPillar = null;
            }
            transform.Translate(Vector3.up * -15 * Time.deltaTime);
            if (transform.position.y <= -25)
            {
                manager.usedChars.Remove(charecter);
                manager.usedPos.Remove((int)transform.position.z);
                gameObject.SetActive(false);
                meshRenderer.material = initialMaterial;
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
                    if (pos == targetPos)
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

            keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), ch.ToString());
            letterText.text = ch.ToString();
            return ch;
        }
    }
    public void Shoot()
    {
        shootSFX.Play();
        shotTime += Time.deltaTime;
        meshRenderer.material = shotMaterial;
    }
}