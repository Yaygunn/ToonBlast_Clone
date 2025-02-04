using UnityEngine;
using Zenject;
using YBlast.Scriptables;

namespace YBlast.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private GridCreationDataSO _gridCreationDataSo;

        [SerializeField] private SpacingSettingsSO _spacingSettingsSO;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_gridCreationDataSo.GridData).AsSingle();

            Container.BindInstance(_spacingSettingsSO).AsSingle();
            
            Container.Bind<ICellPositionManager>()
                .To<YBlast.Managers.CellPosition.Classic.CellPositionManager>()
                .AsSingle();
            
        }
    }
}