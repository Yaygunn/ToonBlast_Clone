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

        private int _colorIndex;

        private ColorCubeBlaster _cubeBlaster;

        private Action _returnToPoolCallBack;

        private Vector2Int _previousCellIndex;
        
        #region Getters
        public int ColorIndex => _colorIndex;
        #endregion

        [Inject]
        void Constuct(ColorCubeBlaster cubeBlaster)
        {
            _cubeBlaster = cubeBlaster;
        }

        public void SetColor(int colorIndex)
        {
            _colorIndex = colorIndex;
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
            EventHub.ColorCubeBlasted(CellIndex,_colorIndex);
            _returnToPoolCallBack();
        }


        public void SetReturnCallBack(Action returnToPoolCallBack)
        {
            _returnToPoolCallBack = returnToPoolCallBack;
        }

        public override void SetCellIndex(Vector2Int cellIndex)
        {
            _previousCellIndex = CellIndex;
            base.SetCellIndex(cellIndex);
        }

        public override void Fall(Vector3 fallDestination)
        {
            base.Fall(fallDestination);
            EventHub.ColorCubeStartFalling(_previousCellIndex);
        }

        protected override void FallenToDestination()
        {
            base.FallenToDestination();
            EventHub.ColorCubeReachedFallDestination(CellIndex);
        }
    }
}
