using Source.View;

namespace Source.Gameplay.Player
{
    public static class PlayerInputFactory
    {
        public static IPlayerInput CreatePlayerInput(PlayerInputSettings inputSettings, PlayerViewComponent view)
        {
            if (inputSettings is PlayerHumanSettings humanSettings)
            {
                var camera = humanSettings.gameCamera;
                var distance = camera.transform.position - view.transform.position;
                return new HumanPlayerInput(humanSettings, distance.y);
            }

            return null;
        }
    }
}