using UnityEngine;
using UnityEngine.Serialization;
using YBlast.Managers;
using Zenject;
using YBlast.Scriptables;

namespace YBlast.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private LevelDataSO _levelDataSO;

        [SerializeField] private SpacingSettingsSO _spacingSettingsSO;

        [SerializeField] private CubePrefabHolderSO _cubePrefabHolderSO;

        [SerializeField] private ColorCubeSpriteHolderSO _colorCubeSpriteHolderSO;

        [SerializeField] private LayerMasksSO _layerMasksSO;
        
        public override void InstallBindings()
        {
            #region SO Injection

            Container.BindInstance(_levelDataSO.GridData).AsSingle();

            Container.BindInstance(_levelDataSO.GroupRules).AsSingle();

            Container.BindInstance(_spacingSettingsSO).AsSingle();

            Container.BindInstance(_cubePrefabHolderSO).AsSingle();

            Container.BindInstance(_colorCubeSpriteHolderSO).AsSingle();
            
            Container.BindInstance(_layerMasksSO).AsSingle();
            
            #endregion
            
            Container.Bind<ICellPositionManager>()
                .To<YBlast.Managers.CellPosition.Classic.CellPositionManager>()
                .AsSingle();

            Container.Bind<GridManager>().AsSingle();

            Container.Bind<CubeSpawner>().AsSingle();

            Container.Bind<CubeSpriteManager>().AsSingle();

            Container.Bind<NeighborCalculator>().AsSingle();

            Container.BindInterfacesAndSelfTo<InputListener>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
        }
    }
}