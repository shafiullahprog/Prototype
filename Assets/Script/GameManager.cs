using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int GarbageCollected;
    public int Money;

    [Header("UI")]
    public TextMeshProUGUI moneyEarned;
    public TextMeshProUGUI garbageCollected;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        garbageCollected.text = "Garbage in Kg: " + GarbageCollected.ToString();
        moneyEarned.text = "Money Earned: " + Money.ToString();
    }

    public void AddGarbage(int amount)
    {
        Debug.Log("Add money");
        GarbageCollected += amount;
        garbageCollected.text = "Garbage in Kg: "+ GarbageCollected.ToString();
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        moneyEarned.text = "Money Earned: " + Money.ToString();
    }
}
