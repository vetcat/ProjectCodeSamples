namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using Slash.Unity.DataBind.Foundation.Commands;

    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    ///   Command which is invoked when the element is started to be dragged.
    ///   Parameters:
    ///   - Pointer event data.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] Begin Drag Command (Unity)")]
    public class BeginDragCommand : Command, IBeginDragHandler
    {
        #region Public Methods and Operators

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.InvokeCommand(eventData);
        }

        #endregion
    }
}