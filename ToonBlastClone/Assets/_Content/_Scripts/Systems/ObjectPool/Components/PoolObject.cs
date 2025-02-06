using System;
using System.ObjectPool;
using UnityEngine;

namespace Systems.ObjectPool
{
    public class PoolObject : MonoBehaviour, IPoolObject
    {
        private Action _returnToPoolCallBack;

        public void SetReturnCallBack(Action returnToPoolCallBack)
        {
            _returnToPoolCallBack = returnToPoolCallBack;
        }

        private void OnDisable()
        {
            if (_returnToPoolCallBack != null)
                _returnToPoolCallBack();
            else
            {
                Debug.LogWarning("return to pool action is null");
                Destroy(gameObject);
            }
        }
    }
}
