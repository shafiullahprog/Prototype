using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance;
    public int GarbageCollected;
    public int Money;

    public TextMeshProUGUI moneyEarned;
    public TextMeshProUGUI garbageCollected;

    public UnityEvent<int> OnMoneyCollectedUpdated;

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
        Debug.Log("Add Garbage");
        GarbageCollected += amount;
        UpdateGarbageCount();
    }

    private void UpdateGarbageCount()
    {
        garbageCollected.text = "Garbage in Kg: " + GarbageCollected.ToString();
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        OnMoneyCollectedUpdated.Invoke(Money);
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyEarned.text = "Money Earned: " + Money.ToString();
    }

    public void LoadData(GameData data)
    {
        this.Money = data.Money;
        this.GarbageCollected = data.GarbageCollected;
        UpdateMoney();
        UpdateGarbageCount();
    }

    public void SaveData(ref GameData data)
    {
        data.Money = this.Money;
        data.GarbageCollected  = this.GarbageCollected;

    }

}