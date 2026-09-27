using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] GameObject gameCamera;
    float dashTimer=0;
    public Animator animator;
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

        if(animator.GetBool("dash")==true)
        {
            dashTimer += Time.deltaTime;
            if(dashTimer>0.5f)
            {
                dashTimer = 0;
                animator.SetBool("dash", false);
            }
        }
        if (curentPillar != null)
        {
            transform.position = curentPillar.transform.position + 9.2f * Vector3.up;
        }
        else
        {
            transform.LookAt(gameCamera.transform.position);
            animator.SetBool("dead",true);
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
