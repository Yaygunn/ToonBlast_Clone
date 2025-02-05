using UnityEngine;

namespace YBlast
{
    public class ColorCube : BaseCube
    {
        public override ECubeType Type => ECubeType.ColorCube;

        private ECubeColor _color;

        #region Getters

        public ECubeColor CubeColor => _color;

        #endregion

        public void SetColor(ECubeColor cubeColor)
        {
            _color = cubeColor;
        }
    }
}
