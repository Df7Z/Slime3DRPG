using System;
using System.Collections;
using UnityEngine;

public class Enemy : BaseEntity
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Animator _animator;
    private int _idleAnim = Animator.StringToHash("ZombieIdle");
    private int _walkAnim = Animator.StringToHash("ZombieWalk");
    private int _meleeAnim = Animator.StringToHash("ZombieMelee");
    private int _health;
    private Player _target;
    private bool _isDead;
    public bool IsDead => _isDead;
    public Action OnEnemyDeath;
    public static Action<int> OnEnemyDeathReward;
    
    protected override void Awake()
    {
        base.Awake();
        ResetData();
    }

    public override void ResetData()
    {
        StopAllCoroutines();
        base.ResetData();
        _isDead = false;
        _target = null;
        _health = _enemyData.DefaultHealth;
        _animator.speed = 1.0f;
        _animator.CrossFade(_idleAnim, 0.0f);
        OnHealthChange?.Invoke(_health, _enemyData.DefaultHealth);
    }

    public void Activate(Player player)
    {
        player.OnPlayerReady += AttackPlayer;
        _target = player;
    }

    private void AttackPlayer()
    {
        _target.OnPlayerReady -= AttackPlayer;
        _animator.CrossFade(_walkAnim, 0.5f); 
        StartCoroutine(MoveTo());
    }
    
    private IEnumerator MoveTo()
    {
        WaitForSeconds delay = new WaitForSeconds(0.01f);
        
        while (!CheckMeleeAttackDistance())
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                    new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z), Time.deltaTime * _enemyData.Speed);
            yield return delay;
        }
        
        StartCoroutine(MeleeAttack());
    }

    private bool CheckMeleeAttackDistance()
    {
        return (Vector3.Distance(transform.position, _target.transform.position) < _enemyData.MeleeRange);
    }
    
    private IEnumerator MeleeAttack()
    {
        _animator.CrossFade(_meleeAnim, 0.5f);
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        
        while (CheckMeleeAttackDistance())
        {
            _target.PointDamage(_enemyData.MeleeDamage);
            yield return delay;
        }
        
        // StartCoroutine(MoveTo());
    }
    
    protected override void Death()
    {
        base.Death();
        StopAllCoroutines();
        _isDead = true;
        OnEnemyDeath?.Invoke();
        OnEnemyDeathReward?.Invoke(_enemyData.KillReward);
        gameObject.SetActive(false);
    }
    
    public override void PointDamage(int value)
    {
        base.PointDamage(value);
        _health -= value;

        if (_health <= 0)
        {
            _health = 0;
            Death();
        }
        
        OnHealthChange?.Invoke(_health, _enemyData.DefaultHealth);
    }
}

