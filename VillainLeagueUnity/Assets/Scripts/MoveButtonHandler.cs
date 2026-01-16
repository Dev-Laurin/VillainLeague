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
        Debug.Log($"[MoveButtonHandler] OnPointerEnter - move:{(move!=null?move.moveName:"<null>")} parentUI:{(parentUI!=null)}");
        if (parentUI != null && move != null)
        {
            parentUI.OnMoveHover(move, resource, secondaryResource);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"[MoveButtonHandler] OnPointerExit - move:{(move!=null?move.moveName:"<null>")} parentUI:{(parentUI!=null)}");
        if (parentUI != null)
        {
            parentUI.OnMoveUnhover();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"[MoveButtonHandler] OnPointerClick - move:{(move!=null?move.moveName:"<null>")} parentUI:{(parentUI!=null)}");
        if (parentUI != null && move != null)
        {
            parentUI.OnMoveClicked(move);
        }
    }
}
