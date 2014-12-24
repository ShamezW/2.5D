using UnityEngine;
using System.Collections;

public class BasicBlock : Block
{
    public override Vector3 Use(Vector3 pos)
    {
        Destroy(gameObject);
        GameManager.isCompleated();
        return transform.position;
    }
}
