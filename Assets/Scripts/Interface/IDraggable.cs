using UnityEngine.EventSystems;

public interface IDraggable : IPointerDownHandler, IPointerUpHandler
{
     void OnDrag();
     void OnDrop();
     void OnCancel();
}
