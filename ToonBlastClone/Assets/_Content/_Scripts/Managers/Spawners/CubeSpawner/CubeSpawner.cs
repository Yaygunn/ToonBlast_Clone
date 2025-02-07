using Systems.ObjectPool;
using UnityEngine;
using YBlast.Data;
using YBlast.Scriptables;
using YBlast.Utilities;
using Zenject;

namespace YBlast.Managers
{
    public class CubeSpawner
    {
        private CubePrefabHolderSO _cubePrefabHolder;

        private CubeSpriteManager _cubeSpriteManager;

        private ObjectPoolSystem _objectPool;
        
        [Inject]
        void Construct(CubePrefabHolderSO cubePrefabHolder, CubeSpriteManager cubeSpriteManager, ObjectPoolSystem objectPool, GridCreationData gridCreationData)
        {
            _cubePrefabHolder = cubePrefabHolder;
            _cubeSpriteManager = cubeSpriteManager;
            _objectPool = objectPool;

            int poolListLength = (int)(gridCreationData.GridSize.GetMultiplication() * 1.25f);
            _objectPool.InitializeQueue( _cubePrefabHolder.ColorCube.gameObject, poolListLength);
        }
        
        public ColorCube SpawnColorCube(ECubeColor color)
        {
            ColorCube colorCube = _objectPool.GetObject(_cubePrefabHolder.ColorCube.gameObject)
                .GetComponent<ColorCube>();
            
            colorCube.SetColor(color);
            
            _cubeSpriteManager.SetCorrectSprite(colorCube);
            
            return colorCube;
        }
    }
}
