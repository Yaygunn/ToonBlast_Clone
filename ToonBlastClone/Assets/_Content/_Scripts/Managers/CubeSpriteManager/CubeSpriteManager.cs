using System.Collections.Generic;
using UnityEngine;
using YBlast.Data;
using YBlast.Scriptables;
using Zenject;

namespace YBlast.Managers
{
    public class CubeSpriteManager
    {
        private GroupRules _groupRules;
        
        private ColorCubeSpriteHolderSO _colorCubeSpriteHolderSO;

        private GridManager _gridManager;
        
        [Inject]
        void Construct(GroupRules groupRules, ColorCubeSpriteHolderSO colorCubeSpriteHolderSO, GridManager gridManager)
        {
            _groupRules = groupRules;
            _colorCubeSpriteHolderSO = colorCubeSpriteHolderSO;
            _gridManager = gridManager;
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
            Sprite sprite = _colorCubeSpriteHolderSO.GetSprite(_gridManager.GetCubeColor(cellIndexes[0]),
                _groupRules.GetColorVersion(cellIndexes.Count));
            
            foreach (var cellIndex in cellIndexes)
            {
                ((ColorCube)_gridManager.GetBaseCube(cellIndex)).SetSprite(sprite);
                
            }
        }
    }
}
