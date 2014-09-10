using UnityEngine;
using System.Collections;

public enum GameMode {Menu, Game}

public class GameManager : Singleton<GameManager> {
    public Animator cameraAnimator;
    public static bool orthoToggle = false;
    public static int numBlocks;

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
        onGameActive;

    void Awake()
    {
        levels = Resources.LoadAll<LevelData>("Levels");
    }

    void Start()
    {
        if (onMenuActive != null)
            onMenuActive();
    }

    void Update()
    {
        if (GestureManager.State == GestureState.DoubleTap)
            ToggleOrthoMode();
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

    public static void LevelCompleate()
    {
        if (onLevelCompleated != null)
            onLevelCompleated();
        Instance.ToggleOrthoMode();
    }

    public static void LoadLevel(int index)
    {
        Instance.levels[index].CreateLevel();
        numBlocks = GameObject.FindGameObjectsWithTag("BasicBlock").Length;
        if (onGameActive != null)
            onGameActive();
    }
}
