using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] allTiles, stones, chess, blocks;
    public Tile[,] tiles = new Tile[7, 7];

    // Start is called before the first frame update
    void Start()
    {
        allTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in allTiles)
        {
            Tile t = tile.GetComponent<Tile>();
            tiles[t.col, t.row] = t;
        }
        foreach (GameObject tile in allTiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.siblings = findSiblings(t.col, t.row);
        }
        stones = GameObject.FindGameObjectsWithTag("Stone");
        foreach (GameObject stone in stones)
        {
            int col = (int)Mathf.Round(stone.transform.position.x * 10) + 3;
            int row = (int)Mathf.Round(stone.transform.position.z * 10) + 3;
            tiles[col, row].onTop = stone;
        }
        chess = GameObject.FindGameObjectsWithTag("Chess");
        foreach (GameObject ch in chess)
        {
            Chess c = ch.GetComponent<Chess>();
            tiles[c.col, c.row].onTop = ch;
        }
        blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            int col = (int)Mathf.Round(block.transform.position.x * 10) + 3;
            int row = (int)Mathf.Round(block.transform.position.z * 10) + 3;
            tiles[col, row].onTop = block;
        }
    }

    public List<Tile> findSiblings(int col, int row)
    {
        List <Tile> siblings = new List<Tile>();
        if (col > 0)
        {
            siblings.Add(tiles[col - 1, row]);
        }
        if (col < 6)
        {
            siblings.Add(tiles[col + 1, row]);
        }
        if (row > 0)
        {
            siblings.Add(tiles[col, row - 1]);
        }
        if (row < 6)
        {
            siblings.Add(tiles[col, row + 1]);
        }

        return siblings;
    }
}
