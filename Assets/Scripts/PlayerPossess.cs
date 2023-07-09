using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerPossess : MonoBehaviour
{
    private List<GameObject> fruits;

    public GameObject possessedFruit;

    public GameObject companionFruit;

    private SnakeHead snake;

    private GameObject target;

    private GameObject companionTarget;

    public GameObject targetPrefab;
    public GameObject companionTargetPrefab;

    public UIDocument defeatScreenRef;
    public UIDocument victoryScreenRef;



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

        if (companionFruit)
        {
            fruits.Remove(companionFruit);
            companionFruit.GetComponent<PlayerMovement>().SetPosssessionType(PossessionType.IsCompanion);

            if (!companionTarget)
            {
                companionTarget = Instantiate<GameObject>(companionTargetPrefab);
                companionTarget.transform.position = companionFruit.transform.position;
            }
        }
    }

    private void Update()
    {
        if (target & possessedFruit)
            target.transform.position = possessedFruit.transform.position;

        if (companionTarget & companionFruit)
            companionTarget.transform.position = companionFruit.transform.position;

        if (Input.GetKeyDown(KeyCode.Space) & fruits.Count > 1)
        {
            possessedFruit.GetComponent<PlayerMovement>().SetPosssessionType(PossessionType.None);
            GameObject newPossessedFruit = GetNextFruit(possessedFruit);
            if (newPossessedFruit)
                OnPossess(newPossessedFruit);
        }
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
            Instantiate<UIDocument>(defeatScreenRef);
            return;
        }

        // ricerco nuovo frutto e distruggo il precedente
        if (self == possessedFruit)
        {
            GameObject newPossessedFruit = GetNextFruit(self);
            if (newPossessedFruit)
                OnPossess(newPossessedFruit);
        }

        Destroy(self);
    }

    public void SetVictory(VictoryEvent victoryEvent)
    {
        switch (victoryEvent)
        {
            case VictoryEvent.SnakeDeath:
                UnpossessFruits();
                Instantiate<UIDocument>(victoryScreenRef);
                return;

            case VictoryEvent.FinalTile:
                UnpossessFruits();
                snake.SnakeDeath();
                Instantiate<UIDocument>(victoryScreenRef);
                return;


            default: return;
        }
    }

    public void UnpossessFruits()
    {
        if (companionFruit)
            companionFruit.GetComponent<PlayerMovement>().SetPosssessionType(PossessionType.None);

        if (possessedFruit)
            possessedFruit.GetComponent<PlayerMovement>().SetPosssessionType(PossessionType.None);
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
            playerMovement.SetPosssessionType(PossessionType.IsPlayer);
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
        GameObject nextFruit = null;

        foreach (var item in fruits)
        {
            if (item == lastFruit)
                continue;
            Vector2 a = item.transform.position;
            Vector2 b = lastFruit.transform.position;
            float fruitDistance = Vector2.Distance(a, b);

            if (fruitDistance < minDistance)
            {
                minDistance = fruitDistance;
                nextFruit = item;
            }
        }
        return nextFruit;
    }


}
