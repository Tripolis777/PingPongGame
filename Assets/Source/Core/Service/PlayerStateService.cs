namespace Source.Core.Service
{
    public class PlayerStateService : IPlayerStateService
    {
        private PlayerState _cachedState;

        public PlayerState GetState()
        {
            if (_cachedState == null)
            {
                var storageService = ServiceLocator.Instance.GetService<IStorageDataService>();
                if (!storageService.TryLoadData(out _cachedState))
                    _cachedState = new PlayerState();
            }

            return _cachedState;
        }

        public void SaveState()
        {
            if (_cachedState == null)
                return;

            var storageService = ServiceLocator.Instance.GetService<IStorageDataService>();
            storageService.SaveData(_cachedState);
        }
    }
}