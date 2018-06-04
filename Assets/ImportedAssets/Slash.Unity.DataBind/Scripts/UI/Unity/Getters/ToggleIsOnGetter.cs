// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToggleIsOnGetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Getters
{
    using Slash.Unity.DataBind.Foundation.Providers.Getters;

    using UnityEngine.UI;

    public class ToggleIsOnGetter : ComponentSingleGetter<Toggle, bool>
    {
        #region Methods

        protected override void AddListener(Toggle target)
        {
            target.onValueChanged.AddListener(this.OnToggleValueChanged);
        }

        protected override bool GetValue(Toggle target)
        {
            return target.isOn;
        }

        protected override void RemoveListener(Toggle target)
        {
            target.onValueChanged.RemoveListener(this.OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool newValue)
        {
            this.OnTargetValueChanged();
        }

        #endregion
    }
}