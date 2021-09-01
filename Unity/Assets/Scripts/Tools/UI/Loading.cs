using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private List<string> levelToLoad = new List<string>();
    private bool sceneFinishLoad;

    void Start()
    {
        GameManager.Instance.UnloadGame();

        levelToLoad.Clear();
        switch (GameManager.Instance.nextScene)
        {
            case EnumScene.Menu:
                {
                    levelToLoad.Add("Menu");
                }
                break;
            case EnumScene.Game:
                {
                    levelToLoad.Add("Game");
                }
                break;
        }

        // Fade
        ScreenFader.Instance.FadeOut(Color.clear, GameManager.Instance._GameData.TimeFadeInOutLoading.y, _action: EndFadeOut);
    }

    private void EndFadeOut()
    {
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; ++i)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "Loading")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        for (int i = 0; i < levelToLoad.Count; ++i)
        {
            SceneManager.LoadSceneAsync(levelToLoad[i], LoadSceneMode.Additive);
        }

        StartCoroutine(Load());
    }

	public IEnumerator Load()
	{
        float timeWaiting = GameManager.Instance._GameData.TimeWaitingLoading;
        yield return new WaitForSeconds(timeWaiting);

        while (!sceneFinishLoad)
        {
            yield return null;
        }
        LaunchOutLoading();
    }

    private void LaunchOutLoading()
    {
        // Fade
        ScreenFader.Instance.FadeIn(Color.black, GameManager.Instance._GameData.TimeFadeInOutLoading.x, _action: EndFadeIn);
    }

    private void EndFadeIn()
    {
        SceneManager.UnloadSceneAsync("Loading");

        // Fade
        ScreenFader.Instance.FadeOut(Color.clear, GameManager.Instance._GameData.TimeFadeInOutLoading.y, _action: GameManager.Instance.EndFadeOutLoad);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        sceneFinishLoad = true;
        GameManager.Instance.currentScene = GameManager.Instance.nextScene;
    }
}
