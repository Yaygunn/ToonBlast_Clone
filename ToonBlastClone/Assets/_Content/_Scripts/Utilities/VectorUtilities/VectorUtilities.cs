using System;
using UnityEngine;

namespace YBlast.Utilities
{
    public static class VectorUtilities 
    {
        public static void OperateOnEachIndex(Vector2Int gridSize, Action<Vector2Int> operationAction )
        {
            Vector2Int cellIndex = Vector2Int.zero;

            for (cellIndex.x = 0; cellIndex.x < gridSize.x; cellIndex.x++)
            {
                for (cellIndex.y = 0; cellIndex.y < gridSize.y; cellIndex.y++)
                {
                    operationAction(cellIndex);
                }
            }
        }
    }
}
