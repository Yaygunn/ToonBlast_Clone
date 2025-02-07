using UnityEngine;
using YBlast.Scriptables;
using YBlast.Systems;
using Zenject;

namespace YBlast.Installers
{
    public class LevelDataInstaller : MonoInstaller
    {
        [SerializeField] private LevelHolderSO _levelHolderSO;

        private LevelDataSO _levelDataSO;

        
        public override void InstallBindings()
        {
            SetLevelData();
            
            
            Container.BindInstance(_levelDataSO.GridData).AsSingle();

            Container.BindInstance(_levelDataSO.GroupRules).AsSingle();
            
            Container.BindInstance(_levelDataSO.ColorPossibilities).AsSingle();

        }

        private void SetLevelData()
        {
            _levelDataSO = _levelHolderSO.GetLevel(SaveSystem.GetLevelIndex());
        }
    }
}