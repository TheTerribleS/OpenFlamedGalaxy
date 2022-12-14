using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MercenaryBehavior : BaseBehaviorTree
{
        private List<Ship> _enemies = new(),
                       _shipsToProtect = new();

    void Awake()
    {
        base.Awake();
        beh = Behavior.Standby;
        

    }

    private void Start()
    {
        Vector3 newRandPos = RandomPos();
        SetNewTarget(newRandPos, true);
    }

    private void Update()
    {
        base.Update();
        switch (beh)
        {
            case Behavior.Standby:
                mov.Wander();
                if (Vector3.Distance(transform.position, target) <= 10)
                {
                    Vector3 newRandPos = RandomPos();
                    SetNewTarget(newRandPos, true);
                }
                break;
            case Behavior.Fighting:
                if (_enemies.Count == 0)
                {
                    beh = Behavior.Standby;
                    return;
                }

                    SetNewTarget(_enemies[0].transform.position, false);
                mov.Seek(_enemies[0].transform);
                
                
                break;
            case Behavior.Working:
            case Behavior.Fleeing:
            case Behavior.CONTROLLED_BY_PLAYER:
            default:
                throw new ArgumentOutOfRangeException();
        }

        RaycastHit ray;
        if (Physics.Raycast(transform.position, transform.position + transform.forward * 100, out ray, 2000))
        {
            if (!ray.collider.gameObject.GetComponent<Ship>()) return;

            bool isEnemyShip = ray.collider.gameObject.GetComponent<Ship>().Faction is Factions.Rebel or Factions.Enemy;
            
            if (shootTimer <= 0 && isEnemyShip)
            {
                myShip.Fire();
                ResetShoot();
            }
        }


    }


    protected override void PerceiveShip(Ship ship)
    {
        base.PerceiveShip(ship);
        
        if (ship.Faction == myShip.Faction)
        {
            _shipsToProtect.Add(ship);
        }
        else if (ship.Faction != Factions.Enemy)
        {
            _enemies.Add(ship);
        }
        Decide();
    }

    protected override void PerceiveShipOut(Ship ship)
    {
        base.PerceiveShipOut(ship);
        
        if (_shipsToProtect.Contains(ship))
        {
            _shipsToProtect.Remove(ship);
        }
        else if (_enemies.Contains(ship))
        {
            _enemies.Remove(ship);
        }
        Decide();
        
    }

    protected override void Decide()
    {
        beh = _enemies.Count >= 1 ? Behavior.Fighting : Behavior.Standby;
    }

    Vector3 RandomPos()
    {
        Vector3 myPos = transform.position;
        
        return WorldContext.Instance.station.transform.position + 
               new Vector3(Random.Range(myPos.x - 150, myPos.x + 150),
                           Random.Range(myPos.y - 150, myPos.y + 150), 
                           Random.Range(myPos.z - 100, myPos.y - 300));
    }
}
