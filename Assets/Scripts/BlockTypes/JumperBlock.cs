using UnityEngine;
using System.Collections;

public class JumperBlock : Block 
{
    public override Vector3 Use(Vector3 pos)
    {
        return pos + ((transform.position - pos) * 2);
    }
}
