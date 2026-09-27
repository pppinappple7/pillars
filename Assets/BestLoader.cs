using TMPro;
using UnityEngine;

public class BestLoader : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Best score - \n" + PlayerPrefs.GetInt("score").ToString();
    }
}
