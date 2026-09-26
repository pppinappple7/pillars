using UnityEngine;

public class key : MonoBehaviour
{
    [SerializeField] string letter;
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource audioSource;
    KeyCode keyCode;
    void Start()
    {
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), letter);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y<0)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            if(transform.localPosition.y>0)
            {
                transform.localPosition = Vector3.zero;
            }
        }
        if(Input.GetKeyDown(keyCode))
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
            transform.localPosition = new Vector3(0, -0.15f, 0);
        }
    }
}
