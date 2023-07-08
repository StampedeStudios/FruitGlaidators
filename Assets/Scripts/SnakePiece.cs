using UnityEngine;

public class SnakePiece : MonoBehaviour
{
    private GameObject prevPiece;
    private SnakePiece nextPiece;

    private float rotVelocity = 30f;

    private Vector2 targetPosition;

    private Vector2 currentPosition;

    private float elapsedTime;

    private float animLenght;


    public void Init(Vector3 initPosition, Quaternion initRotation)
    {
        gameObject.transform.SetPositionAndRotation(initPosition, initRotation);
        currentPosition = initPosition;
        targetPosition = initPosition;
    }

    private void Update()
    {
        elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0f, animLenght);

        // lerp della posizione 
        transform.position = Vector2.Lerp(currentPosition, targetPosition, elapsedTime / animLenght);

        // lerp della rotazione 
        Vector2 dir = prevPiece.transform.position - transform.position;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotVelocity * Time.deltaTime);
    }

    public void SetPieceReference(GameObject prevReference, SnakePiece nextReference, float animationLenght)
    {
        prevPiece = prevReference;
        animLenght = animationLenght;
        nextPiece = nextReference;
    }

    public void SetPieceTarget(Vector2 newPosition)
    {
        currentPosition = targetPosition;
        targetPosition = newPosition;

        elapsedTime = 0;

        if (nextPiece)
            nextPiece.SetPieceTarget(currentPosition);

    }
}
