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

    private bool isPossessed = false;

    private PossessionType possessionType = PossessionType.None;

    private GameObject companionFruit;

    private bool hasPlayerInput = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch (possessionType)
        {
            case PossessionType.None:
                Move();
                return;

            case PossessionType.IsPlayer:
                // movimento del player nelle 4 direzioni
                if (Input.GetKeyDown(KeyCode.UpArrow) & !isMoving)
                {
                    elapsedTime = 0;
                    currentPosition = transform.position;
                    nextPosition = currentPosition + Vector2.up * movingStep;

                    if (IsObstructed())
                        return;

                    if (companionFruit)
                        companionFruit.GetComponent<PlayerMovement>().UpdateCompanion(Vector2.up);

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

                    if (companionFruit)
                        companionFruit.GetComponent<PlayerMovement>().UpdateCompanion(Vector2.down);

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

                    if (companionFruit)
                        companionFruit.GetComponent<PlayerMovement>().UpdateCompanion(Vector2.left);

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

                    if (companionFruit)
                        companionFruit.GetComponent<PlayerMovement>().UpdateCompanion(Vector2.right);

                    isMoving = true;
                    anim.SetTrigger("isJumping");
                }
                Move();
                return;

            case PossessionType.IsCompanion:

                if (IsObstructed())
                    return;

                if (hasPlayerInput & !isMoving)
                {
                    hasPlayerInput = false;
                    isMoving = true;
                    anim.SetTrigger("isJumping");
                }
                Move();
                return;

            default: return;
        }
    }

    public void UpdateCompanion(Vector2 direction)
    {
        elapsedTime = 0;
        currentPosition = transform.position;
        nextPosition = currentPosition + direction * movingStep;
        hasPlayerInput = true;
    }

    private void Move()
    {
        elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0f, animLenght);

        if (isMoving)
            transform.position = Vector2.Lerp(currentPosition, nextPosition, elapsedTime / animLenght);

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

    public void TogglePossession(bool possessActive)
    {
        isPossessed = possessActive;
    }

    public void SetPosssessionType(PossessionType newPossessionType)
    {
        possessionType = newPossessionType;

        if (possessionType == PossessionType.IsPlayer)
            companionFruit = GameObject.FindObjectOfType<PlayerPossess>().companionFruit;
    }

}
