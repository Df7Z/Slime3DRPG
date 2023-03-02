using System;
using System.Collections;
using UnityEngine;

public class Player : BaseEntity
{
    [SerializeField] private int _startHealth = 100;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private Animator _animator;
    private int _maxHealth;
    private int _health;
    private float _defDist = 0.05f; //Погрешность
    private int _idleAnim = Animator.StringToHash("SlimeIdle");
    private int _attackAnim = Animator.StringToHash("SlimeAttack");
    private int _walkAnim = Animator.StringToHash("SlimeWalk");
    public Action OnPlayerReady;
    public Action OnPlayerDead;
    
    protected override void Awake()
    {
        base.Awake();
        _playerWeapon.OnWeaponShoot += PlayerShoot;
        ResetData();
    }

    public override void ResetData()
    {
        StopAllCoroutines();
        base.ResetData();
        _maxHealth = _startHealth;
        _health = _maxHealth;
        _animator.CrossFade(_idleAnim, 0.0f);
        OnHealthChange?.Invoke(_health, _maxHealth);
    }
    
    private void PlayerShoot(float fireRate)
    {
        _animator.CrossFade(_attackAnim, 0.1f);
        _animator.speed = 1 / fireRate;
    }
    
    public override void PointDamage(int value)
    {
        base.PointDamage(value);
        _health -= value;

        if (_health <= 0)
        {
            _playerWeapon.Deactivate();
            OnPlayerDead?.Invoke();
        }
        
        OnHealthChange?.Invoke(_health, _maxHealth);
    }
    
    public void MoveToNextScene(GameScene gameScene)
    {
        _animator.speed = 1f;
        StartCoroutine(MoveTo(gameScene));
    }

    private IEnumerator MoveTo(GameScene gameScene)
    {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        _animator.CrossFade(_walkAnim, 0.3f);
        
        while (Vector3.Distance(transform.position, gameScene.PlayerSceneTransform.position) > _defDist)
        {
            transform.position =  Vector3.MoveTowards(transform.position, gameScene.PlayerSceneTransform.transform.position,
                Time.deltaTime * _speed);
            yield return frame;
        }
        
        _animator.CrossFade(_idleAnim, 0.3f);
        OnPlayerReady?.Invoke();
        _playerWeapon.Activate(gameScene);
    }

    public void UpgradeHealth()
    {
        _maxHealth *= 2;
        _health = _maxHealth;
        OnHealthChange?.Invoke(_health, _maxHealth);
    }
    
    public void UpgradeSpeed() { _playerWeapon.UpgradeSpeed(); }
    
    public void UpgradeDamage() { _playerWeapon.UpgradeDamage(); }
    
}

