// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnimatorBooleanSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using UnityEngine;

    /// <summary>
    ///   Sets the animator paramater of a game object to the boolean data value.
    ///   <para>Input: Boolean</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Animator Boolean Setter")]
    public class AnimatorBooleanSetter : AnimatorParameterSetter<bool>
    {
        #region Methods

        protected override void SetAnimatorParameter(bool newValue)
        {
            this.Target.SetBool(this.AnimatorParameterName, newValue);
        }

        #endregion
    }
}