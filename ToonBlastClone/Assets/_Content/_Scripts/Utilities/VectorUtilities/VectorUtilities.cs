using System;
using System.Collections.Generic;
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

        public static List<Vector2Int> GetAllIndexesOfGridAsList(Vector2Int gridSize)
        {
            List<Vector2Int> indexList = new();

            OperateOnEachIndex(gridSize, (cellIndex)=> indexList.Add(cellIndex));

            return indexList;
        }
    }
}
