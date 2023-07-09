using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnakeHead : MonoBehaviour
{
    private List<SnakePiece> snakePieces;
    public GameObject snakePiecePrefab;

    public UIDocument victoryScreenRef;

    private GameObject targetFruit;

    private SnakePiece nextPiece;

    public int numberPieces;

    private float movingStep = 1f;

    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Quaternion currentRotation;
    private Quaternion targetRotation;

    private float elapsedTime;

    private bool isMoving = false;

    private float animLenght = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        snakePieces = new List<SnakePiece>();
        audioSource = GetComponent<AudioSource>();

        GameObject go = gameObject;
        if (snakePiecePrefab)
        {
            for (int i = 0; i < numberPieces; i++)
            {
                Vector2 prevPosition = go.transform.position;
                Vector2 newPosition = prevPosition - (Vector2)gameObject.transform.up * movingStep;

                Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, prevPosition - newPosition);

                go = Instantiate<GameObject>(snakePiecePrefab, newPosition, newRotation);

                SnakePiece snakePiece = go.GetComponent<SnakePiece>();

                snakePieces.Add(snakePiece);
                snakePiece.Init(newPosition, newRotation);
            }
        }

        nextPiece = snakePieces[0];
    }

    private void Start()
    {
        UpdatePieceReference();
    }

    private void Update()
    {
        if (!isMoving & targetFruit)
        {
            elapsedTime = 0f;
            currentPosition = transform.position;
            ChoseDirection();
            if (nextPiece)
                nextPiece.SetPieceTarget(currentPosition);
            isMoving = true;
        }

        elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0f, animLenght);

        if (isMoving)
        {
            transform.position = Vector2.Lerp(currentPosition, targetPosition, elapsedTime / animLenght);
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, elapsedTime / animLenght);
        }

        if (elapsedTime == animLenght)
        {
            isMoving = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fruit")
        {
            HealthHandler fruitHealth = other.gameObject.GetComponent<HealthHandler>();
            if (fruitHealth)
            {
                fruitHealth.Death();
                audioSource.Play();
            }
        }

        if (other.tag == "Snake")
        {
            Instantiate<UIDocument>(victoryScreenRef);
        }
    }

    public void IncrementSnakeStats(int incrementPieces, float decrementAnimTime)
    {
        for (int i = 0; i < incrementPieces; i++)
        {
            int last = snakePieces.Count - 1;
            GameObject lastPiece = snakePieces[last].gameObject;

            Vector3 newPosition = lastPiece.transform.position;
            Quaternion newRotation = lastPiece.transform.rotation;

            GameObject newPiece = Instantiate<GameObject>(snakePiecePrefab, newPosition, newRotation);

            SnakePiece snakePiece = newPiece.GetComponent<SnakePiece>();

            snakePieces.Add(snakePiece);
            snakePiece.Init(newPosition, newRotation);
        }

        animLenght -= decrementAnimTime;

        UpdatePieceReference();
    }

    private void UpdatePieceReference()
    {
        GameObject prevReference = null;
        SnakePiece nextReference = null;
        for (int i = 0; i < snakePieces.Count; i++)
        {
            if (i == 0)
                prevReference = gameObject;

            if (i == snakePieces.Count - 1)
                nextReference = null;
            else
                nextReference = snakePieces[i + 1];

            snakePieces[i].SetPieceReference(prevReference, nextReference, animLenght);

            prevReference = snakePieces[i].gameObject;
        }
    }

    public void SetNewTargetFruit(GameObject fruit)
    {
        if (fruit)
            targetFruit = fruit;
    }

    private void ChoseDirection()
    {
        float evaluateDistance = 0;
        float prevDistance = 1000;



        Vector2 evaluatePosition;

        evaluatePosition = currentPosition - (Vector2)transform.right * movingStep;
        evaluateDistance = CheckPriorityPosition(evaluatePosition);
        if (evaluateDistance < prevDistance)
        {
            prevDistance = evaluateDistance;
            targetPosition = evaluatePosition;
            targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - currentPosition);
        }

        evaluatePosition = currentPosition + (Vector2)transform.up * movingStep;
        evaluateDistance = CheckPriorityPosition(evaluatePosition);
        if (evaluateDistance < prevDistance)
        {
            prevDistance = evaluateDistance;
            targetPosition = evaluatePosition;
            targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - currentPosition);
        }

        evaluatePosition = currentPosition + (Vector2)transform.right * movingStep;
        evaluateDistance = CheckPriorityPosition(evaluatePosition);
        if (evaluateDistance < prevDistance)
        {
            prevDistance = evaluateDistance;
            targetPosition = evaluatePosition;
            targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - currentPosition);
        }
    }

    private float CheckPriorityPosition(Vector2 evaluatePosition)
    {
        RaycastHit2D hit = Physics2D.Linecast(currentPosition, evaluatePosition);

        if (hit)
        {
            if (hit.collider.tag == "Wall")
                return 1000;
            if (hit.collider.tag == "Snake")
                return 900;
        }

        Vector2 fruitPosition = targetFruit.transform.position;
        return Vector2.Distance(fruitPosition, evaluatePosition);
    }
}
