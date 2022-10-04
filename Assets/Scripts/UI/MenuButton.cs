using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float coefScale = 1.1f;

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale *= coefScale;
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale /= coefScale;
    }
}
