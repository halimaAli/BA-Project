using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public string username;
    public int coins;
    public int healthPoints;
    public Vector3 spawnPoint;
    public int currentSceneIndex;
    internal long lastUpdated;

    public SerializableDictionary<string, float> volumeSettings = new SerializableDictionary<string, float>();
    public SerializableDictionary<string, bool> coinsCollected;
    public SerializableDictionary<string, bool> spawnPointReached;


    public GameData() {
        username = "Nexus";
        coins = 0;
        healthPoints = 9;
        spawnPoint = Vector3.zero;
        volumeSettings.Add("masterVolume", 1);
        volumeSettings.Add("musicVolume", 1);
        volumeSettings.Add("soundFXVolume", 1);
        currentSceneIndex = 1;
        lastUpdated = 0;

        coinsCollected = new SerializableDictionary<string, bool>();
        spawnPointReached = new SerializableDictionary<string, bool>();
    }
}