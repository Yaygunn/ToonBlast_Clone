using UnityEngine;
using Zenject;

namespace YBlast.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private GridCreationDataSO _gridCreationDataSo;
        public override void InstallBindings()
        {
            Container.BindInstance(_gridCreationDataSo.GridData).AsSingle();
        }
    }
}