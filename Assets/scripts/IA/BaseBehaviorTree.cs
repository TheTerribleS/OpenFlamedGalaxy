using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class BaseBehaviorTree : MonoBehaviour
{
    protected CpuMove mov;
    protected int[] shipCount;
    protected Ship myShip;
    protected Behavior beh;
    protected Vector3 target;
    
    protected readonly float shootReset = 0.2f;
    protected float shootTimer;
    
    protected readonly float boostReset = 5f;
    protected float boostTimer;
    
    protected readonly float boostUseLimit = 2f;
    protected float boostUsageTimer = 0;
    protected bool _isOnBoost;

    protected void Awake()
    {
        shipCount = new int[Enum.GetNames(typeof(Factions)).Length];
        mov = GetComponent<CpuMove>();
        myShip = GetComponent<Ship>();
        WorldContext.ShipSpawn += PerceiveShip;
        WorldContext.ShipDeath += PerceiveShipOut;
    }

    protected void Update()
    {
        if (shootTimer >= 0) shootTimer -= Time.deltaTime;
        if (boostTimer >= 0) boostTimer -= Time.deltaTime;

        if (_isOnBoost)
        {
            boostUsageTimer += Time.deltaTime;
            if (boostUsageTimer >= boostUseLimit)
            {
                _isOnBoost = false;
                ResetBoost();
            }
        }
    }

    protected virtual void Decide()
    {
        
    }

    protected virtual void PerceiveShip(Ship ship)
    {
        
    }

    protected virtual void PerceiveShipOut(Ship ship)
    {
        
    }

    /*protected void SetNewTarget()
    {
        target = WorldContext.Instance.station.transform.position + 
                 new Vector3(Random.Range(-250, 250),
                     Random.Range(-250, 250), 
                     Random.Range(-150, -450));
        
        mov.Target = target;
    }*/

    protected void SetNewTarget(Vector3 newTar, bool setNonShipTar)
    {
        target = newTar;
        if (setNonShipTar)
        {
            mov.nonShipTarget.transform.position = newTar;
            mov.nonShipTarget.transform.LookAt(transform);
        }
    }

    public void ResetShoot()
    {
        shootTimer = shootReset;
    }

    public void ResetBoost()
    {
        boostTimer = boostReset;
    }
    
}

public enum Behavior
{
    CONTROLLED_BY_PLAYER,
    Standby,
    Working,
    Fighting,
    Fleeing
}
