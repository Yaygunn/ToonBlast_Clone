using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YBlast.Data;
using YBlast.Managers;
using YBlast.Scriptables;
using Zenject;

namespace YBlast.UI
{
    public class GoalText : MonoBehaviour
    {
        [SerializeField] private Image _goalImage;
        [SerializeField] private TextMeshProUGUI _goalText;
        [SerializeField] private ColorCubeSpriteHolderSO _colorCubeSpriteHolderSO;

        private CubeSpriteManager _cubeSpriteManager;

        [Inject]
        void Constuct(Goals goals, CubeSpriteManager cubeSpriteManager)
        {
            _cubeSpriteManager = cubeSpriteManager;
            
            SGoal goal = goals.GetGoal();
            SetGoalImage(goal.CubeColorIndex);
            SetGoalText(goal.Amount.ToString());
        }
        
        private void OnEnable()
        {
            EventHub.Ev_UpdateGoalText += UpdateGoal;
        }
        
        private void OnDisable()
        {
            EventHub.Ev_UpdateGoalText -= UpdateGoal;
        }

        private void UpdateGoal(int cubeColorIndex, int amount)
        {
            SetGoalImage(cubeColorIndex);
            SetGoalText(amount.ToString());
        }

        private void SetGoalImage(int cubeColor)
        {
            _goalImage.sprite = _cubeSpriteManager.GetSpriteOfCubeColorIndex(cubeColor);
        }

        private void SetGoalText(string text)
        {
            _goalText.text = text;
        }

        
    }
}
