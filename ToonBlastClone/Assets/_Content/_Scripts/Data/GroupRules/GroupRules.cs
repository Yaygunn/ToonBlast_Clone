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

        public ECubeColorVersion GetColorVersion(int groupSize)
        {
            if (_groupSizeForC <= groupSize)
                return ECubeColorVersion.C;
            if (_groupSizeForB <= groupSize)
                return ECubeColorVersion.B;
            if (_groupSizeForA <= groupSize)
                return ECubeColorVersion.A;
            return ECubeColorVersion.Default;
        }
    }
}
