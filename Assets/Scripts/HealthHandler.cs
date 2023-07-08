using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public void Death()
    {
        // rimuovo la collisione dal frutto mangiato
        GetComponent<Collider2D>().enabled = false;
        
        // attivo l'animazione di morte
        GetComponent<Animator>().SetTrigger("isDead");
    }

    private void Destroy()
    {
        GameObject.FindObjectOfType<PlayerPossess>().OnRemoveFruit(gameObject);
    }
}
