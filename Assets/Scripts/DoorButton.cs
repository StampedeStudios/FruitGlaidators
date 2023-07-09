using UnityEngine;

public class DoorButton : MonoBehaviour
{

    public Door controlledDoor;
    private bool newState = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fruit")
        {
            newState = !newState;
            if (!controlledDoor.ToggleDoorStatus(newState))
                newState = !newState;
        }
    }

}
