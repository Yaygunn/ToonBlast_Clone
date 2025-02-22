using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YBlast.Data
{
    [Serializable]
    public class ColorPossibilities
    {
        [SerializeField] private List<ECubeColor> _colorPossibilities;
        
        public Dictionary<ECubeColor, int> GetColorIndexDictionary()
        {
            Dictionary<ECubeColor, int> _colorIndexes = new(_colorPossibilities.Count);

            for (int i = 0 ; i < _colorPossibilities.Count ; i++)
                _colorIndexes.Add(_colorPossibilities[i], i);

            return _colorIndexes;
        }
        
        public int GetRandomColorIndex()
        {
            return Random.Range(0, _colorPossibilities.Count);
        }

        public List<ECubeColor> GetAllColors()
        {
            return _colorPossibilities;
        }
    }
}
