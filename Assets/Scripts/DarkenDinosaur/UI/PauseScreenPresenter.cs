using DarkenDinosaur.Managers;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private GameObject pauseMenuContainer;
    [Space]
    [SerializeField] private string scorePrefix = "Счет: ";
    [SerializeField] private Text scoreText;
    [SerializeField] private Text inGameScoreText;
    [Space]
    [SerializeField] private string distancePrefix = "Расстояние: ";
    [SerializeField] private Text distanceText;
    [SerializeField] private Text inGamedistanceText;
    [Space]
    [SerializeField] private Text balanceValue;
    [Space]
    [SerializeField] private GameObject scoreManager;
    private ScoreManager scoreManagerScript;

    private void Start()
    {
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
    }
    public void OnPlayerDead()
    {
        this.pauseMenuContainer.SetActive(true);

        if (scoreText != null && inGameScoreText != null)
        {
            scoreText.text = scorePrefix + inGameScoreText.text;
        }

        if (distanceText != null && inGamedistanceText != null)
        {
            distanceText.text = distancePrefix + inGamedistanceText.text;
        }

        if (scoreManagerScript == null)
        {
            scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        }

        balanceValue.text = scoreManagerScript.GetBalance().ToString();
    }

    public void SpendBalance()
    {
        if (scoreManagerScript == null)
        {
            scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        }

        scoreManagerScript.ChangeBalance(500);
    }
}
