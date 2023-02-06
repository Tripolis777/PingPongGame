using Cysharp.Threading.Tasks;
using Source.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Initialization
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static async UniTaskVoid AfterInitializeScene()
    {
        var rootScene = SceneManager.GetSceneByBuildIndex(0);
        if (!rootScene.isLoaded)
            await SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);

        await WorldController.Instance.PreloadDefaultScene();
    }
}
