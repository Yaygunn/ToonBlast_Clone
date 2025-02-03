using System;
using UnityEngine;

namespace YBlast
{
    
    [Serializable]
    public class GridCreationData
    {
        [SerializeField] private Vector2Int _gridSize = new Vector2Int(1, 1);

        [SerializeField] private ECubeType[] _cubeTypes;
        [SerializeField] private ECubeColor[] _cubeColors;

        #region Getters

        public Vector2Int GridSize => _gridSize;

        public ECubeType GetCubeType(Vector2Int gridIndex)
        {
            return _cubeTypes[GetIndex(gridIndex)];
        }

        public ECubeColor GetCubeColor(Vector2Int gridIndex)
        {
            return _cubeColors[GetIndex(gridIndex)];
        }

        #endregion

        public void SetGridSize(Vector2Int size)
        {
            _gridSize = size;

            int totalSize = _gridSize.x * _gridSize.y;
            _cubeTypes = new ECubeType[totalSize];
            _cubeColors = new ECubeColor[totalSize];
        }

        public void SetCubeType(ECubeType cubeType, Vector2Int gridIndex)
        {
            int index = GetIndex(gridIndex);
            _cubeTypes[index] = cubeType;
            _cubeColors[index] = ECubeColor.None;
        }

        public void SetColorCube(ECubeColor cubeColor, Vector2Int gridIndex)
        {
            int index = GetIndex(gridIndex);
            _cubeTypes[index] = ECubeType.ColorCube;
            _cubeColors[index] = cubeColor;
        }

        private int GetIndex(Vector2Int gridIndex)
        {
            return gridIndex.x + gridIndex.y * _gridSize.x;
        }
    }
}
