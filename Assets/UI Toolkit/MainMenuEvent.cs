using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvent : MonoBehaviour
{
    private UIDocument document;
    private Button button;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        button = document.rootVisualElement.Q<Button>("StartButton");
        button.RegisterCallback<ClickEvent>(ev => StartGame());
    }
    /*
    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(ev => StartGame());
    }
    */

    private void StartGame()
    {
        Debug.Log("Start Game button clicked.");
        // Add logic to start the game, e.g., load a new scene
        SceneManager.LoadScene("Stage1");
    }

}
