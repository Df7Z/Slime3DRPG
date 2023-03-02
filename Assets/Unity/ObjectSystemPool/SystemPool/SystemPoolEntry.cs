
using UnityEngine;

namespace PoolSystem
{
    public class SystemPoolEntry : MonoBehaviour
    {
        [SerializeField] private PoolPreset poolPreset;

        private void Awake()
        {
            SystemPool.InstallPoolItems(poolPreset);
        }

        private void OnDestroy()
        {
            SystemPool.Reset();
        }
    }
}