using UnityEngine;
using System.Collections;

public enum GameMode {Menus, Game, Pause}

public class GameManager : Singleton<GameManager> 
{
    public static GameMode mode;
    public BaseCube baseCube;

    private int currentLevel = 0;

    public GameObject
        PlayerBlock,
        BasicBlock,
        JumperBlock,
        BlockerBlock;

    [HideInInspector]
    public LevelData[] levels;

    public delegate void GameManagerEvents();
    public static event GameManagerEvents
        onLevelCompleated,
        onMenuActive,
        onGameActive,
        onPauseActive;

    public static bool orthoToggle
    {
        get { return Camera.main.fieldOfView == 1f; }
        set { Camera.main.GetComponent<DollyZoom>().StartOrthoTween(value); }
    }

    public static int numBlocks
    {
        get { return Instance.baseCube.transform.childCount; }
    }

    void Awake()
    {
        GestureManager.onGesture += TouchGesture;
        levels = Resources.LoadAll<LevelData>("Levels");
        mode = GameMode.Menus;
    }

    void Start()
    {
        //Fire onMenuActive
        if (onMenuActive != null)
            onMenuActive();
    }

    public static void ToggleOrthoMode()
    {
        if (orthoToggle)
            SetOrthoMode(false);
        else
            SetOrthoMode(true);
    }

    public static void SetOrthoMode(bool value)
    {
        orthoToggle = value;
    }

    public static void isCompleated()
    {
        Instance.StartCoroutine(Instance.LateIsCompleated());
    }

    private IEnumerator LateIsCompleated()
    {
        yield return null;
        if (GameObject.FindGameObjectsWithTag("BasicBlock").Length == 0) 
            LevelCompleate();
    }

    #region MainStates
    public static void PauseGame()
    {
        mode = GameMode.Pause;
        if (onPauseActive != null)
            onPauseActive();
    }

    public static void ResumeGame()
    {
        mode = GameMode.Game;
        if (onGameActive != null)
            onGameActive();
    }

    public static void MainMenu()
    {
        mode = GameMode.Menus;
        if (onMenuActive != null)
            onMenuActive();
    }
    #endregion

    #region LevelLoading
    public static void NextLevel()
    {
        int next = Instance.currentLevel + 1;
        if (next < Instance.levels.Length)
            LoadLevel(next);
        else
            Debug.Log("LastLevel");
    }

    public static void LoadLevel(int index)
    {
        mode = GameMode.Game;

        //Fire onGameActive
        if (onGameActive != null)
            onGameActive();

        SetOrthoMode(false);

        //Clean up left over cubes
        Instance.baseCube.CleanUp();

        Instance.levels[index].CreateLevel();
        Instance.currentLevel = index;
    }

    public static void ReloadLevel()
    {
        LoadLevel(Instance.currentLevel);
    }

    public static void LevelCompleate()
    {
        mode = GameMode.Pause;

        SetOrthoMode(false);

        //Fire onLevelCompleated
        if (onLevelCompleated != null)
            onLevelCompleated();
    }
    #endregion

    #region Events
    void TouchGesture(GestureState eventData)
    {
        if (mode == GameMode.Game && eventData == GestureState.DoubleTap)
            ToggleOrthoMode();
    }
    #endregion
}
