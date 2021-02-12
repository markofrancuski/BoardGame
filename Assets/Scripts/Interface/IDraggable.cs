using UnityEngine.EventSystems;

public interface IDraggable : IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler
{
     void OnCancel();
}
