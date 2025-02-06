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
        
        public override void InstallBindings()
        {
            #region SO Injection

            Container.BindInstance(_levelDataSO.GridData).AsSingle();

            Container.BindInstance(_spacingSettingsSO).AsSingle();

            Container.BindInstance(_cubePrefabHolderSO).AsSingle();

            Container.BindInstance(_colorCubeSpriteHolderSO).AsSingle();
            
            #endregion
            
            Container.Bind<ICellPositionManager>()
                .To<YBlast.Managers.CellPosition.Classic.CellPositionManager>()
                .AsSingle();

            Container.Bind<GridManager>().AsSingle();

            Container.Bind<CubeSpawner>().AsSingle();

            Container.Bind<CubeSpriteManager>().AsSingle();
        }
    }
}