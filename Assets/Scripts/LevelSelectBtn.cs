using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class LevelSelectBtn : MonoBehaviour {
    public int index;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadLevel);
    }

    void LoadLevel()
    {
        GameManager.LoadLevel(index);
    }
}
