
using System;
using UnityEngine;

namespace PoolSystem
{
    [Serializable]
    public sealed class PoolItem
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int size;
        
        public GameObject Prefab => prefab;
        public int Size => size;
    }
}