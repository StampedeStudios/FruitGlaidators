using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectiveHandler : MonoBehaviour
{

    private UIDocument UIDoc;

    public Texture2D image;

    void Awake()
    {
        UIDoc = GetComponent<UIDocument>();
        VisualElement root = UIDoc.rootVisualElement;

        VisualElement imageSlot = root.Q<VisualElement>("ImageSlot");
        imageSlot.style.backgroundImage = image;
    }

    void Start()
    {
        // Show image for 3 seconds than destroy
        StartCoroutine(SelfDestroy(3));
    }

    IEnumerator SelfDestroy(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
