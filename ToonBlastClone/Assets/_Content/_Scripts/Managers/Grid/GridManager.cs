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

        public BaseCube GetBaseCube(Vector2Int gridIndex)
        {
            return _grid.Cubes[gridIndex.x, gridIndex.y];
        }

        public ECubeColor GetCubeColor(Vector2Int gridIndex)
        {
            return _grid.CubeColors[gridIndex.x, gridIndex.y];
        }

        public void PlaceCube(BaseCube cube, Vector2Int gridIndex)
        {
            _grid.Cubes[gridIndex.x, gridIndex.y] = cube;
            if (cube.Type == ECubeType.ColorCube)
                _grid.CubeColors[gridIndex.x, gridIndex.y] = ((ColorCube)cube).CubeColor;
        }

        public void RemoveCube(Vector2Int gridIndex)
        {
            _grid.Cubes[gridIndex.x, gridIndex.y] = null;
            _grid.CubeColors[gridIndex.x, gridIndex.y] = ECubeColor.None;
        }
    }
}
