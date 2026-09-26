using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuFunctions : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI volumeText;
    [SerializeField] bool pause;
    private void Start()
    {
        if(audioMixer != null)
        {
            if(PlayerPrefs.GetFloat("volume",1f)!=0)
            {
                audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-50, 0, PlayerPrefs.GetFloat("volume", 1f)));
            }
            else
            {
                audioMixer.SetFloat("MasterVolume",-80);
            }
            
        }
        
    }
    private void OnEnable()
    {
        if(slider!=null)
        {
            slider.value = PlayerPrefs.GetFloat("volume") * 100;
        }
       
        if (pause)
        {
            Time.timeScale = 0;
        }
        
    }
    public void ChangeScene(int scene)
    { 
         SceneManager.LoadScene(scene);
    }
    public void LeaveGame()
    {
         Application.Quit();
    }
    public void Continue()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void ResetPP(string name)
    {
        PlayerPrefs.SetInt(name, 0);
    }
    public void changeVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume / 100);
        volumeText.text = volume.ToString();
        if (audioMixer != null)
        {
            if (PlayerPrefs.GetFloat("volume", 1f) != 0)
            {
                audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-50, 0, PlayerPrefs.GetFloat("volume", 1f)));
            }
            else
            {
                audioMixer.SetFloat("MasterVolume", -80);
            }
        }
    }
    public void setActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
