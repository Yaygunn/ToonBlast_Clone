using UnityEngine;

namespace YBlast.Data
{
    public class GridData 
    {
        public Vector2Int GridSize;
        public BaseCube[,] Cubes;
        public int[,] CubeColors;

        public GridData(Vector2Int gridSize)
        {
            GridSize = gridSize;
            Cubes = new BaseCube[GridSize.x, GridSize.y];
            CubeColors = new int[GridSize.x, GridSize.y];
        }
    }
}
