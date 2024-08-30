using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{

    public GameObject muteButton;
    public GameObject unmuteButton;
    public AudioSource bgm;
    public AudioSource click;

    // Start is called before the first frame update
    void Start()
    {
        muteButton.GetComponent<Button>().onClick.AddListener(Mute);
        unmuteButton.GetComponent<Button>().onClick.AddListener(Unmute);

        int s = PlayerPrefs.GetInt("bgm", 0);
        if (s == 0)
        {
            bgm.Play();
            unmuteButton.SetActive(false);
        }
        else
        {
            unmuteButton.SetActive(true);
            muteButton.SetActive(false);
            bgm.Stop();
        }
    }

    void Mute()
    {
        click.Play();
        bgm.Stop();
        PlayerPrefs.SetInt("bgm", 1);
        unmuteButton.SetActive(true);
        muteButton.SetActive(false);
    }

    void Unmute()
    {
        click.Play();
        bgm.Play();
        PlayerPrefs.SetInt("bgm", 0);
        unmuteButton.SetActive(false);
        muteButton.SetActive(true);
    }
}
