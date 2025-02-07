using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class FallManager : ITickable
    {
        private GridManager _gridManager;

        private ICellPositionManager _cellPositionManager;

        private SpawnManager _spawnManager;

        private HashSet<int> _blastedColumbs;

        private int _gridHeight;

        private List<Vector2Int> _cellsThatNeedSpawn = new();
        
        [Inject]
        void Construct(GridManager gridManager, ICellPositionManager cellPositionManager, SpawnManager spawnManager)
        {
            _gridManager = gridManager;
            _cellPositionManager = cellPositionManager;
            _spawnManager = spawnManager;

            _gridHeight = _gridManager.GetGridSize().x;
            _blastedColumbs = new(_gridManager.GetGridSize().y);
        }
        
        public void Tick()
        {
            FallBlastedColumbs();
        }

        public void AddABlastedCell(Vector2Int cellIndex)
        {
            _blastedColumbs.Add(cellIndex.y);
        }

        private void FallBlastedColumbs()
        {
            foreach (int columb in _blastedColumbs)
                ColumbFall(columb);
            
            _blastedColumbs.Clear();
            
            _spawnManager.SpawnCubes(_cellsThatNeedSpawn, FallToACell);
            _cellsThatNeedSpawn.Clear();
        }

        private void FallCube(Vector2Int cellIndex, Vector2Int destinationIndex)
        {
            BaseCube cube = _gridManager.GetBaseCube(cellIndex);
            _gridManager.RemoveCube(cellIndex);
            
            FallToACell(cube, destinationIndex);
        }

        private void SpawnAndFall( Vector2Int destinationIndex)
        {
            _cellsThatNeedSpawn.Add(destinationIndex);
        }

        private void FallToACell(BaseCube cube, Vector2Int destinationIndex)
        {
            _gridManager.PlaceCube(cube, destinationIndex);
            cube.Fall(_cellPositionManager.GetCellPos(destinationIndex));
        }
        
        private void ColumbFall(int columbIndex)
        {
            int rowIndex = _gridHeight;

            while (true)
            {
                rowIndex--;
                
                rowIndex = FindLowestEmptyCell(columbIndex, rowIndex);
                if (rowIndex == -1)
                    return;
                
                int cellAbove = FindLowestFallableCell(columbIndex, rowIndex - 1);

                if(cellAbove == -1)
                    SpawnAndFall(new Vector2Int(rowIndex,columbIndex));
                else
                    FallCube(new Vector2Int(cellAbove, columbIndex), new Vector2Int(rowIndex, columbIndex));
                
            }
            

            
        }

        private int FindLowestEmptyCell(int columbIndex, int startRowIndex)
        {
            Vector2Int cellIndex = new Vector2Int(startRowIndex, columbIndex);
            
            for (; cellIndex.x>= 0; cellIndex.x--)
                if (_gridManager.GetCubeType(cellIndex) == ECubeType.None)
                    return cellIndex.x;
            return cellIndex.x;
        }

        private int FindLowestFallableCell(int columbIndex, int startRowIndex)
        {
            Vector2Int cellIndex = new Vector2Int(startRowIndex, columbIndex);

            for (; cellIndex.x >= 0; cellIndex.x--)
            {
                if (_gridManager.GetCubeType(cellIndex) == ECubeType.None)
                    continue;

                return cellIndex.x;
            }

            return -1;
        }
    }
}
