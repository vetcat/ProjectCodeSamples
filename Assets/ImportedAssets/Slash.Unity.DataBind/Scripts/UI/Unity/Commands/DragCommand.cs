namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using Slash.Unity.DataBind.Foundation.Commands;

    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    ///   Command which is invoked when the element is dragged.
    ///   Parameters:
    ///   - Pointer event data.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] Drag Command (Unity)")]
    public class DragCommand : Command, IDragHandler
    {
        #region Public Methods and Operators

        public void OnDrag(PointerEventData eventData)
        {
            this.InvokeCommand(eventData);
        }

        #endregion
    }
}