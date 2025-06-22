using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    { 
        SceneManager.LoadScene("Game"); // Replace with your game scene name
    }
    [DllImport("__Internal")]
    private static extern void FromUnity(string str);

    public void FromUnityBtn()
    {
        FromUnity("Сообщение получено из Unity");
    }

}
