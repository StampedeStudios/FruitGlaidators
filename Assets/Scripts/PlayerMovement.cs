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
        anim.SetTrigger("isDead");
    }

    private void Destroy()
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
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.down * movingStep;
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.left * movingStep;
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) & !isMoving)
        {
            isMoving = true;
            nextPosition += Vector2.right * movingStep;
            anim.SetTrigger("isJumping");
        }

        // lerp della posizione 
        transform.position = Vector2.Lerp(transform.position, nextPosition, velocity * Time.deltaTime);

        // arrotondamento della posizione e fine movimento
        if (Vector2.Distance(transform.position, nextPosition) < 0.1f)
        {
            isMoving = false;
            transform.position = nextPosition;
        }

    }

}
