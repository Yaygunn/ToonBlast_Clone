using System;
using System.ObjectPool;
using UnityEngine;
using YBlast.Managers;
using Zenject;

namespace YBlast
{
    public class ColorCube : BaseCube, IPoolObject
    {
        public override ECubeType Type => ECubeType.ColorCube;

        private ECubeColor _color;

        private ColorCubeBlaster _cubeBlaster;

        private Action _returnToPoolCallBack;
        
        #region Getters
        public ECubeColor CubeColor => _color;
        #endregion

        [Inject]
        void Constuct(ColorCubeBlaster cubeBlaster)
        {
            _cubeBlaster = cubeBlaster;
        }

        public void SetColor(ECubeColor cubeColor)
        {
            _color = cubeColor;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public override void OnClick()
        {
            if(!_cubeBlaster.TryToBlastColorCube(CellIndex))
                base.OnClick();
        }

        public override void OnBlast()
        {
            _returnToPoolCallBack();
        }


        public void SetReturnCallBack(Action returnToPoolCallBack)
        {
            _returnToPoolCallBack = returnToPoolCallBack;
        }
    }
}
