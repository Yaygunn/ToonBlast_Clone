using UnityEngine;

namespace YBlast
{
    public class BaseCube : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        
        public virtual ECubeType Type => ECubeType.None;

        public virtual bool IsFallable => true;
        
        public Vector2Int CellIndex { get; private set; }

        public virtual void SetCellIndex(Vector2Int cellIndex)
        {
            CellIndex = cellIndex;
            _spriteRenderer.sortingOrder = -cellIndex.x;
        }

        public virtual void OnClick()
        {
            Debug.Log("play cube shake anim");
        }

        public virtual void OnBlast()
        {
            Debug.Log("blasted");
        }

        public void Fall(Vector3 fallDestination)
        {
            transform.position = fallDestination;
            FallenToDestination();
        }

        protected virtual void FallenToDestination()
        {
            
        }
    }
}
