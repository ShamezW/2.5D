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
                Move(Vector3.left);
        }
        else if (GestureManager.State == GestureState.SwipeRight)
        {
            if (RayHit(GestureManager.orgLoc))
                Move(Vector3.right);
        }
        else if (GestureManager.State == GestureState.SwipeUp)
        {
            if (RayHit(GestureManager.orgLoc))
                Move(Vector3.up);
        }
        else if (GestureManager.State == GestureState.SwipeDown)
        {
            if (RayHit(GestureManager.orgLoc))
                Move(Vector3.down);
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
        Vector3 rayDest = transform.position + dir;
        rayDest = Camera.main.WorldToScreenPoint(rayDest);
        Ray ray = Camera.main.ScreenPointToRay(rayDest);
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("BasicBlock"))
        {
            transform.position = hit.transform.position;
            Destroy(hit.transform.gameObject);
            GameManager.numBlocks--;
        }
        if (GameManager.numBlocks == 0)
            GameManager.LevelCompleate();
    }
}
