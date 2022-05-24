using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public void OffPanel()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("InGame");
    }
}
