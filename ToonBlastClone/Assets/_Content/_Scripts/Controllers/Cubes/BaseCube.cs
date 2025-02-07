using DG.Tweening;
using UnityEngine;

namespace YBlast
{
    public class BaseCube : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        private const float _gravityMultiply = 0.1f;
        
        public virtual ECubeType Type => ECubeType.None;

        public virtual bool IsFallable => true;
        
        public Vector2Int CellIndex { get; private set; }

        private Sequence _tweenSequence;

        public virtual void SetCellIndex(Vector2Int cellIndex)
        {
            CellIndex = cellIndex;
            _spriteRenderer.sortingOrder = -cellIndex.x;
        }

        public virtual void OnClick()
        {
           Transform tr= _spriteRenderer.transform;
           tr.DOKill();
           tr.rotation = Quaternion.identity;
           tr.DOShakeRotation(0.5f, new Vector3(0, 0, 30), 10,0);
        }

        public virtual void OnBlast()
        {
            Debug.Log("blasted");
        }

        public virtual void Fall(Vector3 fallDestination)
        {
            float time = Mathf.Sqrt((2 * (fallDestination - transform.position).magnitude) * _gravityMultiply);
            
            _tweenSequence.Kill();
            
            _tweenSequence = DOTween.Sequence();

            _tweenSequence.Append(transform.DOMove(fallDestination, time).SetEase(Ease.OutBounce));
            _tweenSequence.Insert(time * 0.6f, DOTween.To(() => 0, x => { }, 1, 0).OnComplete(FallenToDestination));
        }

        protected virtual void FallenToDestination()
        {
            
        }
    }
}
