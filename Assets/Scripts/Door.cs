using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioSource audioSource;
    private Collider2D doorCollider;
    private Vector2 closedPosition;
    private Vector2 targetPosition;

    public GameObject door;


    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        closedPosition = transform.position;
        targetPosition = closedPosition;
    }

    public void ToggleDoorStatus(bool isOpen)
    {
        if (isOpen)
        {
            doorCollider.enabled = false;
            audioSource.Play();
            targetPosition = closedPosition + (Vector2)transform.up * 1.7f;
        }
        else
        {
            if (IsObstructed())
                return;
            doorCollider.enabled = true;
            audioSource.Play();
            targetPosition = closedPosition;
        }

    }

    private void Update()
    {

        if (Vector2.Distance(door.transform.position, targetPosition) > 0.01f)
            door.transform.position = Vector2.Lerp(door.transform.position, targetPosition, 20f * Time.deltaTime);
        else
            door.transform.position = targetPosition;
    }


    private bool IsObstructed()
    {
        Vector2 start = transform.position;
        Vector2 end = start + Vector2.down * 2f;
        Vector2 size = new Vector2(1, 4);

        int layerMask = LayerMask.GetMask("Player");
        return Physics2D.BoxCast(transform.position, size, 0f, Vector2.up, 0f, layerMask);
        // return Physics2D.Linecast(start, end, layerMask);
    }
}
