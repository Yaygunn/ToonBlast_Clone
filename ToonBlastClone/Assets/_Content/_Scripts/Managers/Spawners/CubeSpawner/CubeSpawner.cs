using UnityEngine;
using YBlast.Scriptables;
using Zenject;

namespace YBlast.Managers
{
    public class CubeSpawner
    {
        private CubePrefabHolderSO _cubePrefabHolder;
        
        [Inject]
        void Construct(CubePrefabHolderSO cubePrefabHolder)
        {
            _cubePrefabHolder = cubePrefabHolder;
        }
        
        public ColorCube SpawnColorCube(ECubeColor color)
        {
            ColorCube colorCube = GameObject.Instantiate(_cubePrefabHolder.ColorCube.gameObject)
                .GetComponent<ColorCube>();
            
            colorCube.SetColor(color);
            return colorCube;
        }
    }
}
