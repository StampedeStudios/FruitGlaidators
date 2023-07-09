using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float animLenght = 0.15f;
    private bool isMoving = false;

    private Vector2 nextPosition;
    private Vector2 currentPosition;

    private float movingStep = 1f;

    private Animator anim;

    private float elapsedTime;

    private AudioSource audioSource;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // movimento del player nelle 4 direzioni
        if (Input.GetKeyDown(KeyCode.UpArrow) & !isMoving)
        {
            elapsedTime = 0;
            currentPosition = transform.position;
            nextPosition = currentPosition + Vector2.up * movingStep;

            if (IsObstructed())
                return;

            isMoving = true;
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) & !isMoving)
        {
            elapsedTime = 0;
            currentPosition = transform.position;
            nextPosition = currentPosition + Vector2.down * movingStep;

            if (IsObstructed())
                return;

            isMoving = true;
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) & !isMoving)
        {
            elapsedTime = 0;
            currentPosition = transform.position;
            nextPosition = currentPosition + Vector2.left * movingStep;

            if (IsObstructed())
                return;

            isMoving = true;
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) & !isMoving)
        {
            elapsedTime = 0;
            currentPosition = transform.position;
            nextPosition = currentPosition + Vector2.right * movingStep;

            if (IsObstructed())
                return;

            isMoving = true;
            anim.SetTrigger("isJumping");
        }

        elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0f, animLenght);

        if (isMoving)
        {
            // lerp della posizione 
            transform.position = Vector2.Lerp(currentPosition, nextPosition, elapsedTime / animLenght);
        }
        if (elapsedTime == animLenght)
        {
            isMoving = false;
        }
    }

    private bool IsObstructed()
    {
        return Physics2D.Linecast(currentPosition, nextPosition, 3);
    }

    private void PlaySound()
    {
        audioSource.Play();
    }

}
