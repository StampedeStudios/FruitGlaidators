using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class VictoryHandler : MonoBehaviour
{
    private UIDocument victoryScreenRef;

    void Awake()
    {
        victoryScreenRef = GetComponent<UIDocument>();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        VisualElement root = victoryScreenRef.rootVisualElement;
        Button menuButton = root.Q<Button>("NextLvlBtn");
        menuButton.RegisterCallback<ClickEvent>(ev =>
        {
            int nextScene = sceneIndex + 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        });
    }
}
