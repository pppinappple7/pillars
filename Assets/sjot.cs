using UnityEngine;

public class sjot : MonoBehaviour
{
    int i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if(i==2)
        {
            i = 0;
            gameObject.SetActive(false);
        }
    }
}
