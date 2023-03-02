using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
public class EnemyData :  ScriptableObject 
{
    [SerializeField, Min(1)] private int _defaultHealth = 100;
    [SerializeField, Min(0)] private float _speed = 1.0f;
    [SerializeField, Min(1)] private int _minMeleeDamage = 10;
    [SerializeField, Min(1)] private int _maxMeleeDamage = 10;
    [SerializeField, Min(0.1f)] private float _meleeRange = 0.5f;
    [SerializeField, Min(0)] private int _minkillReward = 10;
    [SerializeField, Min(0)] private int _maxkillReward = 10;
    public int DefaultHealth => _defaultHealth;
    public float Speed => _speed;
    public int MeleeDamage => Random.Range(_minMeleeDamage, _maxMeleeDamage);
    public float MeleeRange => _meleeRange;
    public int KillReward => Random.Range(_minkillReward, _maxkillReward);
}
