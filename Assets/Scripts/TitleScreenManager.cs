using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] public Button startButton;
    [SerializeField] public Button exitButton;
    [SerializeField] public InputField usernameInput;

    void Awake()
    {
        
    }

    public void StartButtonClicked()
    {
        if (usernameInput.text.Length > 0)
        {
            // Set current user name
            GameManager.Instance.username = usernameInput.text;

            // Set score to zero
            GameManager.Instance.ResetScore();

            // Go to Main Scene and start the game
            GameManager.Instance.StartMainScene();
        }
    }

    public void ExitButtonClicked()
    {
        GameManager.Instance.CloseGame();
    }
}
