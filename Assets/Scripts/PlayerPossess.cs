using UnityEngine;
using System.Collections.Generic;

public class PlayerPossess : MonoBehaviour
{
    private List<GameObject> fruits;

    public GameObject possessedFruit;

    private void Awake()
    {
        // ricerca frutti nella scena
        fruits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Fruit"));
    }

    private void Start()
    {
        // possesso frutto di partenza
        OnPossess(possessedFruit);
    }

    public void OnRemoveFruit(GameObject self)
    {
        // rimuovo frutto dalla lista
        if (fruits.Contains(self))
            fruits.Remove(self);

        // termino se non ci sono piu frutti
        if (fruits.Count == 0)
        {
            Destroy(self);
            Debug.Log("End Game!");
            return;
        }

        // ricerco nuovo frutto e distruggo il precedente
        if (GetNextFruit(self))
        {
            Destroy(self);
            OnPossess(possessedFruit);
        }
        else
        {
            Destroy(self);
            Debug.Log("End Game!");
        }
    }

    private void OnPossess(GameObject newFruit)
    {
        // acquisisco controllo del nuovo frutto
        PlayerMovement playerMovement;
        playerMovement = newFruit.GetComponent<PlayerMovement>();

        if (playerMovement)
            playerMovement.enabled = true;
    }

    bool GetNextFruit(GameObject lastFruit)
    {
        // ricerco nuovo frutto nella lista scegliendo quello piu vicino
        float minDistance = 1000f;
        possessedFruit = null;

        foreach (var item in fruits)
        {
            Vector2 a = item.transform.position;
            Vector2 b = lastFruit.transform.position;
            float fruitDistance = Vector2.Distance(a, b);

            if (fruitDistance < minDistance)
            {
                minDistance = fruitDistance;
                possessedFruit = item;
            }
        }

        if (possessedFruit)
            return true;
        else
            return true;
    }


}
