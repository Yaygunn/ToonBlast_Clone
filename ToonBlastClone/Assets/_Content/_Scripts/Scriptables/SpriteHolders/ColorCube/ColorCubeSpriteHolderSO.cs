using UnityEngine;
using YBlast.Data;

namespace YBlast.Scriptables
{
    [CreateAssetMenu(menuName = "SO/Holder/Sprite/ColorCube")]
    public class ColorCubeSpriteHolderSO : ScriptableObject
    {
        [SerializeField] private SCubeColors _blue;
        [SerializeField] private SCubeColors _green;
        [SerializeField] private SCubeColors _pink;
        [SerializeField] private SCubeColors _purple;
        [SerializeField] private SCubeColors _red;
        [SerializeField] private SCubeColors _yellow;

        public Sprite[] GetColorSprites(ECubeColor cubeColor)
        {
            Sprite[] sprites = new Sprite [4];
            SCubeColors sCubeColors = GetSCubeColors(cubeColor);

            sprites[0] = sCubeColors.Default;
            sprites[1] = sCubeColors.A;
            sprites[2] = sCubeColors.B;
            sprites[3] = sCubeColors.C;
            
            return sprites;
        }
        
        private SCubeColors GetSCubeColors(ECubeColor cubeColor)
        {
            switch (cubeColor)
            {
                case ECubeColor.Blue:
                    return _blue;
                case ECubeColor.Green:
                    return _green;
                case ECubeColor.Pink:
                    return _pink;
                case ECubeColor.Purple:
                    return _purple;
                case ECubeColor.Red:
                    return _red;
                case ECubeColor.Yellow:
                    return _yellow;
            }
            return _yellow;
        }
        public Sprite GetSprite(ECubeColor cubeColor, ECubeColorVersion colorVersion)
        {
            switch (cubeColor)
            {
                case ECubeColor.Blue:
                    return GetSpriteVersion(_blue, colorVersion);
                case ECubeColor.Green:
                    return GetSpriteVersion(_green, colorVersion);
                case ECubeColor.Pink:
                    return GetSpriteVersion(_pink, colorVersion);
                case ECubeColor.Purple:
                    return GetSpriteVersion(_purple, colorVersion);
                case ECubeColor.Red:
                    return GetSpriteVersion(_red, colorVersion);
                case ECubeColor.Yellow:
                    return GetSpriteVersion(_yellow, colorVersion);
            }
            
            Debug.LogError("color version does not exist");
            return null;
        }

        private Sprite GetSpriteVersion(SCubeColors cubeColors, ECubeColorVersion colorVersion)
        {
            switch (colorVersion)
            {
                case ECubeColorVersion.Default:
                    return cubeColors.Default;
                case ECubeColorVersion.A:
                    return cubeColors.A;
                case ECubeColorVersion.B:
                    return cubeColors.B;
                case ECubeColorVersion.C:
                    return cubeColors.C;
            }

            Debug.LogError("color version does not exist");
            return null;
        }
    
        [System.Serializable]
        private struct SCubeColors
        {
            public Sprite Default;
            public Sprite A;
            public Sprite B;
            public Sprite C;
        }
    }
}
