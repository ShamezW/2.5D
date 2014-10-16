using UnityEngine;
using System.Collections;

public enum GameMode {Menus, Game, Pause}

public class GameManager : Singleton<GameManager> {
    public static GameMode mode;

    public Animator cameraAnimator;
    public static bool orthoToggle = false;
    //public static int numBlocks;

    public BaseCube baseCube;
    private Player player;

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

    public static int numBlocks
    {
        get { return Instance.baseCube.transform.childCount; }
    }

    void Awake()
    {
        levels = Resources.LoadAll<LevelData>("Levels");

        //Init Gamemode
        mode = GameMode.Menus;
    }

    void Start()
    {
        //Fire onMenuActive
        if (onMenuActive != null)
            onMenuActive();
    }

    void Update()
    {
        switch(mode)
        {
            case GameMode.Menus:
                break;

            case GameMode.Game:
                if (GestureManager.State == GestureState.DoubleTap)
                    ToggleOrthoMode();
                break;

            case GameMode.Pause:
                break;
        }
    }

    public void ToggleOrthoMode()
    {
        if (!orthoToggle)
        {
            Instance.cameraAnimator.SetTrigger("Ortho");
            orthoToggle = true;
        }
        else
        {
            Instance.cameraAnimator.SetTrigger("Persp");
            orthoToggle = false;
        }
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

        //Clean up left over cubes
        Instance.baseCube.CleanUp();

        Instance.levels[index].CreateLevel();
        Instance.currentLevel = index;
        Instance.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public static void ReloadLevel()
    {
        LoadLevel(Instance.currentLevel);
    }

    public static void LevelCompleate()
    {
        mode = GameMode.Pause;

        Instance.ToggleOrthoMode();

        //Fire onLevelCompleated
        if (onLevelCompleated != null)
            onLevelCompleated();
    }
    #endregion
}
