using System.Collections.Generic;
using UnityEngine;
using Zenject;
using YBlast.Data;
using YBlast.Utilities;

namespace YBlast.Managers
{
    public class GridManager
    {
        private GridData _grid;

        [Inject]
        void Construct(GridCreationData creationData)
        {
            _grid = new GridData(creationData.GridSize.Add(2,2));
            
            // set cube colors to empty
            for (int i = 0; i < _grid.GridSize.x; i++)
                for(int j = 0 ; j < _grid.GridSize.y ; j++)
                    _grid.CubeColors[i,j] = -1;
        }

        public BaseCube GetBaseCube(Vector2Int cellIndex)
        {
            return _grid.Cubes[cellIndex.x, cellIndex.y];
        }

        public int GetCubeColor(Vector2Int cellIndex)
        {
            return _grid.CubeColors[cellIndex.x, cellIndex.y];
        }

        public void PlaceCube(BaseCube cube, Vector2Int cellIndex)
        {
            _grid.Cubes[cellIndex.x, cellIndex.y] = cube;
            cube.SetCellIndex(cellIndex);
            
            if (cube.Type == ECubeType.ColorCube)
                _grid.CubeColors[cellIndex.x, cellIndex.y] = ((ColorCube)cube).ColorIndex;
        }

        public void RemoveCube(Vector2Int cellIndex)
        {
            _grid.Cubes[cellIndex.x, cellIndex.y] = null;
            _grid.CubeColors[cellIndex.x, cellIndex.y] = -1;
        }

        public Vector2Int GetGridSize()
        {
            return _grid.GridSize;
        }

        public ECubeType GetCubeType(Vector2Int cellIndex)
        {
            BaseCube cube = _grid.Cubes[cellIndex.x, cellIndex.y];
            if (cube == null)
                return ECubeType.None;
            return cube.Type;
        }

        public void SetColorsForTesting(Dictionary<Vector2Int, int> testDictionary)
        {
            foreach (var cellIndex in testDictionary)
                _grid.CubeColors[cellIndex.Key.x, cellIndex.Key.y] = cellIndex.Value;
        }

        public void ResetTestedColors(List<Vector2Int> testedCells)
        {
            foreach (var cellIndex in testedCells)
                _grid.CubeColors[cellIndex.x, cellIndex.y] = -1;
        }

        public bool IsValidIndex(Vector2Int cellIndex)
        {
            return cellIndex.x >= 0 && cellIndex.x < _grid.GridSize.x
                                    && cellIndex.y >= 0 && cellIndex.y < _grid.GridSize.y;
        }
    }
}
