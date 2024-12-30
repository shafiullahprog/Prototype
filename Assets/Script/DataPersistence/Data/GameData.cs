using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public int GarbageCollected;
    public int Money;
    public List<Vector3> RemainingGarbagePositions;

    public GameData() 
    {
        this.GarbageCollected = 0;
        this.Money = 0;
        RemainingGarbagePositions = new List<Vector3>();
    }
}
