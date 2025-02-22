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
            int padding = 1;
            int containerPadding = 3;

            int numberOfButtons = _levelData.ColorPossibilities.GetAllColors().Count + 1;
            
            _colorButtonsContainer.style.flexDirection = FlexDirection.Row;
            _colorButtonsContainer.style.flexWrap = Wrap.Wrap;

            _colorButtonsContainer.style.width =  (buttonWidth + (2 * padding)) * (numberOfButtons) + resetButtonWidth  + 2 * padding + 2 * containerPadding;
            _colorButtonsContainer.style.height = buttonHeight + (2 * containerPadding);

            _colorButtonsContainer.style.paddingBottom = containerPadding;
            _colorButtonsContainer.style.paddingRight = containerPadding;
            _colorButtonsContainer.style.paddingTop = containerPadding;
            _colorButtonsContainer.style.paddingLeft = containerPadding;
            
            AddNewButtonWithColor(ECubeColor.None);

            var colorList = _levelData.ColorPossibilities.GetAllColors();
            for (int i = 0 ; i < colorList.Count; i++)
                AddNewButtonWithColor(colorList[i]);

            AddResetButton();

            void AddNewButtonWithColor(ECubeColor cubeColor)
            {
                Button button = new Button();
                
                button.style.width = buttonWidth;
                button.style.height = buttonHeight;
                button.style.marginRight = padding ;
                button.style.marginLeft = padding ;
                button.style.marginBottom = padding ;
                button.style.marginTop = padding ;

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
                button.style.marginLeft = padding ;
                button.style.marginBottom = padding ;
                button.style.marginTop = padding ;

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
            _levelData.GridData.UpdateGridSize(evt.newValue);
            UpdateGrid();
        }
        
        private void UpdateGrid()
        {
            _gridContainer.Clear();

            int x = _levelData.GridSize.x;
            int y = _levelData.GridSize.y;

            int buttonWidth = 50;
            int buttonHeight = 50;
            int padding = 1;
            int containerPadding = 2;
                
            _gridContainer.style.flexDirection = FlexDirection.Column;
            _gridContainer.style.flexWrap = Wrap.Wrap;

            _gridContainer.style.width = (buttonWidth + 2 * padding) * y + 2 * containerPadding; 
            _gridContainer.style.height = (buttonHeight + 2 * padding) * x + 2 * containerPadding ;

            _gridContainer.style.paddingBottom = containerPadding;
            _gridContainer.style.paddingRight = containerPadding;
            _gridContainer.style.paddingTop = containerPadding;
            _gridContainer.style.paddingLeft = containerPadding;
            
            for (int i = 0; i < x * y; i++)
            {
                Button button = new Button();
                
                button.style.width = buttonWidth;
                button.style.height = buttonHeight;
                button.style.marginRight = padding ;
                button.style.marginLeft = padding ;
                button.style.marginBottom = padding ;
                button.style.marginTop = padding ;

                _gridContainer.Add(button);
                
                int index = i;
                button.clicked += () => OnGridButtonClicked(button, index);
                
                SetButtonColor(button, _levelData.GridData.GetCubeColor(i));
            }
        }

        private void OnGridButtonClicked(Button button, int index)
        {
            SetButtonColor(button, _selectedCubeColor);
            _levelData.GridData.SetColorCube(_selectedCubeColor, index);
            
            UnityEditor.EditorUtility.SetDirty(_levelData);
            UnityEditor.AssetDatabase.SaveAssets();
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
