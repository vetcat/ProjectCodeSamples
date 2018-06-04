// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BehaviourEnabledSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using UnityEngine;

    /// <summary>
    ///   Sets a behaviour enabled/disabled depending on the boolean data value.
    ///   <para>Input: Boolean</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Behaviour Enabled Setter")]
    public class BehaviourEnabledSetter : ComponentSingleSetter<Behaviour, bool>
    {
        #region Methods

        protected override void OnValueChanged(bool newValue)
        {
            this.Target.enabled = newValue;
        }

        #endregion
    }
}