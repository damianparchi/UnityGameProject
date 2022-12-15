using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartOrExit : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level");
        
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
