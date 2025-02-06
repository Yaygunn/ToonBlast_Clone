using UnityEngine;

namespace YBlast
{
    public class BaseCube : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        public virtual ECubeType Type => ECubeType.None;

        public void SetCellIndex(Vector2Int cellIndex)
        {
            _spriteRenderer.sortingOrder = -cellIndex.x;
        }

        public virtual void OnClick()
        {
        }
    }
}
