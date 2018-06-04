namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using Slash.Unity.DataBind.Foundation.Commands;

    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    ///   Command which is invoked when dragging the element ended.
    ///   Parameters:
    ///   - Pointer event data.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] End Drag Command (Unity)")]
    public class EndDragCommand : Command, IEndDragHandler
    {
        #region Public Methods and Operators

        public void OnEndDrag(PointerEventData eventData)
        {
            this.InvokeCommand(eventData);
        }

        #endregion
    }
}