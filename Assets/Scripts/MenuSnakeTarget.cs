using UnityEngine;
using System.Collections.Generic;

public class MenuSnakeTarget : MonoBehaviour
{
    private List<GameObject> fruits;
    private List<GameObject> eatenFruits;

    private GameObject possessedFruit;

    private SnakeHead snake;

    private void Awake()
    {
        // ricerca frutti nella scena
        fruits = new List<GameObject>(GameObject.FindGameObjectsWithTag("FakeFruit"));
        eatenFruits = new List<GameObject>(fruits);
        snake = GameObject.FindObjectOfType<SnakeHead>();
        OnPossess(fruits[0]);
    }

    public void OnRemoveFruit(GameObject self)
    {
        eatenFruits.Remove(self);

        if (eatenFruits.Count == 0)
        {
            eatenFruits = new List<GameObject>(fruits);
        }

        GameObject newPossessedFruit = GetNextFruit();
        OnPossess(newPossessedFruit);
    }

    private void OnPossess(GameObject newFruit)
    {
        possessedFruit = newFruit;
        possessedFruit.GetComponent<Collider2D>().enabled = true;
        snake.SetNewTargetFruit(possessedFruit);
    }

    GameObject GetNextFruit()
    {
        int random = Random.Range(0, eatenFruits.Count);

        return eatenFruits[random];
    }

}
