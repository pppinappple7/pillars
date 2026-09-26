using TMPro;
using UnityEngine;
using UnityEngine.Audio;

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
        if(initialText=="Off" || initialText=="On")
        {
            if(PlayerPrefs.GetInt("ReduceFlashes")==0 )
            {
                initialText = "Off";
            }
            else
            {
                initialText = "On";
            }
            text.text = initialText;
        }
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
    public void ChangeStatus(string playerPref)
    {
        if (initialText=="On")
        {
            initialText = "Off";
            PlayerPrefs.SetInt(playerPref, 0);
            text.text = ">" + initialText;
        }
        else
        {
            initialText = "On";
            PlayerPrefs.SetInt(playerPref, 1);
            text.text = ">" + initialText;
        }
    }
}
