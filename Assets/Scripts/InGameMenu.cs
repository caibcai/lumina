using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject pausepanel;

    // Start is called before the first frame update
    void Start()
    {
        pausepanel.SetActive(false);
    }

    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void Pause()
    {
        pausepanel.SetActive(true);
    }

    public void Resume()
    {
        pausepanel.SetActive(false);
    }
}
