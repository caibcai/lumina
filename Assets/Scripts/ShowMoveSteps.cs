using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowMoveSteps : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public MoveControl movecontrol;

    // Start is called before the first frame update
    void Start()
    {
        movecontrol = FindObjectOfType<MoveControl>();
        textMeshPro = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Moves: " + movecontrol.movedSteps + " / " + movecontrol.a1 +
            "\nMoves: " + movecontrol.movedSteps + " / " + movecontrol.a2 +
            "\nMoves: " + movecontrol.movedSteps + " / " + movecontrol.a3;
    }
}
