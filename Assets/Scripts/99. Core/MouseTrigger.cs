using UnityEngine;
using UnityEngine.EventSystems;

public class MouseTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
    }
}
