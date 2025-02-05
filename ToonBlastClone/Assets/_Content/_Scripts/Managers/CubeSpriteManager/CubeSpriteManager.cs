using System.Collections.Generic;
using UnityEngine;
using YBlast.Data;
using YBlast.Scriptables;
using Zenject;

namespace YBlast.Managers
{
    public class CubeSpriteManager
    {
        private ColorCubeSpriteHolderSO _colorCubeSpriteHolderSO;
        [Inject]
        void Construct(ColorCubeSpriteHolderSO colorCubeSpriteHolderSO)
        {
            _colorCubeSpriteHolderSO = colorCubeSpriteHolderSO;
        }

        public void SetCorrectSprite(ColorCube cube)
        {
            cube.SetSprite(_colorCubeSpriteHolderSO.GetSprite(cube.CubeColor, ECubeColorVersion.Default));
        }
        
        public void SetCorrectSprites(Vector2Int cellIndex)
        {
            
        }

        public void SetCorrectSprites(List<Vector2Int> cellIndexes)
        {
            
        }
    }
}
