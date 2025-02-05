using UnityEngine;
using YBlast.Managers;
using Zenject;
using YBlast.Scriptables;

namespace YBlast.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private GridCreationDataSO _gridCreationDataSo;

        [SerializeField] private SpacingSettingsSO _spacingSettingsSO;

        [SerializeField] private CubePrefabHolderSO _cubePrefabHolder;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_gridCreationDataSo.GridData).AsSingle();

            Container.BindInstance(_spacingSettingsSO).AsSingle();

            Container.BindInstance(_cubePrefabHolder).AsSingle();
            
            Container.Bind<ICellPositionManager>()
                .To<YBlast.Managers.CellPosition.Classic.CellPositionManager>()
                .AsSingle();

            Container.Bind<GridManager>().AsSingle();

            Container.Bind<CubeSpawner>().AsSingle();
        }
    }
}