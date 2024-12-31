using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public int GarbageCollected;
    public int Money;

    public SerializableDictionary<GameObject, bool> IsCityLocked;
    public SerializableDictionary<Vector3, GameObject> RemainingGarbagePositions;

    public GameData() 
    {
        this.GarbageCollected = 0;
        this.Money = 0;
        IsCityLocked = new SerializableDictionary<GameObject, bool>();
        RemainingGarbagePositions = new SerializableDictionary<Vector3, GameObject>();
    }
}
