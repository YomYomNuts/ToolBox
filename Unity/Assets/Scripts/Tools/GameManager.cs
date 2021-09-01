using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum EGameState
{
    MENU = 0,
    PAUSE,
    INGAME,
    LENGHT
};

public enum EnumScene
{
    None = -1,
    Loading = 0,
    Menu = 1,
    Game = 2,
    Length
};

public partial class GameManager : UnitySingletonPersistent<GameManager>
{
    public class PauseEvent : UnityEvent<bool> {}
    [HideInInspector, System.NonSerialized]
    public PauseEvent eventOnPause = new PauseEvent();
    [HideInInspector, System.NonSerialized]
    public UnityEvent eventOnUnloadGame = new UnityEvent();

    #region Public Attributs
    public static bool IsGameExitting = false;

    [HideInInspector]
    public EnumScene previousScene;
    [HideInInspector]
    public EnumScene currentScene;
    [HideInInspector]
    public EnumScene nextScene;

    [Header("Datas")]
    public GameData _GameData;
    #endregion

    #region Private Attributs
    protected EGameState GameState;
    #endregion

    #region Getter Game
    public bool GameIsPause()
    {
        return this.GameState == EGameState.PAUSE;
    }
    public bool IsInGame()
    {
        return this.GameState == EGameState.INGAME;
    }
    public bool IsInMenu()
    {
        return this.GameState == EGameState.MENU;
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 60;
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.wantsToQuit += WantsToQuit;
    }

    protected static bool WantsToQuit()
    {
        IsGameExitting = true;
        return true;
    }

    protected virtual void Start()
    {
        UnloadGame();
        Init();
    }

    private void Init()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Main")
        {
            previousScene = EnumScene.None;
            currentScene = EnumScene.None;
            nextScene = EnumScene.None;
            StartCoroutine(FirstLaunch());
        }
        else
        {
            previousScene = EnumScene.Menu;
            nextScene = currentScene = EnumScene.Menu;
        }
    }

    private IEnumerator FirstLaunch()
    {
        yield return null;
        SwitchScene(EnumScene.Menu, true);
    }

    public void SwitchScene(EnumScene _enumScene, bool _instant = false, Color? _color = null)
    {
        previousScene = currentScene;
        nextScene = _enumScene;

        // Fade
        Color color = _color ?? Color.black;
        ScreenFader.Instance.FadeIn(color, _instant ? 0.0f : _GameData.TimeFadeInOutLoading.x, _action: EndFade);
    }

    private void EndFade()
    {
        UnloadGame();
        currentScene = EnumScene.Loading;
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Single);
    }

    public virtual void EndFadeOutLoad()
    {
    }

    public void PauseGame(bool _pause)
    {
        this.GameState = _pause ? EGameState.PAUSE : EGameState.INGAME;

        if (eventOnPause != null)
        {
            eventOnPause.Invoke(_pause);
        }
    }

    public void UnloadGame()
    {
        this.GameState = EGameState.MENU;

        if (this.eventOnUnloadGame != null)
        {
            this.eventOnUnloadGame.Invoke();
        }
    }
}
