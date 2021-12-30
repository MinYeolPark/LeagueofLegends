using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Inputmanager");
    }
    private void Update()
    {
        //if(IsPointerOverUIObject)     //Compare with tag
        //{
        //    Debug.Log("Clicked Over UI");
        //}
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            if(raycastHit.collider.TryGetComponent<BaseUnits>(out BaseUnits units))
            {
                Debug.Log($"{raycastHit.collider.gameObject.name}");

                units.OnHoverEnter();

                
            }
            else
            {
                units.OnHoverExit();
                return;
            }
        }
    }
}
