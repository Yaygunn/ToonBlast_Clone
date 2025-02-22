using UnityEngine;

namespace YBlast
{
    public interface ICellPositionManager
    {
        public abstract Vector3 GetCellPos(Vector2Int cellIndex);

        public abstract Vector3 GetSpawnPos(int columb, int cubeChainOrder);

        public abstract Vector2Int GetPossibleCellIndex(Vector2 position);
    }
}
