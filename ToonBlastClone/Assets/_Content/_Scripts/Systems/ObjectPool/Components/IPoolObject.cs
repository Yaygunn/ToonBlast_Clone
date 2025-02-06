namespace System.ObjectPool
{
    public interface IPoolObject
    {
        public abstract void SetReturnCallBack(Action returnToPoolCallBack);
    }
}
