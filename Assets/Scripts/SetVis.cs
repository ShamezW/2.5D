using UnityEngine;
using System.Collections;

public class SetVis : MonoBehaviour {
    public GameMode Mode;

    void Start()
    {
        Debug.Log(gameObject.name);
        GameManager.onMenuActive += () =>
        {
            if (Mode == GameMode.Menu)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        };

        GameManager.onGameActive += () =>
        {
            if (Mode == GameMode.Game)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        };
    }
}
