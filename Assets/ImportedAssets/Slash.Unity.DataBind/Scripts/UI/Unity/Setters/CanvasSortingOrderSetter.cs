namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;

    /// <summary>
    ///   Sets the sorting order of a canvas.
    ///   <para>Input: Integer</para>
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Canvas Sorting Order Setter (Unity)")]
    public class CanvasSortingOrderSetter : ComponentSingleSetter<Canvas, int>
    {
        #region Methods

        protected override void OnValueChanged(int newValue)
        {
            this.Target.sortingOrder = newValue;
        }

        #endregion
    }
}