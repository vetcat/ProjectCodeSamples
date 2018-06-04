using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Context = Slash.Unity.DataBind.Core.Data.Context;

public class SceneUIContext : Context
{
    [Inject] private SignalItemDragBegin _signalItemDragBegin;
    [Inject] private SignalItemDragEnd _signalItemDragEnd;
    [Inject] private SignalItemDrag _signalItemDrag;
    
    public void ItemBeginDrag(PointerEventData eventData)
    {
        _signalItemDragBegin.Fire(eventData);
    }
    
    public void ItemDrag(PointerEventData eventData)
    {
        _signalItemDrag.Fire(eventData);
    }
    
    public void ItemEndDrag(PointerEventData eventData)
    {        
        _signalItemDragEnd.Fire(eventData);
    }
}
