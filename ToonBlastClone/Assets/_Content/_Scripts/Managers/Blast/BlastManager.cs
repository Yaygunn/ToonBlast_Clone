using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class BlastManager
    {
        private GridManager _gridManager;
        
        [Inject]
        void Constuct(NeighborCalculator neighborCalculator, GridManager gridManager)
        {
            _gridManager = gridManager;
        }

        public void Blast(List<Vector2Int> blastedCubeIndexes)
        {
            foreach (Vector2Int cellIndex in blastedCubeIndexes)
                Blast(cellIndex);
        }
        
        public void Blast(Vector2Int blastedCubeIndex)
        {
            _gridManager.GetBaseCube(blastedCubeIndex).OnBlast();
            _gridManager.RemoveCube(blastedCubeIndex);
        }
    }
}
