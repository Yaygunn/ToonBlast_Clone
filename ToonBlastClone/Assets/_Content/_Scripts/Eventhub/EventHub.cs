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
        
        public static event Action<Vector2Int, int> Ev_ColorCubeBlasted;

        public static void ColorCubeBlasted(Vector2Int cellIndex, int cubeColor)
        {
            Ev_ColorCubeBlasted?.Invoke(cellIndex, cubeColor);
        }

        public static event Action<int, int> Ev_UpdateGoalText;

        public static void UpdateGoalText(int cubeColorIndex, int amount)
        {
            Ev_UpdateGoalText?.Invoke(cubeColorIndex, amount);
        }

        public static event Action Ev_GameWon;

        public static void GameWon()
        {
            Ev_GameWon?.Invoke();
        }
    }
}
