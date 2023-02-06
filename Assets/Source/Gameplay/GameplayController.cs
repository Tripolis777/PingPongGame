namespace Source.Gameplay
{
    public abstract class GameplayController : IGameplayController, IPausable
    {
        protected bool isPaused { get; private set; }

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
        
        public virtual void Dispose() {}
    }
}