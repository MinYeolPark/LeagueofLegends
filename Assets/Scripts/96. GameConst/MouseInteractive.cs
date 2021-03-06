using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteractive : MonoBehaviour, IClickable
{
    public string orgLayer = "Champion";
    public string blueLayer = "Blue Outlined";
    public string redLayer = "Red Outlined";
    public string shopLayer = "ItemShop";
    public Color hoverColor;

    private void Start()
    {
        Transform[] tran = GetComponentsInChildren<Transform>();

        foreach (Transform t in tran)
        {
            t.gameObject.layer = LayerMask.NameToLayer(orgLayer);
        }
    }
    
    public void OnMouseEnter()
    {
        Transform[] tran = GetComponentsInChildren<Transform>();

        foreach (Transform t in tran)
        {
            if (gameObject.TryGetComponent<BaseStats>(out BaseStats obj))
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

            /////////////
            //Temporary Shop Process//
            /////////////
            //if (gameObject.TryGetComponent<LayerMask>(out LayerMask layerMask))
            //{
            //    Debug.Log(layerMask);
            //    if(layerMask.ToString()==shopLayer)
            //    {
            //        Debug.Log("???? ????");
            //    }
            //}
        }        
    }

    private void OnMouseDown()
    {
        if (LayerMask.GetMask(orgLayer) != LayerMask.NameToLayer(orgLayer))
        {
            Debug.Log("Left Clicked");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
                if (raycastHit.collider.TryGetComponent<BaseStats>(out BaseStats obj))
                {
                    ObjectStatusPanel objStatus = FindObjectOfType<ObjectStatusPanel>();
                    objStatus.ObjStatusUpdate(obj);
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
