// Chess.cs Script
using System.Collections;
using UnityEngine;

public class Chess : MonoBehaviour
{
    private Vector3 targetPosition;
    public int col, row;
    public float moveSpeed = 5f; // Speed of the movement

    private void Awake()
    {
        col = (int)Mathf.Round(transform.position.x * 10) + 3;
        row = (int)Mathf.Round(transform.position.z * 10) + 3;
    }

    void Start()
    {
    }

    void Update()
    {

    }

    //public void Move(Vector3 targetPosition)
    //{
    //    // Move the chess piece to the target position
    //    this.targetPosition = targetPosition;
    //    targetPosition.y = transform.position.y;
    //    // Perform the movement animation or teleport the chess piece to the target position
    //    transform.position = targetPosition;

    //    col = (int)Mathf.Round(transform.position.x * 10) + 3;
    //    row = (int)Mathf.Round(transform.position.z * 10) + 3;
    //}

    public void Move(Vector3 targetPosition)
    {
        // Ensure the target position has the same y as the current position
        targetPosition.y = transform.position.y;

        // Start the movement coroutine
        StartCoroutine(MoveToPosition(targetPosition));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        // Get the starting position
        Vector3 startPosition = transform.position;

        // Calculate the distance to move
        float distance = Vector3.Distance(startPosition, targetPosition);

        // Move the chess piece over time
        float elapsedTime = 0;
        while (elapsedTime < distance / moveSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime * moveSpeed) / distance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set
        transform.position = targetPosition;

        // Update the row and column based on the new position
        col = (int)Mathf.Round(transform.position.x * 10) + 3;
        row = (int)Mathf.Round(transform.position.z * 10) + 3;
    }
}
