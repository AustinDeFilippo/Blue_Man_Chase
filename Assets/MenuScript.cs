using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void LevelOneGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void Quit()
    {
       
        Application.Quit();

    }
}


