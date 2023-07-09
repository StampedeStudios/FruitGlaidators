using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DefeatHandler : MonoBehaviour
{

    private UIDocument defeatScreenRef;

    void Awake()
    {
        defeatScreenRef = GetComponent<UIDocument>();
        string sceneName = SceneManager.GetActiveScene().name;

        VisualElement root = defeatScreenRef.rootVisualElement;
        Button menuButton = root.Q<Button>("RestartBtn");
        menuButton.RegisterCallback<ClickEvent>(ev =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        });

    }
}
