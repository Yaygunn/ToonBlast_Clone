using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class ColorCubeBlaster 
    {
        private NeighborCalculator _neighborCalculator;
        
        private BlastManager _blastManager;

        [Inject]
        void Constuct(NeighborCalculator neighborCalculator, BlastManager blastManager)
        {
            _neighborCalculator = neighborCalculator;
            _blastManager = blastManager;
        }
        
        public bool TryToBlastColorCube(Vector2Int clickedColorCube)
        {
            List<Vector2Int> blastGroup = _neighborCalculator.CalculateSameColorNeighbors(clickedColorCube);

            if (blastGroup.Count < 2)
                return false;

            foreach (Vector2Int cellIndex in blastGroup)
                _blastManager.Blast(cellIndex);

            return true;
        }
    }
}
