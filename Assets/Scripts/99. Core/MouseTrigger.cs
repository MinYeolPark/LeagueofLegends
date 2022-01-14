using UnityEngine;
using UnityEngine.EventSystems;

public class MouseTrigger :EventTrigger /*MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler*/
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
    }

    private void OnMouseExit()
    {
        
    }
}
