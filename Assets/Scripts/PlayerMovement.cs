using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float velocity = 20f;
    private bool isMoving = false;

    private Vector2 nextPosition;

    private float movingStep = 1f;

    void Start()
    {
        nextPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.up * movingStep;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.down * movingStep;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.left * movingStep;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.right * movingStep;
        }

        transform.position = Vector2.Lerp(transform.position, nextPosition, velocity * Time.deltaTime);
        if (Vector2.Distance(transform.position, nextPosition) < 0.01f)
        {
            transform.position = nextPosition;
            isMoving = false;
        }
    }
}
