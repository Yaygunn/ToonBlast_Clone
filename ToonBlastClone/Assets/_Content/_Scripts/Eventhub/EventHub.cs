using System;
using UnityEngine;

namespace YBlast
{
    public static class EventHub
    {
        public static event Action<Vector2Int> Ev_CubeReachedFallDestination;

        public static void CubeReachedFallDestination(Vector2Int cellIndex)
        {
            Ev_CubeReachedFallDestination?.Invoke(cellIndex);
        }
    }
}
