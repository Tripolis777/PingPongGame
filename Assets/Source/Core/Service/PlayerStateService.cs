namespace Source.Core.Service
{
    public class PlayerStateService : IPlayerStateService
    {
        private PlayerState cachedState;

        public PlayerState GetState()
        {
            if (cachedState == null)
            {
                var storageService = ServiceLocator.Instance.GetService<IStorageDataService>();
                if (!storageService.TryLoadData(out cachedState))
                    cachedState = new PlayerState();
            }

            return cachedState;
        }

        public void SaveState()
        {
            if (cachedState == null)
                return;

            var storageService = ServiceLocator.Instance.GetService<IStorageDataService>();
            storageService.SaveData(cachedState);
        }
    }
}