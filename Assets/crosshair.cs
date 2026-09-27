using UnityEngine;

public class crosshair : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float beepTime;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        beepTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        beepTime += Time.deltaTime;
        if (beepTime >= 0.25f)
        {
            if (spriteRenderer.enabled == true)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }

            beepTime = 0;
        }
    }
}
