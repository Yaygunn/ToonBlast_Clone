using UnityEngine;
using Zenject;

namespace YBlast.Factories
{
    public class GridFactory : MonoBehaviour
    {
        private GridCreationData _gridCreationData;
        
        [Inject]
        void Construct(GridCreationData creationData)
        {
            _gridCreationData = creationData;

            print(_gridCreationData.GetCubeColor(Vector2Int.one));
        }
    }
}
