using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using YBlast.Data;
using YBlast.Scriptables;

namespace YBlast.Editorr
{
    [CustomEditor(typeof(LevelDataSO))]
    public class LevelDataEditor : Editor
    {
        [SerializeField] private VisualTreeAsset _visualTree;

        [SerializeField] private ColorCubeSpriteHolderSO _colorCubeSpriteHolderSO;

        private LevelDataSO _levelData;
        
        private VisualElement _document;
        
        private VisualElement _gridContainer;
        private VisualElement _colorButtonsContainer;

        private ECubeColor _selectedCubeColor;

        private void OnEnable()
        {
            _levelData = (LevelDataSO)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            _document = new VisualElement();
            _visualTree.CloneTree(_document);

            HandleGrid();
            
            PropertyField colorPossibilities = _document.Q<PropertyField>("ColorPossibilities");
            colorPossibilities.RegisterCallback<ChangeEvent<Vector2Int>>((val)=> Debug.Log("value changed"));


            return _document;
        }

        #region GridCreation

        private void HandleGrid()
        {
            _gridContainer = _document.Q<VisualElement>("GridContainer");
            _colorButtonsContainer = _document.Q<VisualElement>("ColorButtonContainer");

            PropertyField gridSize = _document.Q<PropertyField>("GridSize");
            gridSize.RegisterCallback<ChangeEvent<Vector2Int>>(ResetGrid);
            
            UpdateGrid();
            UpdateColorButtons();
        }

        private void ReserColorButtons()
        {
            UpdateColorButtons();
        }
        
        private void UpdateColorButtons()
        {
            _colorButtonsContainer.Clear();
            
            int buttonWidth = 50;
            int resetButtonWidth = 100;
            int buttonHeight = 50;
            int padding = 5;

            int numberOfButtons = _levelData.ColorPossibilities.GetAllColors().Count + 1;
            
            _colorButtonsContainer.style.flexDirection = FlexDirection.Row;
            _colorButtonsContainer.style.flexWrap = Wrap.Wrap;

            _colorButtonsContainer.style.width = resetButtonWidth + buttonWidth * numberOfButtons + (2 * padding) * (numberOfButtons + 1);
            _colorButtonsContainer.style.height = buttonHeight + (2 * padding);

            _colorButtonsContainer.style.paddingBottom = padding;
            _colorButtonsContainer.style.paddingRight = padding;
            _colorButtonsContainer.style.paddingTop = padding;
            _colorButtonsContainer.style.paddingLeft = padding;
            
            AddNewButtonWithColor(ECubeColor.None);

            foreach (var cubeColor in _levelData.ColorPossibilities.GetAllColors())
                AddNewButtonWithColor(cubeColor);

            AddResetButton();

            void AddNewButtonWithColor(ECubeColor cubeColor)
            {
                Button button = new Button();
                
                button.style.width = buttonWidth;
                button.style.height = buttonHeight;
                button.style.marginRight = padding ;
                button.style.marginBottom = padding ;

                _colorButtonsContainer.Add(button);
                
                button.clicked += () => OnColorButtonClicked(cubeColor);
                
                SetButtonColor(button, cubeColor);
            }

            void AddResetButton()
            {
                Button button = new Button();
                
                button.style.width = resetButtonWidth;
                button.style.height = buttonHeight;
                button.style.marginRight = padding ;
                button.style.marginBottom = padding ;

                button.text = "Update Colors";

                _colorButtonsContainer.Add(button);
                
                button.clicked += UpdateColorButtons;
            }
        }

        private void OnColorButtonClicked(ECubeColor cubeColor)
        {
            _selectedCubeColor = cubeColor;
        }
        
        private void ResetGrid(ChangeEvent<Vector2Int> evt)
        {
            UpdateGrid();
        }
        
        private void UpdateGrid()
        {
            _gridContainer.Clear();

            int x = _levelData.GridSize.x;
            int y = _levelData.GridSize.y;

            int buttonWidth = 50;
            int buttonHeight = 50;
            int padding = 5;
                
            _gridContainer.style.flexDirection = FlexDirection.Row;
            _gridContainer.style.flexWrap = Wrap.Wrap;

            _gridContainer.style.width = buttonWidth * y + (2 * padding) * (y - 0.5f); 
            _gridContainer.style.height = buttonHeight * x + (2 * padding) * (x- 1.5f);

            _gridContainer.style.paddingBottom = padding;
            _gridContainer.style.paddingRight = padding;
            _gridContainer.style.paddingTop = padding;
            _gridContainer.style.paddingLeft = padding;
            
            for (int i = 0; i < x * y; i++)
            {
                Button button = new Button();
                
                button.style.width = buttonWidth;
                button.style.height = buttonHeight;
                button.style.marginRight = padding ;
                button.style.marginBottom = padding ;

                _gridContainer.Add(button);
                
                int index = i;
                button.clicked += () => OnGridButtonClicked(button, index);
            }
        }

        private void OnGridButtonClicked(Button button, int index)
        {
            Debug.Log(index);
        }

        private void SetButtonColor(Button button, ECubeColor cubeColor)
        {
            if (cubeColor == ECubeColor.None)
            {
                button.style.backgroundImage = null;
                return;
            }
            
            button.style.backgroundImage = new StyleBackground(_colorCubeSpriteHolderSO.GetSprite(cubeColor, ECubeColorVersion.Default));
        }
        
        #endregion
    }
}
