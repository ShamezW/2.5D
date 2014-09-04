using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private RaycastHit hit;

	void Update () 
    {
        if (GameManager.orthoToggle)
            SwipeControls();
	}

    void SwipeControls()
    {
        if (GestureManager.State == GestureState.SwipeLeft)
        {
            if (RayHit(GestureManager.orgLoc))
                transform.Translate(Vector3.left, Space.World);
        }
        else if (GestureManager.State == GestureState.SwipeRight)
        {
            if (RayHit(GestureManager.orgLoc))
                transform.Translate(Vector3.right, Space.World);
        }
        else if (GestureManager.State == GestureState.SwipeUp)
        {
            if (RayHit(GestureManager.orgLoc))
                transform.Translate(Vector3.up, Space.World);
        }
        else if (GestureManager.State == GestureState.SwipeDown)
        {
            if (RayHit(GestureManager.orgLoc))
                transform.Translate(Vector3.down, Space.World);
        }
    }

    bool RayHit(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Player"))
            return true;
        else
            return false;
    }

    void Move(Vector3 dir)
    {

    }
}
