using Systems.ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;
using YBlast.Managers;
using Zenject;
using YBlast.Scriptables;

namespace YBlast.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [Header("SO")] 
        [SerializeField] private SpacingSettingsSO _spacingSettingsSO;

        [SerializeField] private CubePrefabHolderSO _cubePrefabHolderSO;

        [SerializeField] private ColorCubeSpriteHolderSO _colorCubeSpriteHolderSO;

        public override void InstallBindings()
        {
            #region SO Injection


            Container.BindInstance(_spacingSettingsSO).AsSingle();

            Container.BindInstance(_cubePrefabHolderSO).AsSingle();

            Container.BindInstance(_colorCubeSpriteHolderSO).AsSingle();
            
            #endregion
            
            #region WithInterfaces

            Container.BindInterfacesAndSelfTo<InputListener>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<FallManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CubeSpriteManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<GoalManager>().AsSingle().NonLazy();


            #endregion

            #region Hierarcy

            Container.Bind<ObjectPoolSystem>().FromComponentInHierarchy().AsSingle();

            #endregion

            
            Container.Bind<ICellPositionManager>()
                .To<YBlast.Managers.CellPosition.Classic.CellPositionManager>()
                .AsSingle();

            Container.Bind<GridManager>().AsSingle();

            Container.Bind<CubeSpawner>().AsSingle();

            Container.Bind<NeighborCalculator>().AsSingle();

            Container.Bind<ColorCubeBlaster>().AsSingle();

            Container.Bind<BlastManager>().AsSingle();

            Container.Bind<SpawnManager>().AsSingle();

        }
    }
}