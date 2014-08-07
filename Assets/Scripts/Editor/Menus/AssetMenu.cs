using UnityEngine;
using UnityEditor;

public class AssetMenu
{
    [MenuItem("Assets/Create/Level Data")]
    public static void CreateLevelData()
    {
        ScriptableObjectUtility.CreateAsset<LevelData>();
    }
}
