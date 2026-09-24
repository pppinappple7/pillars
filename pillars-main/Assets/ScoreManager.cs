using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    bool counting =true;
    public float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = ((int)score).ToString();
        if(counting)
        {
            score += Time.deltaTime * 10f;
        }
        
    }
    public void StopCount()
    {
        counting = false;
        PlayerPrefs.SetInt("score", (int)score);
    }
}
