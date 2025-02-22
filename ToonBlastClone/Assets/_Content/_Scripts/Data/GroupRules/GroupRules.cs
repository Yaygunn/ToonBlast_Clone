using UnityEngine;
using System;

namespace YBlast.Data
{
    [Serializable]
    public class GroupRules
    {
        [SerializeField] private int _groupSizeForA;
        [SerializeField] private int _groupSizeForB;
        [SerializeField] private int _groupSizeForC;

        public int GetColorVersion(int groupSize)
        {
            if (_groupSizeForC <= groupSize)
                return 3;
            if (_groupSizeForB <= groupSize)
                return 2;
            if (_groupSizeForA <= groupSize)
                return 1;
            return 0;
        }
    }
}
