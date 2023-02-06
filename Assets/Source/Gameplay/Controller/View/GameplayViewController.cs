using Source.View;

namespace Source.Gameplay
{
    public abstract class GameplayViewController<T> : GameplayController where T : ViewComponent
    {
        public T View { get; private set; }

        public GameplayViewController(T view)
        {
            View = view;
        }

        public override void Init()
        {
            View.Init(this);
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            View = null;
        }
    }
}