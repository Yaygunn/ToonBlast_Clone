using System;
using UnityEngine;
using YBlast.Data;
using YBlast.Systems;
using Zenject;

namespace YBlast.Managers
{
    public class GoalManager : IDisposable
    {
        private SGoal _goal;
        
        [Inject]
        void Construct(Goals goals)
        {
            _goal = goals.GetGoal();
            
            EventHub.Ev_ColorCubeBlasted += OnColorCubeBlasted;
        }
        
        public void Dispose()
        {
            EventHub.Ev_ColorCubeBlasted -= OnColorCubeBlasted;
        }

        private void OnColorCubeBlasted(Vector2Int cellIndex, int cubeColorIndex)
        {
            if (cubeColorIndex != _goal.CubeColorIndex)
                return;

            _goal.Amount--;
            EventHub.UpdateGoalText(_goal.CubeColorIndex, _goal.Amount);
            
            if(_goal.Amount == 0)
                GameWon();
        }

        private void GameWon()
        {
            EventHub.Ev_ColorCubeBlasted -= OnColorCubeBlasted;
            
            SaveSystem.IncreaseLevel();
            
            EventHub.GameWon();
            
        }
    }
}
