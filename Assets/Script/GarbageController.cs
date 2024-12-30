using UnityEngine;

public class GarbageController : MonoBehaviour
{
    public static GarbageController Instance;

    [SerializeField] private int processedGarbage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectGarbage(GameObject garbage)
    {
        Debug.Log("Destroyed");
        Destroy(garbage);
        GameManager.Instance.AddGarbage(1);
    }

    public void DeliverGarbage(int garbageInTruck)
    {
        processedGarbage += garbageInTruck;
        int moneyEarned = garbageInTruck * 10;
        GameManager.Instance.AddMoney(moneyEarned);
        garbageInTruck = 0;
        Debug.Log("Processed Garbage: "+processedGarbage+", Money Earned: "+moneyEarned);
    }
}
