using System.Collections.Generic;
using YBlast.Data;
using UnityEngine;

namespace YBlast
{
    [CreateAssetMenu(fileName = "SO_Level", menuName = "SO/Level")]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private Vector2Int _gridSize;

        [SerializeField] private GridCreationData _gridData;

        [SerializeField] private GroupRules _groupRules;

        public GridCreationData GridData => _gridData;
        public GroupRules GroupRules => _groupRules;

        #if UNITY_EDITOR

        public void RandomlyFillGrid()
        {
            _gridData.SetGridSize(_gridSize);

            List<ECubeColor> _colorEnums = new List<ECubeColor>(
                (ECubeColor[])System.Enum.GetValues(typeof(ECubeColor)));

            Vector2Int cellIndex = new Vector2Int();
            
            for (cellIndex.x = 0; cellIndex.x < _gridSize.x; cellIndex.x++)
            {
                for (cellIndex.y = 0; cellIndex.y < _gridSize.y; cellIndex.y++)
                {
                    _gridData.SetColorCube(GetRandomCubeColor(), cellIndex);
                }
            }

            ECubeColor GetRandomCubeColor()
            {
                return _colorEnums[Random.Range(1, _colorEnums.Count)];
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }
        #endif

    }
    
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
    
    #endif
}

