using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gameover;
    [SerializeField] GameObject[] disableOnLoose;
    [SerializeField] ScoreManager scoreManager;
    Rigidbody rb;
    public GameObject curentPillar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (curentPillar != null)
        {
            transform.position = curentPillar.transform.position + 9.2f * Vector3.up;
        }
        else
        {
            foreach (GameObject obj in disableOnLoose)
            {
                obj.SetActive(false);
            }
            gameover.SetActive(true);
            scoreManager.StopCount();
            rb.constraints= RigidbodyConstraints.None;
            rb.AddForce(new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), ForceMode.Impulse);
            rb.AddTorque(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
            enabled = false;

            
        }
    }
}
