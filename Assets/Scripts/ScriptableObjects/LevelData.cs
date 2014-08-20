using UnityEngine;
using System.Collections.Generic;

public class LevelData : ScriptableObject {
    public Vector3 PlayerBlockLoc;
    public List<Level> myLevel = new List<Level>();

    private GameObject PlayerBlock = Resources.Load<GameObject>("Blocks/Player");
    private GameObject BasicBlock = Resources.Load<GameObject>("Blocks/Basic");
    private GameObject BlockerBlock = Resources.Load<GameObject>("Blocks/Blocker");
    private GameObject JumperBlock = Resources.Load<GameObject>("Blocks/Jumper");

    public void CreateLevel()
    {
        // Create Player Cube
        GameObject.Instantiate(PlayerBlock, GetCoords(PlayerBlockLoc), Quaternion.identity);

        // Loop other blocks and spawn
        foreach (Level i in myLevel)
        {
            if (i.blockType == Level.BlockType.Basic)
            {
                GameObject.Instantiate(BasicBlock, GetCoords(i.Location), Quaternion.identity);
            }
            else if (i.blockType == Level.BlockType.Jumper)
            {
                GameObject.Instantiate(JumperBlock, GetCoords(i.Location), Quaternion.identity);
            }
            else if (i.blockType == Level.BlockType.Blocker)
            {
                GameObject.Instantiate(BlockerBlock, GetCoords(i.Location), Quaternion.identity);
            }
        }
    }

    private Vector3 GetCoords(Vector3 s)
    {
        s.x = -1.5f + (s.x - 0f) * (1.5f - -1.5f) / (3f - 0f);
        s.y = -1.5f + (s.y - 0f) * (1.5f - -1.5f) / (3f - 0f);
        s.z = -1.5f + (s.z - 0f) * (1.5f - -1.5f) / (3f - 0f);
        return s;
    }
}

[System.Serializable]
public struct Level
{
    public enum BlockType {Basic, Blocker, Jumper}

    public BlockType blockType;
    public Vector3 Location;
    public Transform Prefab;
}
