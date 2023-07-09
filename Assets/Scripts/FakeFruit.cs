using UnityEngine;

public class FakeFruit : MonoBehaviour
{
    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;

        FindObjectOfType<MenuSnakeTarget>().OnRemoveFruit(gameObject);
    }

}
