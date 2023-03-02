using System.Collections;
using PoolSystem;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OutgoingDamageText : MonoBehaviour, IPoolItem
{
    [SerializeField] private TextMeshPro _damageCountText;
    [SerializeField] private AnimationCurve _textFadeAnimationCurve;
    private Rigidbody _rigidbody;
    private Color defaultColor;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _damageCountText.overrideColorTags = true;
        defaultColor = _damageCountText.color;
    }

    public void Init(Vector3 velocity, string text)
    {
        _rigidbody.velocity = velocity;
        _damageCountText.text = text;
        StartCoroutine(FlyLoop());
    }

    private IEnumerator FlyLoop()
    {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        float time = 0f;
        Color defaultColor = _damageCountText.color;
        
        while (time < 1f)
        {
            _damageCountText.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, _textFadeAnimationCurve.Evaluate(time));
            time += Time.deltaTime;
            yield return frame;
        }
        
        SystemPool.Despawn(gameObject);
    }
    
    public void OnSpawn() { }

    public void OnDespawn()
    {
        StopAllCoroutines();
        _damageCountText.color = defaultColor;
        _rigidbody.velocity = Vector3.zero;
    }
}
