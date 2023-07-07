using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float velocity = 20f;
    private bool isMoving = false;

    private Vector2 nextPosition;

    private float movingStep = 1f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        nextPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.up * movingStep;
            anim.SetInteger("direction", 12);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.down * movingStep;
            anim.SetInteger("direction", 6);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.left * movingStep;
            anim.SetInteger("direction", 9);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.right * movingStep;
            anim.SetInteger("direction", 3);
        }

        transform.position = Vector2.Lerp(transform.position, nextPosition, velocity * Time.deltaTime);
        if (Vector2.Distance(transform.position, nextPosition) < 0.01f)
        {
            transform.position = nextPosition;
            anim.SetInteger("direction", 0);
            isMoving = false;
        }
    }


}
