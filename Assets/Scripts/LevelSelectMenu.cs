using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelSelectMenu : MonoBehaviour {
    public GameObject uiPrefab;

	void Start() 
    {
        for (int i = 0; i < GameManager.Instance.levels.Length; i++)
        {
            GameObject obj = Instantiate(uiPrefab) as GameObject;
            obj.transform.SetParent(gameObject.transform, false);
            obj.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString("00");
            obj.GetComponent<LevelSelectBtn>().index = i;
        }
	}
}
