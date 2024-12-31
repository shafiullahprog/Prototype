using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPopulatedArea : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int unlockPoints = 50;
    [SerializeField] private BoxCollider cityCollider;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject objectToEnable;
    Button button;
    public Button childButton => button;
    [SerializeField] private bool isUnlocked = false;
    bool IsFactoryVisible = false;  

    [SerializeField] string lockValue = "Lock";
    [SerializeField] string unlockValue = "Unlock";

    GameData gameData;

    private void Awake()
    {
        if (cityCollider == null)
        {
            cityCollider = GetComponent<BoxCollider>();
        }
    }

    private void LockCity()
    {
        cityCollider.enabled = isUnlocked;
        canvasObject.SetActive(!isUnlocked);
        //Debug.Log(gameObject.name +" is locked.");
    }

    private void UnlockCity()
    {
        if (GameManager.Instance.Money >= unlockPoints)
        {
            isUnlocked = true;
            cityCollider.enabled = isUnlocked;
            canvasObject.SetActive(!isUnlocked);

            GameManager.Instance.Money -= unlockPoints;
            GameManager.Instance.UpdateMoney();
            
            if (objectToEnable != null)
                objectToEnable.SetActive(isUnlocked);
        }
        else
        {
            Debug.Log("Insufficient Points");
        }
    }

    private void CheckUnlockCondition(int moneyEarned)
    {
        if (!isUnlocked && moneyEarned >= unlockPoints)
        {
            Debug.Log("Deduct: " + moneyEarned);
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = unlockValue;
        }
        else
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = lockValue;
        }
    }
    public void LoadData(GameData data)
    {
        gameData = data;
        Initialization();

        foreach (KeyValuePair<GameObject, bool> isCityUnlock in data.IsCityLocked)
        {
            GameObject referenceGameObject = isCityUnlock.Key;
            bool boolVal = isCityUnlock.Value;
            
            if (referenceGameObject == gameObject)
            {
                isUnlocked = boolVal;
                LockCity();
                button.interactable = isUnlocked;
            }
            else
            {
                cityCollider.enabled = isUnlocked;
                canvasObject.SetActive(!isUnlocked);
                
                if (objectToEnable != null)
                    objectToEnable.SetActive(!isUnlocked);
            }
        }
    }

    private void Initialization()
    {
        button = canvasObject.GetComponentInChildren<Button>();
        GameManager.Instance.OnMoneyCollectedUpdated.AddListener(CheckUnlockCondition);
        button.onClick.AddListener(() => 
        {
            UnlockCity();
        });
    }

    public void SaveData(ref GameData data)
    {
        if(!data.IsCityLocked.ContainsKey(gameObject))
            data.IsCityLocked.Add(gameObject, isUnlocked);
    }
}
