using System;
using UnityEngine;

namespace YBlast
{
    public static class EventHub
    {
        public static event Action<Vector2Int> Ev_ColorCubeReachedFallDestination;

        public static void ColorCubeReachedFallDestination(Vector2Int cellIndex)
        {
            Ev_ColorCubeReachedFallDestination?.Invoke(cellIndex);
        }
        
        public static event Action<Vector2Int> Ev_ColorCubeStartFalling;

        public static void ColorCubeStartFalling(Vector2Int cellIndex)
        {
            Ev_ColorCubeStartFalling?.Invoke(cellIndex);
        }
        
        public static event Action<Vector2Int> Ev_ColorCubeBlasted;

        public static void ColorCubeBlasted(Vector2Int cellIndex)
        {
            Ev_ColorCubeBlasted?.Invoke(cellIndex);
        }
    }
}
