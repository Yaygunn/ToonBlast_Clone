using UnityEngine;
using Zenject;
using YBlast.Data;

namespace YBlast.Managers
{
    public class GridManager
    {
        private GridData _grid;

        [Inject]
        void Construct(GridCreationData creationData)
        {
            _grid = new GridData(creationData.GridSize);
        }

        public BaseCube GetBaseCube(Vector2Int cellIndex)
        {
            return _grid.Cubes[cellIndex.x, cellIndex.y];
        }

        public ECubeColor GetCubeColor(Vector2Int cellIndex)
        {
            return _grid.CubeColors[cellIndex.x, cellIndex.y];
        }

        public void PlaceCube(BaseCube cube, Vector2Int cellIndex)
        {
            _grid.Cubes[cellIndex.x, cellIndex.y] = cube;
            if (cube.Type == ECubeType.ColorCube)
                _grid.CubeColors[cellIndex.x, cellIndex.y] = ((ColorCube)cube).CubeColor;
        }

        public void RemoveCube(Vector2Int cellIndex)
        {
            _grid.Cubes[cellIndex.x, cellIndex.y] = null;
            _grid.CubeColors[cellIndex.x, cellIndex.y] = ECubeColor.None;
        }

        public bool IsValidIndex(Vector2Int cellIndex)
        {
            return cellIndex.x >= 0 && cellIndex.y >= 0 && cellIndex.x < _grid.GridSize.x && cellIndex.y < _grid.GridSize.y;
        }
    }
}
