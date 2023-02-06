namespace Source.Gameplay
{
    public abstract class GameplayController : IGameplayController, IPausable
    {
        protected bool isPaused { get; private set; }
        protected bool isDisposed { get; private set; }

        public abstract void Init();

        public void Pause()
        {
            isPaused = true;
            OnPause();
        }

        public void Resume()
        {
            isPaused = false;
            OnResume();
        }

        protected virtual void OnPause() {}
        protected virtual void OnResume() {}

        public void Dispose()
        {
            if (isDisposed)
                return;
            
            isDisposed = true;
            DisposeInternal();
        }
        
        protected virtual void DisposeInternal() {}
    }
}