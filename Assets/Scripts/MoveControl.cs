using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MoveControl : MonoBehaviour
{
    public Tile selectedTile = null;
    public GameObject rotatePanel;
    public GameObject tutorialPanel;
    public GameObject tutorialTextField;
    public TMP_Text tutorialText;
    public int movedSteps;
    int tileLayerMask;
    public bool GameState;
    public int stageNo, a1, a2, a3, a;
    public string stage_a;
    public bool tutorial;
    public AudioSource moveaudio;
    public AudioSource selectaudio;
    public AudioSource gameclearaudio;
    public bool rotateState;

    public GameObject stageClearPanel;

    // Start is called before the first frame update
    void Start()
    {
        rotatePanel.SetActive(false);
        movedSteps = 0;
        tileLayerMask = LayerMask.GetMask("Tile");
        GameState = false;
        rotateState = false;

        if (tutorial)
        {
            tutorialText = tutorialTextField.GetComponent<TMP_Text>();
            tutorialText.text = "Welcome to Lumina, \nFirst of all, you need to point the camera at a flat surface (table or ground).\nThen, place the game plate using the indicator\n(Try to reposition your camera if no indicator is shown)";
            tutorialPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for touch input
        if (GameState && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is over a UI element
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayerMask))
                {
                    // Check if the touched object is a tile
                    Tile touchedTile = hit.collider.GetComponent<Tile>();

                    if (touchedTile != null)
                    {
                        if (selectedTile != null)
                        {
                            // If a tile is already selected, check if the touched tile is selectable
                            if (touchedTile.selectable)
                            {
                                // The touched tile is selectable, move the chess piece to it
                                moveOnTop(touchedTile);
                                movedSteps++;
                                deselect();
                                return;
                            }
                            else
                            {
                                // If the touched tile is not selectable, deselect the currently selected tile
                                deselect();
                                return;
                            }
                        }
                        else
                        {
                            // If no tile is currently selected, select the touched tile
                            select(touchedTile);
                            return;
                        }
                    }
                }
            }
        }
    }

    void select(Tile tile)
    {
        selectaudio.Play();
        selectedTile = tile;
        selectedTile.GetComponent<Renderer>().material.color = Color.magenta;

        if (selectedTile.onTop != null && selectedTile.onTop.CompareTag("Chess"))
        {
            if (tutorial)
            {
                tutorialText.text = "Now, you can move the mirror and rotate it using the buttons at bottom left.\nRemember, your goal is to redirect the LIGHT to the Portal.";
            }
            foreach (Tile t in selectedTile.siblings)
            {
                if (t.onTop == null)
                {
                    t.selectable = true;
                    t.GetComponent<Renderer>().material.color = Color.green;
                }
            }
            rotatePanel.SetActive(true);
        }
    }

    void deselect()
    {
        selectedTile.GetComponent<Renderer>().material.color = Color.white;
        foreach(Tile t in selectedTile.siblings)
        {
            t.selectable = false;
            t.GetComponent<Renderer>().material.color = Color.white;
        }
        selectedTile = null;
        rotatePanel.SetActive(false);
        if (tutorial)
        {
            tutorialText.text = "Try to tap on any tile under the mirrors.";
        }
    }

    void moveOnTop(Tile targetTile)
    {
        moveaudio.Play();
        selectedTile.onTop.GetComponent<Chess>().Move(targetTile.gameObject.transform.position);

        targetTile.onTop = selectedTile.onTop;
        selectedTile.onTop = null;
    }

    public void gameClear()
    {
        if (GameState)
        {
            if ((stageNo + 1) > PlayerPrefs.GetInt("stage", 0))
            {
                PlayerPrefs.SetInt("stage", stageNo + 1);
            }
        }
        GameState = false;
        gameclearaudio.Play();
        stageClearPanel.SetActive(true);

        if (movedSteps <= a3)
        {
            a = 3;
        }
        else if (movedSteps <= a2)
        {
            a = 2;
        }
        else if (movedSteps <= a1)
        {
            a = 1;
        }
        else a = 0;

        Debug.Log(a);

        if (a > PlayerPrefs.GetInt(stage_a, 0))
        {
            PlayerPrefs.SetInt(stage_a, a);
        }
        Debug.Log("Fetched: " + PlayerPrefs.GetInt(stage_a, 0));
    }
}
