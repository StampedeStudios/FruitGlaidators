using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuHandler : MonoBehaviour
{

    public void Awake()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        Button menuButton = root.Q<Button>("PlayBtn");
        menuButton.RegisterCallback<ClickEvent>(ev => OnPlay());
    }

    public void OnPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

}
