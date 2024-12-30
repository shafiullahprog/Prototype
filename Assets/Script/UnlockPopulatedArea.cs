using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPopulatedArea : MonoBehaviour
{
    [SerializeField] private int unlockPoints = 50;
    [SerializeField] private BoxCollider cityCollider;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject objectToEnable;
    Button button;
    public Button childButton => button;
    [SerializeField] private bool isUnlocked = false;

    [SerializeField] string lockValue = "Lock";
    [SerializeField] string unlockValue = "Unlock";

    private void Start()
    {
        if (cityCollider == null)
        {
            cityCollider = GetComponent<BoxCollider>();
        }
        LockCity();
        GameManager.Instance.OnMoneyCollectedUpdated.AddListener(CheckUnlockCondition);

        button = canvasObject.GetComponentInChildren<Button>();
        button.interactable = isUnlocked;
        button.onClick.AddListener(UnlockCity);
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
            cityCollider.enabled = true;
            canvasObject.SetActive(false);

            GameManager.Instance.Money -= unlockPoints;
            GameManager.Instance.UpdateMoney();
            if (objectToEnable != null)
                objectToEnable.SetActive(true);
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
}
