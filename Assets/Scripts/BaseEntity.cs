using System;
using PoolSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseEntity : MonoBehaviour, IDamageble
{
    [SerializeField] protected EntityHPBar _entityHpBar;
    [SerializeField] protected OutgoingDamageText _outgoingDamageText;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    public Action<int, int> OnHealthChange; 
    
    protected virtual void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        
        if (_entityHpBar != null)
            OnHealthChange += _entityHpBar.UpdateBar;
    }

    public virtual void ResetData()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
    
    protected virtual void Death() { }

    public virtual void PointDamage(int value)
    {
        CreateOutgoingDamageText(value);
    }

    private void CreateOutgoingDamageText(int value)
    {
        OutgoingDamageText outText = SystemPool.Spawn(_outgoingDamageText, transform.position, Quaternion.identity);
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(2, 4f), Random.Range(-1f, 0f)) * 1.75f;
        outText.Init(randomDirection, value.ToString());
    }
}
