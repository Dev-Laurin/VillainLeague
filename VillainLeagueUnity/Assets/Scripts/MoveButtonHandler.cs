using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Move move;
    public MoveChooserUI parentUI;
    public CharacterResource resource;
    public CharacterResource secondaryResource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (parentUI != null && move != null)
        {
            parentUI.OnMoveHover(move, resource, secondaryResource);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (parentUI != null)
        {
            parentUI.OnMoveUnhover();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (parentUI != null && move != null)
        {
            parentUI.OnMoveClicked(move);
        }
    }
}
