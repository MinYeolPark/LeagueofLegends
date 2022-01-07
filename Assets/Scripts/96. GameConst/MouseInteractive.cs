using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteractive : MonoBehaviour, IClickable
{
    public string orgLayer = "Champion";
    public string blueLayer = "Blue Outlined";
    public string redLayer = "Red Outlined";

    public Color hoverColor;

    private void Start()
    {
        Transform[] tran = GetComponentsInChildren<Transform>();

        foreach (Transform t in tran)
        {
            t.gameObject.layer = LayerMask.NameToLayer(orgLayer);
        }
    }

    public void OnLeftClick()
    {
        if(LayerMask.GetMask(orgLayer)!=LayerMask.NameToLayer(orgLayer))
        {
            Debug.Log("Left Clicked");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
                Debug.Log("Raycast hit" + raycastHit.point);

                if (raycastHit.collider.TryGetComponent<BaseStats>(out BaseStats obj))
                {                   
                    Debug.Log("clicked Obj" + obj);
                    ObjectStatusPanel objStatus=FindObjectOfType<ObjectStatusPanel>();
                    objStatus.ObjStatusUpdate(obj);
                }
            }
        }
    }

    public void OnPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            if (raycastHit.collider.TryGetComponent<MouseInteractive>(out MouseInteractive units))
            {
                //units.OnMouseEnter();                
            }
        }

    }
    
    public void OnMouseEnter()
    {
        Transform[] tran = GetComponentsInChildren<Transform>();

        foreach (Transform t in tran)
        {
            if(gameObject.TryGetComponent<BaseStats>(out BaseStats obj))
            {
                if (obj.teamID == GameDataSettings.TEAM.RED_TEAM)
                {
                    t.gameObject.layer = LayerMask.NameToLayer(redLayer);
                }
                else if (obj.teamID == GameDataSettings.TEAM.BLUE_TEAM)
                {
                    t.gameObject.layer = LayerMask.NameToLayer(blueLayer);
                }
                else
                {
                    return;
                }
            }                     
        }
    }

    public void OnMouseExit()
    {
        Transform[] tran = GetComponentsInChildren<Transform>();

        foreach (Transform t in tran)
        {
            t.gameObject.layer = LayerMask.NameToLayer(orgLayer);
        }
    }
}
