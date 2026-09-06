using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Coin & Score Settings")]
    private int currentCoin = 0;
    private int totalCoins = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        currentCoin = 0;
    }

    public void AddCoins(int amount)
    {
        currentCoin += amount;
        totalCoins += amount;

        PlayerPrefs.SetInt("LastRunCoins", currentCoin);
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }
}

