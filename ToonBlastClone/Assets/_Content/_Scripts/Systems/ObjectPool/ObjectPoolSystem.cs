using System;
using System.Collections.Generic;
using System.ObjectPool;
using UnityEngine;
using Zenject;

namespace Systems.ObjectPool
{
    public class ObjectPoolSystem : MonoBehaviour
    {
        private Dictionary<GameObject, Queue<GameObject>> _poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
        private Transform _transform;

        private DiContainer _container;

        [Inject]
        void Construct(DiContainer container)
        {
            _container = container;
        }

        public GameObject GetObject(GameObject prefab)
        {
            GameObject obj;
            if (_poolDictionary.ContainsKey(prefab))
            {
                if (_poolDictionary[prefab].Count > 0)
                {
                    obj = _poolDictionary[prefab].Dequeue();
                    obj.SetActive(true);
                }
                else
                {
                    obj = Spawn(prefab);
                }
            }
            else
            {
                _poolDictionary[prefab] = new Queue<GameObject>();
                obj = Spawn(prefab);
            }

            obj.GetComponent<IPoolObject>().SetReturnCallBack(()=>ReturnObject(prefab, obj));
            return obj;
        }

        private void ReturnObject(GameObject prefab, GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            
            _poolDictionary[prefab].Enqueue(obj);
        }

        private GameObject Spawn(GameObject prefab)
        {
            return _container.InstantiatePrefab(prefab);
        }
    }
}