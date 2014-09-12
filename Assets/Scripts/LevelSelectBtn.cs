using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelSelectBtn : MonoBehaviour, IPointerClickHandler {
    [HideInInspector]
    public int index;

    public void OnPointerClick(PointerEventData data)
    {
        GameManager.LoadLevel(index);
    }
}
