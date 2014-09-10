using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    public Animator cameraAnimator;
    public static bool orthoToggle = false;
    public static int numBlocks;

    public GameObject PlayerBlock;
    public GameObject BasicBlock;
    public GameObject JumperBlock;
    public GameObject BlockerBlock;

    public delegate void GameManagerEvents();
    public static event GameManagerEvents onLevelCompleated;

    void Start()
    {
        LevelData[] levels = Resources.LoadAll<LevelData>("Levels");
        levels[0].CreateLevel();
        numBlocks = GameObject.FindGameObjectsWithTag("BasicBlock").Length;
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
}
