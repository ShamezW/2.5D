using UnityEngine;
using System.Collections;

abstract public class Block : MonoBehaviour 
{
    public abstract Vector3 Use(Vector3 pos);
}
