using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool selectable = false;
    public GameObject onTop;

    public int col, row;
    public List<Tile> siblings;
    //private ChessMovement chessMovement;

    // Start is called before the first frame update
    void Awake()
    {
        col = (int)Mathf.Round(transform.position.x * 10) + 3;
        row = (int)Mathf.Round(transform.position.z * 10) + 3;
    }

    private void Start()
    {
        //chessMovement = FindObjectOfType<ChessMovement>(); // Find the ChessMovement script
        GetComponent<Renderer>().material.color = Color.white;
    }
}
