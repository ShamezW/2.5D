using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private RaycastHit hit;

    void OnEnable()
    {
        DollyZoom.onOrtho += checkVis;
        GestureManager.onGesture += OnGesture;
    }

    void OnDisable()
    {
        DollyZoom.onOrtho -= checkVis;
        GestureManager.onGesture -= OnGesture;
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
            DestroyImmediate(hit.transform.gameObject);
            GameManager.isCompleated();
        }
    }

    public void checkVis() //FIXME
    {
        Vector3 rayDest = transform.position;
        rayDest = Camera.main.WorldToScreenPoint(rayDest);
        Ray ray = Camera.main.ScreenPointToRay(rayDest);
        if (Physics.Raycast(ray, out hit) && !hit.transform.CompareTag("Player"))
            GameManager.SetOrthoMode(false);
    }

    #region Events
    void OnGesture(GestureState eventData)
    {
        if (GameManager.mode == GameMode.Game && GameManager.orthoToggle)
        {
            if (eventData == GestureState.SwipeLeft)
            {
                if (RayHit(GestureManager.orgLoc))
                    Move(Vector3.left);
            }
            else if (eventData == GestureState.SwipeRight)
            {
                if (RayHit(GestureManager.orgLoc))
                    Move(Vector3.right);
            }
            else if (eventData == GestureState.SwipeUp)
            {
                if (RayHit(GestureManager.orgLoc))
                    Move(Vector3.up);
            }
            else if (eventData == GestureState.SwipeDown)
            {
                if (RayHit(GestureManager.orgLoc))
                    Move(Vector3.down);
            }
        }
    }
    #endregion
}
