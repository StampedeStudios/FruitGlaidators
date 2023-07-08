using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public void Death()
    {
        GetComponent<Animator>().SetTrigger("isDead");
    }

    private void Destroy()
    {
        GameObject.FindObjectOfType<PlayerPossess>().OnRemoveFruit(gameObject);
    }
}
