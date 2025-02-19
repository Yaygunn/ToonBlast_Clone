using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace YBlast.Editorr
{
    [CustomEditor(typeof(LevelDataSO))]
    public class LevelDataEditor : Editor
    {
        [SerializeField] private VisualTreeAsset _visualTree;
        
        private VisualElement _document;
        
        public override VisualElement CreateInspectorGUI()
        {
            _document = new VisualElement();
            _visualTree.CloneTree(_document);

            return _document;
        }
    }
}
