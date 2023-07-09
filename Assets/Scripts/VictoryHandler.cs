using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class VictoryHandler : MonoBehaviour
{
    private UIDocument victoryScreenRef;

    void Awake()
    {
        victoryScreenRef = GetComponent<UIDocument>();
        string sceneName = SceneManager.GetActiveScene().name;
        string nextSceneName = "Level" + (int.Parse(sceneName.Substring(sceneName.Length - 1)) + 1).ToString();

        VisualElement root = victoryScreenRef.rootVisualElement;
        Button menuButton = root.Q<Button>("NextLvlBtn");
        menuButton.RegisterCallback<ClickEvent>(ev =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        });

    }
}
