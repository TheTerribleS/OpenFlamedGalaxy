using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class SesionRandGen : MonoBehaviour
{
    [SerializeField] private AssetDB db;
    private readonly int maxAsteroids = 50;
    private readonly int maxSec = 20;

    private void Start()
    {
        int asteroidsToGen = WorldContext.Instance.Level * 5;
        for (int i = 0; i < asteroidsToGen; i++)
        {
            int randIndex = Mathf.FloorToInt(Random.Range(0, db.asteroids.Length));
            GameObject asteroid = Instantiate(db.asteroids[randIndex]);
            asteroid.transform.position = RandUtils.GetRandomVector(WorldContext.Instance.GenZone);
            asteroid.transform.localScale *= Random.Range(2f, 5f);
            asteroid.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }

        int securityToGen = Random.Range(2, WorldContext.Instance.Level*2+1);
        
        GameObject[] secShipArray;
        switch (WorldContext.Instance.Faction)
        {
            case Factions.Human:
                secShipArray = db.HumanShips;
                break;
            case Factions.Reptile:
                secShipArray = db.ReptileShips;
                break;
            case Factions.Humanoid:
                secShipArray = db.HNoidShips;
                break;
            case Factions.Hybrid:
                secShipArray = db.HybShips;
                break;
            case Factions.Rebel:
                secShipArray = db.RebelShips;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        for (int i = 0; i < securityToGen; i++)
        {
            int randIndex = Mathf.FloorToInt(Random.Range(0, secShipArray.Length));
            GameObject secShip = Instantiate(secShipArray[randIndex]);
            secShip.transform.position = RandUtils.GetRandomVector(WorldContext.Instance.GenZone);
            secShip.GetComponent<Ship>().role = Role.Security;
            //secShip.transform.Rotate(Random.value * 360, Random.value * 360, Random.value * 360);
        }

        float randRebChance = Random.value;
        Debug.Log(randRebChance);
        
        if (randRebChance >= (1 - (float)WorldContext.Instance.Danger * 0.25f) )
        {
            SpawnRebels();
        }
    }

    void SpawnRebels()
    {
        int rebelSpawn = Mathf.CeilToInt(Random.Range(1, ((float)WorldContext.Instance.Danger + 1) * (10 - WorldContext.Instance.Level / 2)));
        for (int i = 0; i < rebelSpawn; i++)
        {
            int randIndex = Mathf.FloorToInt(Random.Range(0, db.RebelShips.Length));
            GameObject newRebel = Instantiate(db.RebelShips[randIndex]);
            newRebel.transform.position = RandUtils.GetRandomVector(WorldContext.Instance.GenZone);
            newRebel.GetComponent<Ship>().role = Role.Mercenary;
        }
    }
}
