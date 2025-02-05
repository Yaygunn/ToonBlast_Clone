using UnityEngine;
using YBlast.Scriptables;
using YBlast.Utilities;
using YBlast.Data;
using Zenject;

namespace YBlast.Managers.CellPosition.Classic
{
    public class CellPositionManager : ICellPositionManager
    {
        private Vector3[] _spawnPositionsByColumb;
        
        private Vector3 _cell00Pos = Vector3.zero;

        private Vector3 _centerPos = Vector3.zero;

        private float _spaceBetweenCells = 0;
        
        [Inject]
        void Construct(GridCreationData gridCreationData, SpacingSettingsSO spacingSettings)
        {
            _spaceBetweenCells = spacingSettings.CellSize + spacingSettings.CellSpacing;
            SetCell00Pos(gridCreationData.GridSize, spacingSettings.CellSize, spacingSettings.CellSpacing);
            SetCubeSpawnPositions(gridCreationData, spacingSettings.SpawnYOffset);
        }

        public Vector3 GetCellPos(Vector2Int cellIndex)
        {
            return _cell00Pos.Add(cellIndex.y * _spaceBetweenCells, -cellIndex.x * _spaceBetweenCells);
        }

        public Vector3 GetSpawnPos(int columb, int cubeChainOrder)
        {
            return _spawnPositionsByColumb[columb].Add(0, cubeChainOrder * _spaceBetweenCells);
        }

        private void SetCell00Pos(Vector2Int gridSize, float cellSize, float cellSpacing)
        {
            float totalHeight = gridSize.x * cellSize + (gridSize.x - 1) * cellSpacing;
            float totalWidth = gridSize.y * cellSize + (gridSize.y - 1) * cellSpacing;

            _cell00Pos.x = _centerPos.x - (totalWidth * 0.5f) + (cellSize * 0.5f);
            _cell00Pos.y = _centerPos.y + (totalHeight*0.5f) - (cellSize * 0.5f);
        }

        private void SetCubeSpawnPositions(GridCreationData gridCreationData, float spawnYOffset)
        {
            _spawnPositionsByColumb = new Vector3[gridCreationData.GridSize.y];

            for (int i = 0; i < gridCreationData.GridSize.y; i++)
                _spawnPositionsByColumb[i] = _cell00Pos.Add( i * _spaceBetweenCells, spawnYOffset);
        }
        
        
    }
}
