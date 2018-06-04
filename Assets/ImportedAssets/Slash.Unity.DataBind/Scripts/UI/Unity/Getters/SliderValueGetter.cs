// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SliderValueGetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Getters
{
    using Slash.Unity.DataBind.Foundation.Providers.Getters;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    ///   Gets the value of an input field.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Getters/[DB] Slider Value Getter (Unity)")]
    public class SliderValueGetter : ComponentSingleGetter<Slider, float>
    {
        #region Methods

        protected override void AddListener(Slider target)
        {
            target.onValueChanged.AddListener(this.OnTargetValueChanged);
        }

        protected override float GetValue(Slider target)
        {
            return target.value;
        }

        protected override void RemoveListener(Slider target)
        {
            target.onValueChanged.RemoveListener(this.OnTargetValueChanged);
        }

        private void OnTargetValueChanged(float newValue)
        {
            this.OnTargetValueChanged();
        }

        #endregion
    }
}