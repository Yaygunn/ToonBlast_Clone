using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class FallManager : ITickable
    {
        private GridManager _gridManager;

        private ICellPositionManager _cellPositionManager;

        private HashSet<int> _blastedColumbs;

        private int _gridHeight;


        [Inject]
        void Construct(GridManager gridManager, ICellPositionManager cellPositionManager)
        {
            _gridManager = gridManager;
            _cellPositionManager = cellPositionManager;

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
        }

        private void FallCube(Vector2Int cellIndex, Vector2Int destinationIndex)
        {
            BaseCube cube = _gridManager.GetBaseCube(cellIndex);
            _gridManager.RemoveCube(cellIndex);
            
            _gridManager.PlaceCube(cube, destinationIndex);
            cube.Fall(_cellPositionManager.GetCellPos(destinationIndex));
        }

        private void SpawnAndFall(Vector2Int destinationIndex)
        {
            
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
                
                var cellAbove = FindLowestFallableCell(columbIndex, rowIndex - 1);

                if (!cellAbove.isFallable)
                {
                    rowIndex = cellAbove.rowIndex + 1;
                    continue;
                }
                
                if(cellAbove.rowIndex == -1)
                    SpawnAndFall(new Vector2Int(rowIndex,columbIndex));
                else
                    FallCube(new Vector2Int(cellAbove.rowIndex, columbIndex), new Vector2Int(rowIndex, columbIndex));
                
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

        private (bool isFallable, int rowIndex) FindLowestFallableCell(int columbIndex, int startRowIndex)
        {
            Vector2Int cellIndex = new Vector2Int(startRowIndex, columbIndex);

            for (; cellIndex.x >= 0; cellIndex.x--)
            {
                if (_gridManager.GetCubeType(cellIndex) == ECubeType.None)
                    continue;

                if (_gridManager.GetBaseCube(cellIndex).IsFallable)
                    return (true,cellIndex.x);
                else
                    return (false, cellIndex.x);
            }
            return (true, -1);
        }
    }
}
