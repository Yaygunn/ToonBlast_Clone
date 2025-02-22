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

        [Inject]
        void Constuct(Goals goals)
        {
            SGoal goal = goals.GetGoal();
            SetGoalImage(goal.DesiredCubeColor);
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
            SetGoalText(amount.ToString());
        }

        private void SetGoalImage(ECubeColor cubeColor)
        {
            _goalImage.sprite = _colorCubeSpriteHolderSO.GetSprite(cubeColor,ECubeColorVersion.Default);
        }

        private void SetGoalText(string text)
        {
            _goalText.text = text;
        }

        
    }
}
