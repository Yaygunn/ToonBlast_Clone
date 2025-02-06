using UnityEngine;

namespace YBlast
{
    public class BaseCube : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        public virtual ECubeType Type => ECubeType.None;
        
        public Vector2Int CellIndex { get; private set; }

        public void SetCellIndex(Vector2Int cellIndex)
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
    }
}
