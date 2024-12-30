using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TruckSpawner : MonoBehaviour
{
    [SerializeField] Button spawnButton;
    private void Start()
    {
        spawnButton.onClick.AddListener(() =>
        {
            InstantaiteTruck();
        });
    }

    void InstantaiteTruck()
    {
        GameObject truck = ObjectPool.SharedInstance.GetPooledObject();
        if (truck != null)
        {
            truck.transform.position = transform.position;
            truck.transform.rotation = transform.rotation;
            truck.SetActive(true);
        }
        truck.GetComponent<TruckController>().IsMoving = true;
        spawnButton.interactable = false;

        Invoke("EnableButtonInteraction", 4f);
    }

    void EnableButtonInteraction()
    {
        spawnButton.interactable = true;
    }
}
