using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerPossess : MonoBehaviour
{
    private List<GameObject> fruits;

    public GameObject possessedFruit;

    private SnakeHead snake;

    private GameObject target;

    public GameObject targetPrefab;

    public UIDocument defeathScreenRef;

    private void Awake()
    {
        // ricerca frutti nella scena
        fruits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Fruit"));
        snake = GameObject.FindObjectOfType<SnakeHead>();
    }

    private void Start()
    {
        // possesso frutto di partenza
        OnPossess(possessedFruit);
    }

    private void Update()
    {
        if (target & possessedFruit)
            target.transform.position = possessedFruit.transform.position;

    }

    public void OnRemoveFruit(GameObject self)
    {
        // rimuovo frutto dalla lista
        if (fruits.Contains(self))
            fruits.Remove(self);

        // aggiungo nuovi pezzi al serpente e velocita
        snake.IncrementSnakeStats(1, 0.05f);

        // termino se non ci sono piu frutti
        if (fruits.Count == 0)
        {
            Destroy(self);
            Destroy(target);
            Instantiate<UIDocument>(defeathScreenRef);
            Debug.Log("End Game!");
            return;
        }

        // ricerco nuovo frutto e distruggo il precedente
        if (self == possessedFruit)
        {
            GameObject newPossessedFruit = GetNextFruit(self);
            if (possessedFruit)
                OnPossess(possessedFruit);
        }

        Destroy(self);
    }

    private void OnPossess(GameObject newFruit)
    {
        possessedFruit = newFruit;

        if (!target)
            target = Instantiate<GameObject>(targetPrefab);

        target.transform.position = possessedFruit.transform.position;

        // acquisisco controllo del nuovo frutto
        PlayerMovement playerMovement;
        playerMovement = possessedFruit.GetComponent<PlayerMovement>();

        if (playerMovement)
        {
            playerMovement.enabled = true;
            snake.SetNewTargetFruit(possessedFruit);
        }

        // aquisisco il renderer e imposto il layer del frutto piu alto
        SpriteRenderer renderer = possessedFruit.GetComponent<SpriteRenderer>();

        if (renderer)
            renderer.sortingOrder++;

    }

    GameObject GetNextFruit(GameObject lastFruit)
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
        return possessedFruit;
    }


}
