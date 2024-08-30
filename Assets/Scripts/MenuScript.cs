using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject menupanel;
    public GameObject infopanel;
    public GameObject stagepanel;
    public GameObject stageinfopanel;
    public GameObject gamedescription;
    public GameObject[] stars;
    public GameObject[] buttonhovers;
    public string selectedStage;
    public string[] description = new string[10];
    public string[] help = new string[3];
    public TMP_Text textMeshPro;
    public TMP_Text infoText;
    public int infoNum;

    // Start is called before the first frame update
    void Start()
    {
        if(menupanel != null)
        {
            menupanel.SetActive(true);
            stagepanel.SetActive(false);
        }
        textMeshPro = gamedescription.GetComponent<TMP_Text>();

        int s = PlayerPrefs.GetInt("stage", 0);
        for (int i = 0; i <= s && i <= 9; i++)
        {
            buttonhovers[i].SetActive(false);
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectScene(string scenename) 
    {
        foreach(GameObject star in stars)
        {
            star.SetActive(false);
        }
        int s = PlayerPrefs.GetInt("stage", 0);
        for (int i = 0; i <= s && i <= 9; i++)
        {
            buttonhovers[i].SetActive(false);
        }
        stageinfopanel.SetActive(true);
        selectedStage = scenename;
        int stageNo = scenename[^1] - '0' ;
        textMeshPro.text = description[stageNo];

        int a = PlayerPrefs.GetInt(scenename, 0);
        for(int i = 0; i < a; i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(selectedStage);
    }

    public void StartButton()
    {
        menupanel.SetActive(false);
        stagepanel.SetActive(true);
    }

    public void InfoButtion()
    {
        infopanel.SetActive(true);
        infoNum = 0;
        infoText.text = help[0];
    }

    public void BackButton()
    {
        menupanel.SetActive(true);
        stagepanel.SetActive(false);
    }

    public void SurveyButton()
    {
        Application.OpenURL("https://forms.gle/LKyEWoRgtZnCNVpN6");
    }

    public void InfoBackButton()
    {
        infopanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TestMenu(int stageNo)
    {
        PlayerPrefs.SetInt("stage", stageNo + 1);
    }

    public void NextInfo()
    {
        if(infoNum < 2)
        {
            infoNum++;
            infoText.text = help[infoNum];
        }
        else
        {
            infoNum = 0;
            infoText.text = help[0];
        }
    }

    public void PrevInfo()
    {
        if (infoNum > 0)
        {
            infoNum--;
            infoText.text = help[infoNum];
        }
        else
        {
            infoNum = 2;
            infoText.text = help[infoNum];
        }
    }
}
