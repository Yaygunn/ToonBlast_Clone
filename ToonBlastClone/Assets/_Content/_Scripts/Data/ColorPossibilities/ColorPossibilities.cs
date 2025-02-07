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

        public ECubeColor GetRandomColor()
        {
            return _colorPossibilities[Random.Range(0, _colorPossibilities.Count)];
        }
    }
}
