using PoolSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IPoolItem
{
    private Rigidbody _rigidbody;
    private int _damage;
    private int _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Init(Vector3 velocity, int damage)
    {
        _damage = damage;
        _rigidbody.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) { return; }
        
        var damageble = other.gameObject.GetComponent<IDamageble>();
        if (damageble != null)
        {
            damageble.PointDamage(_damage);
        }
        
        SystemPool.Despawn(gameObject);
    }
        
    
    public void OnSpawn()
    {
       
    }

    public void OnDespawn()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
