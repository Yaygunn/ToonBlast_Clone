using UnityEngine;
using YBlast.Scriptables;
using Zenject;

namespace YBlast.Managers
{
    public class CubeSpawner
    {
        private CubePrefabHolderSO _cubePrefabHolder;

        private CubeSpriteManager _cubeSpriteManager;
        
        [Inject]
        void Construct(CubePrefabHolderSO cubePrefabHolder, CubeSpriteManager cubeSpriteManager)
        {
            _cubePrefabHolder = cubePrefabHolder;
            _cubeSpriteManager = cubeSpriteManager;
        }
        
        public ColorCube SpawnColorCube(ECubeColor color)
        {
            ColorCube colorCube = GameObject.Instantiate(_cubePrefabHolder.ColorCube.gameObject)
                .GetComponent<ColorCube>();
            
            colorCube.SetColor(color);
            
            _cubeSpriteManager.SetCorrectSprite(colorCube);
            
            return colorCube;
        }
    }
}
