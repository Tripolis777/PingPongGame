namespace Source.Core.Service
{
    public interface IStorageDataService : IService
    {
        public bool TryLoadData<T>(out T data);

        public void SaveData<T>(T data);
    }
}