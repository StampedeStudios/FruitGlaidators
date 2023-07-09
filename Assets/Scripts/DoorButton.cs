using UnityEngine;

public class DoorButton : MonoBehaviour
{

    public Door controlledDoor;
    private bool newState = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fruit")
        {
            newState = true;
            Invoke("DelayToggle", 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Fruit")
        {
            newState = false;
            Invoke("DelayToggle", 0.1f);
        }
    }

    private void DelayToggle()
    {
        controlledDoor.ToggleDoorStatus(newState);
    }
}
