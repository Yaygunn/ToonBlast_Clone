using UnityEngine;

namespace YBlast
{
    [CreateAssetMenu(fileName = "SO_GridCreation", menuName = "SO/Level/GridCreationData")]
    public class GridCreationDataSO : ScriptableObject
    {
        [SerializeField] private Vector2Int _gridSize;

        [SerializeField] private GridCreationData _gridData;

        public GridCreationData GridData => _gridData;

        #if UNITY_EDITOR

        public void RandomlyFillGrid()
        {
            _gridData.SetGridSize(_gridSize);

            Vector2Int gridIndex = new Vector2Int();
            
            for (gridIndex.x = 0; gridIndex.x < _gridSize.x; gridIndex.x++)
            {
                for (gridIndex.y = 0; gridIndex.y < _gridSize.y; gridIndex.y++)
                {
                    _gridData.SetColorCube(GetRandomCubeColor(), gridIndex);
                }
            }

            ECubeColor GetRandomCubeColor()
            {
                System.Array colors = System.Enum.GetValues(typeof(ECubeColor));
                int randomIndex = Random.Range(1, colors.Length);
                return (ECubeColor)colors.GetValue(randomIndex);
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }
        #endif

    }
    
    #if UNITY_EDITOR
    
    [UnityEditor.CustomEditor(typeof(GridCreationDataSO))]
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
                GridCreationDataSO gridData = (GridCreationDataSO)target;
                gridData.RandomlyFillGrid();
            }
        }
    }
    
    #endif
}

