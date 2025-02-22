using System.Collections.Generic;
using YBlast.Data;
using UnityEngine;

namespace YBlast
{
    [CreateAssetMenu(fileName = "SO_Level", menuName = "SO/Level")]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private Vector2Int _gridSize = new Vector2Int();

        [SerializeField] private GridCreationData _gridData;

        [SerializeField] private ColorPossibilities _colorPossibilities;
        
        [SerializeField] private GroupRules _groupRules;

        [SerializeField] private Goals _goals;


        public Vector2Int GridSize => _gridSize;
        public GridCreationData GridData => _gridData;
        public ColorPossibilities ColorPossibilities => _colorPossibilities;
        public GroupRules GroupRules => _groupRules;
        public Goals Goals => _goals;

        /*#if UNITY_EDITOR

        public void RandomlyFillGrid()
        {
            _gridData.SetGridSize(_gridSize);

            Vector2Int cellIndex = new Vector2Int();
            
            for (cellIndex.x = 0; cellIndex.x < _gridSize.x; cellIndex.x++)
            {
                for (cellIndex.y = 0; cellIndex.y < _gridSize.y; cellIndex.y++)
                {
                    _gridData.SetColorCube(_colorPossibilities.GetRandomColor(), cellIndex);
                }
            }

            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }
        #endif
*/
    }
    /*
    #if UNITY_EDITOR
    
    [UnityEditor.CustomEditor(typeof(LevelDataSO))]
    public class GridCreationDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Call the base class method to draw the default inspector
            base.OnInspectorGUI();

            // Add a button to the inspector
            if (GUILayout.Button("Randomly Fill Grid"))
            {
                // Get the target object and call the method
                LevelDataSO gridData = (LevelDataSO)target;
                gridData.RandomlyFillGrid();
            }
        }
    }
    
    #endif*/
}

