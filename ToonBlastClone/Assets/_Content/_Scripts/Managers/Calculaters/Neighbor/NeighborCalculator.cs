using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class NeighborCalculator
    {
        private GridManager _gridManager;
        
         Vector2Int[] _directions = 
        {
            Vector2Int.right, 
            Vector2Int.left, 
            Vector2Int.up, 
            Vector2Int.down
        };
        
        [Inject]
        void Construct(GridManager gridManager)
        {
            _gridManager = gridManager;
        }
        

        public List<Vector2Int> CalculateSameColorNeighbors(Vector2Int initialCellIndex)
        {
            ECubeColor desiredColor = _gridManager.GetCubeColor(initialCellIndex);
            
            List<Vector2Int> blastingCubes = new List<Vector2Int>();
            blastingCubes.Add(initialCellIndex);
            
            CheckAllDirectionOfACell(initialCellIndex);

            void CheckAllDirectionOfACell(Vector2Int cellIndex)
            {
                foreach (Vector2Int direction in _directions)
                    CheckACell(cellIndex + direction);
            }

            void CheckACell(Vector2Int cellIndex)
            {
                if(blastingCubes.Contains(cellIndex))
                   return;
                if(!IsSameColor(cellIndex))
                    return;
                
                blastingCubes.Add(cellIndex);
                CheckAllDirectionOfACell(cellIndex);
            }

            bool IsSameColor(Vector2Int cellIndex)
            {
                if (!_gridManager.IsValidIndex(cellIndex))
                    return false;
                return _gridManager.GetCubeColor(cellIndex) == desiredColor;
            }
            
            return blastingCubes;
        }

        public Dictionary<Vector2Int, List<ECubeColor>> GetDamagedCellsAroundBlastedGroup(List<Vector2Int> blastingCubes)
        {
            Dictionary<Vector2Int, List<ECubeColor>> damagedCubes = new();
            
            CheckAllDirectionOfACell(blastingCubes[0]);

            void CheckAllDirectionOfACell(Vector2Int cellIndex)
            {
                foreach (Vector2Int direction in _directions)
                    CheckACell(cellIndex,cellIndex + direction);
            }

            void CheckACell(Vector2Int checkingCell, Vector2Int cellBeingChecked)
            {
                if(blastingCubes.Contains(cellBeingChecked))
                    return;
                if(!_gridManager.IsValidIndex(cellBeingChecked))
                    return;
                
                if(!damagedCubes.ContainsKey(cellBeingChecked))
                    damagedCubes.Add(cellBeingChecked, new List<ECubeColor>());
                
                damagedCubes[cellBeingChecked].Add(_gridManager.GetCubeColor(checkingCell));
            }
            
            return damagedCubes;
        }
    }
}
