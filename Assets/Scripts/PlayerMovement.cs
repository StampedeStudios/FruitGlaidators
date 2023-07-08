using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float velocity = 10f;
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
        Invoke("Death", 5f); // autodestroy per test
    }

    private void Death()
    {
        GameObject.FindObjectOfType<PlayerPossess>().OnRemoveFruit(gameObject);
    }

    private void Update()
    {
        // movimento del player nelle 4 direzioni
        if (Input.GetKeyDown(KeyCode.UpArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.up * movingStep;
            anim.SetBool("isJumping", isMoving);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.down * movingStep;
            anim.SetBool("isJumping", isMoving);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.left * movingStep;
            anim.SetBool("isJumping", isMoving);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.right * movingStep;
            anim.SetBool("isJumping", isMoving);
        }

        // lerp della posizione 
        transform.position = Vector2.Lerp(transform.position, nextPosition, velocity * Time.deltaTime);

        // arrotondamento della posizione e fine movimento
        if (Vector2.Distance(transform.position, nextPosition) < 0.1f)
        {
            isMoving = false;
            transform.position = nextPosition;
            anim.SetBool("isJumping", isMoving);
        }

    }


}
