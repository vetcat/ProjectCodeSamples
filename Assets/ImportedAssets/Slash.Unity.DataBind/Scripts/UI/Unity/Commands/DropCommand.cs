namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Foundation.Commands;

    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    ///   Command which is invoked when an object is dropped on to this one.
    ///   Parameters:
    ///   - Context of dropped object if existing.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] Drop Command (Unity)")]
    public class DropCommand : Command, IDropHandler
    {
        #region Public Methods and Operators

        public void OnDrop(PointerEventData eventData)
        {
            var dragContextHolder = eventData.pointerDrag != null
                ? eventData.pointerDrag.GetComponent<ContextHolder>()
                : null;
            object dragContext = null;
            if (dragContextHolder != null)
            {
                dragContext = dragContextHolder.Context;
            }
            this.InvokeCommand(dragContext);
        }

        #endregion
    }
}