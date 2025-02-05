using UnityEngine;

namespace YBlast
{
    public class ColorCube : BaseCube
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public override ECubeType Type => ECubeType.ColorCube;

        private ECubeColor _color;

        #region Getters

        public ECubeColor CubeColor => _color;

        #endregion

        public void SetColor(ECubeColor cubeColor)
        {
            _color = cubeColor;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}
