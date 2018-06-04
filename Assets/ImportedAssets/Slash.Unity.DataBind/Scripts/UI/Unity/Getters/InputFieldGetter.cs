// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputFieldGetter.cs" company="Slash Games">
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
    [AddComponentMenu("Data Bind/UnityUI/Getters/[DB] Input Field Value Getter (Unity)")]
    public class InputFieldGetter : ComponentSingleGetter<InputField, string>
    {
        #region Methods

        protected override void AddListener(InputField target)
        {
            target.onValueChanged.AddListener(this.OnTargetValueChanged);
        }

        protected override string GetValue(InputField target)
        {
            return target.text;
        }

        protected override void RemoveListener(InputField target)
        {
            target.onValueChanged.RemoveListener(this.OnTargetValueChanged);
        }

        private void OnTargetValueChanged(string newValue)
        {
            this.OnTargetValueChanged();
        }

        #endregion
    }
}