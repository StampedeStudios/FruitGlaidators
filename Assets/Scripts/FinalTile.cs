using UnityEngine;

public class FinalTile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fruit")
        {
            PlayerPossess playerPossess = GameObject.FindObjectOfType<PlayerPossess>();
            if (playerPossess)
                if (playerPossess.companionFruit == other.gameObject)
                    playerPossess.SetVictory(VictoryEvent.FinalTile);
        }
    }
}
