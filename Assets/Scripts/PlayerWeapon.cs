using System;
using System.Collections;
using PoolSystem;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float _startFireRate = 1f;
    [SerializeField] private int _startDamage = 20;
    [SerializeField] private float _shotDistance = 5f;
    [SerializeField] private Transform _projectileSpawnTransform;
    [SerializeField] private Projectile _projectile;
    private float _fireRate;
    private int _damage;
    public Action<float> OnWeaponShoot;

    private void Awake()
    {
        Gamelevel.OnLevelRestart += ResetData;
        ResetData();
    }

    private void ResetData()
    {
        _fireRate = _startFireRate;
        _damage = _startDamage;
    }
    
    private IEnumerator WeaponLoop(GameScene gameScene)
    {
        //WaitForSeconds delay = new WaitForSeconds(_fireRate);
        
        while (true)
        {
            Enemy _target = FindNearestTarget(gameScene.ActivatedEnemys);
            
            if (_target == null)
                yield break;
                
            Shot(_target);
            yield return new WaitForSeconds(_fireRate);
        }
    }

    private Enemy FindNearestTarget(Enemy[] enemies)
    {
        if (enemies.Length == 0) return null;
        
        Enemy nearEnemy = null;
        
        foreach (var enemy in enemies)
        {
            if (enemy.IsDead)
                continue;

            if (nearEnemy == null)
            {
                nearEnemy = enemy;
                continue;
            }
            
            if (Vector3.Distance(transform.position, enemy.transform.position) <
                Vector3.Distance(transform.position, nearEnemy.transform.position))
            {
                nearEnemy = enemy;
            }
        }
        
        return nearEnemy;
    }
    
    private void Shot(Enemy target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) > _shotDistance) return;
        
        _projectileSpawnTransform.localEulerAngles = new Vector3(-45, 0f, 0f);
        
        Vector3 fromTo = target.transform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);
        
        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = 45 * Mathf.PI / 180;

        float v2 = (Physics.gravity.magnitude * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        
        Projectile projectile = SystemPool.Spawn(_projectile, _projectileSpawnTransform.position, Quaternion.identity);
        projectile.Init(_projectileSpawnTransform.forward * v, _damage);
        
        OnWeaponShoot?.Invoke(_fireRate);
    }

    public void Activate(GameScene gameScene)
    {
        StartCoroutine(WeaponLoop(gameScene));
    }
    
    public void Deactivate()
    {
        StopAllCoroutines();
    }
    
    public void UpgradeSpeed()
    {
        if (_fireRate <= 0.25f)
            return;
        
        _fireRate /= 2f;
    }
    
    public void UpgradeDamage()
    {
        _damage *= 4;
    }
}
