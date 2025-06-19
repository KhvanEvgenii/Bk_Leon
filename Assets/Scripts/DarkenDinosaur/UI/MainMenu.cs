using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    { 
        SceneManager.LoadScene("Game"); // Replace with your game scene name
    }
}
