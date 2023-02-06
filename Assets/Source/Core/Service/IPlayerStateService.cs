namespace Source.Core.Service
{
    public interface IPlayerStateService : IService
    {
        public PlayerState GetState();
        public void SaveState();
    }
}