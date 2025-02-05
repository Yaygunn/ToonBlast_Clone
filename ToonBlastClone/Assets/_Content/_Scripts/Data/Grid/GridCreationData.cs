using System;
using UnityEngine;

namespace YBlast.Data
{
    
    [Serializable]
    public class GridCreationData
    {
        [SerializeField] private Vector2Int _gridSize = new Vector2Int(1, 1);

        [SerializeField] private ECubeType[] _cubeTypes;
        [SerializeField] private ECubeColor[] _cubeColors;

        #region Getters

        public Vector2Int GridSize => _gridSize;

        public ECubeType GetCubeType(Vector2Int cellIndex)
        {
            return _cubeTypes[GetIndex(cellIndex)];
        }

        public ECubeColor GetCubeColor(Vector2Int cellIndex)
        {
            return _cubeColors[GetIndex(cellIndex)];
        }

        #endregion

        public void SetGridSize(Vector2Int size)
        {
            _gridSize = size;

            int totalSize = _gridSize.x * _gridSize.y;
            _cubeTypes = new ECubeType[totalSize];
            _cubeColors = new ECubeColor[totalSize];
        }

        public void SetCubeType(ECubeType cubeType, Vector2Int cellIndex)
        {
            int index = GetIndex(cellIndex);
            _cubeTypes[index] = cubeType;
            _cubeColors[index] = ECubeColor.None;
        }

        public void SetColorCube(ECubeColor cubeColor, Vector2Int cellIndex)
        {
            int index = GetIndex(cellIndex);
            _cubeTypes[index] = ECubeType.ColorCube;
            _cubeColors[index] = cubeColor;
        }

        private int GetIndex(Vector2Int cellIndex)
        {
            return cellIndex.x + cellIndex.y * _gridSize.x;
        }
    }
}
