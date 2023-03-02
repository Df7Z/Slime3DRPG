
using UnityEngine;

namespace PoolSystem
{
    public class SystemPoolDespawner : MonoBehaviour
    {
        [SerializeField] private float timeToDespawn = 3f;

        private bool processed;
        private float timer;

        private void OnEnable()
        {
            Restore();
        }

        private void OnDisable()
        {
            Restore();
        }

        private void Update()
        {
            if (IsDespawnMoment() == false)
                return;
            
            SystemPool.Despawn(gameObject);
            
            OnProcessed();
        }

        private bool IsDespawnMoment()
        {
            if (processed)
                return false;
            
            timer += Time.deltaTime;

            if (timer >= timeToDespawn)
                return true;

            return false;
        }

        private void Restore()
        {
            timer = 0f;
            processed = false;
        }

        private void OnProcessed()
        {
            processed = true;
            timer = 0f;
        }
    }
}