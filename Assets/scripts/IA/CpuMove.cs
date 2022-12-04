using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class CpuMove : MonoBehaviour
{
    [SerializeField]float speed;
    [SerializeField] private float rotSpeed;
    private float _power;
    private Ship _ship;
    private Transform target;
    public GameObject nonShipTarget;
    private bool _hasBoost;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    private void Awake()
    {
        nonShipTarget = new GameObject();
        target = nonShipTarget.transform;
    }

    void Start()
    {
        _ship = GetComponent<Ship>();
        _power = 1;
        switch(_ship.role)
        {
            case Role.Security:
                gameObject.AddComponent<SecurityBehavior>();
                break;
            case Role.Mercenary:
                gameObject.AddComponent<MercenaryBehavior>();
                break;
            case Role.Miner:
                break;
            case Role.Merchant:
                break;
            case Role.Escort:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * _power * (_hasBoost? 2f : 1f) * Time.deltaTime), Space.Self);
        
        if(!target) return;

        var lookRotation = Quaternion.LookRotation((target.position - transform.position).normalized, transform.up);
        lookRotation.Normalize();
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotSpeed * Time.deltaTime);
        transform.rotation.Normalize();
    }

    public void RevampThrottle(float newPow)
    {
        _power += newPow;
        if (_power < 0) _power = 0;
        if (_power > 1) _power = 1;
    }

    public void SetThrottle(float newThrottle)
    {
        _power = newThrottle;
    }

    public void RotateShip(Vector3 rotType)
    {
        transform.Rotate(rotType, Space.Self);
    }

    public void Wander()
    {
        target = nonShipTarget.transform;
    }

    public void Seek(Transform trans)
    {
        target = trans;
    }

    public void Flee()
    {
        
    }

    public void SetBoost(bool value)
    {
        _hasBoost = value;
    }
}