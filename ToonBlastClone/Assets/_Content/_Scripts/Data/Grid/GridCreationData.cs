using System;
using UnityEngine;
using YBlast.Utilities;

namespace YBlast.Data
{
    
    [Serializable]
    public class GridCreationData
    {
        [SerializeField, HideInInspector] private Vector2Int _gridSize = new Vector2Int(1, 1);

        [SerializeField] private ECubeType[] _cubeTypes = new ECubeType [0];
        [SerializeField] private ECubeColor[] _cubeColors = new ECubeColor[0];

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

        public ECubeColor GetCubeColor(int index)
        {
            return _cubeColors[index];
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
        
        public void SetColorCube(ECubeColor cubeColor, int index)
        {
            _cubeTypes[index] = ECubeType.ColorCube;
            _cubeColors[index] = cubeColor;
        }

        public void UpdateGridSize(Vector2Int newSize)
        {
            ECubeColor[] newColors = new ECubeColor[newSize.x * newSize.y];
            
            for(int i = 0 ; i < newSize.x ; i++)
            for (int j = 0; j < newSize.y ; j++)
                newColors[GetNewGridIndex(i, j)] = ECubeColor.None;

            for(int i = 0 ; i < newSize.x && i < _gridSize.x ; i++)
            for (int j = 0; j < newSize.y && j < _gridSize.y; j++)
                newColors[GetNewGridIndex(i, j)] = _cubeColors[GetIndex(i,j)];

            int GetNewGridIndex(int x, int y)
            {
                return x + y * newSize.x;
            }

            SetGridSize(newSize);
            _cubeColors = newColors;
        }

        private int GetIndex(Vector2Int cellIndex)
        {
            return cellIndex.x + cellIndex.y * _gridSize.x;
        }
        
        private int GetIndex(int x, int y)
        {
            return x + y * _gridSize.x;
        }
    }
}
