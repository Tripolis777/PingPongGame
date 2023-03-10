using Cysharp.Threading.Tasks;
using Source.Core.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Core
{
    public sealed class WorldController : MonoBehaviorSingleton<WorldController>
    {
        private const string DEFAUL_SCENE_NAME = "GameScene";
        
        [SerializeField] private WorldCameraController worldCameraController;

        public GameScene CurrentScene { get; private set; }
        
        public WorldCameraController WorldCameraController => worldCameraController;

        public async UniTask PreloadDefaultScene()
        {
            var scene = GetFirstLoadedScene();
            if (scene != null)
                await scene.Init();
            else
                await LoadScene();
        }

        public async UniTask LoadScene(string name = null)
        {
            if (CurrentScene != null)
                await UnloadCurrentScene();

            CurrentScene = await LoadSceneInternal(name);
            await CurrentScene.Init();
        }

        private async UniTask UnloadCurrentScene()
        {
            var scene = CurrentScene.gameObject.scene;
            await SceneManager.UnloadSceneAsync(scene);
        }
        
        private async UniTask<GameScene> LoadSceneInternal(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                name = DEFAUL_SCENE_NAME;

            var scene = SceneManager.GetSceneByName(name);
            if (scene.isLoaded)
                return FindGameScene(scene);
            
            await SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            return FindObjectOfType<GameScene>();
        }

        private static GameScene GetFirstLoadedScene()
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var gameScene = FindGameScene(SceneManager.GetSceneAt(i));
                if (gameScene != null)
                    return gameScene;
            }

            return null;
        }

        private static GameScene FindGameScene(Scene scene)
        {
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                var component = rootGameObject.GetComponentInChildren<GameScene>(true);
                if (component != null)
                    return component;
            }

            return null;
        }
    }
}