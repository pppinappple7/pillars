using TMPro;
using UnityEngine;

public class MenuOption : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource audioSource;
    string initialText;
    [SerializeField] TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialText=text.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Enter()
    {
        text.text = ">" + initialText;
        text.fontStyle = FontStyles.Italic;
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }
    public void Exit()
    {
        text.text = initialText;
        text.fontStyle = FontStyles.Normal;
    }

}
