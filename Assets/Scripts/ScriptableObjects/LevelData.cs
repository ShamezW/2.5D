using UnityEngine;
using System.Collections.Generic;

public class LevelData : ScriptableObject {
    public Vector3 PlayerBlockLoc;
    public List<Level> myLevel = new List<Level>();

    public void CreateLevel()
    {
        GameObject block;
        Transform parent = GameObject.Find("BaseCube").transform;

        // Create Player Cube
        GameObject playerCube = (GameObject)GameObject.Instantiate(GameManager.Instance.PlayerBlock, GetCoords(PlayerBlockLoc), Quaternion.identity);
        playerCube.transform.parent = parent;

        // Loop other blocks and spawn
        foreach (Level i in myLevel)
        {
            if (i.blockType == Level.BlockType.Basic)
            {
                block = (GameObject)GameObject.Instantiate(GameManager.Instance.BasicBlock, GetCoords(i.Location), Quaternion.identity);
                block.transform.parent = parent;
            }
            else if (i.blockType == Level.BlockType.Jumper)
            {
                block = (GameObject)GameObject.Instantiate(GameManager.Instance.JumperBlock, GetCoords(i.Location), Quaternion.identity);
                block.transform.parent = parent;
            }
            else if (i.blockType == Level.BlockType.Blocker)
            {
                block = (GameObject)GameObject.Instantiate(GameManager.Instance.BlockerBlock, GetCoords(i.Location), Quaternion.identity);
                block.transform.parent = parent;
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
