using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuFunctions : MonoBehaviour
{

    [SerializeField] bool pause;
    private void OnEnable()
    {
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
    

}
