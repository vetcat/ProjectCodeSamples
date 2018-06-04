// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalPositionSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using UnityEngine;

    /// <summary>
    ///   Sets the local position of a game object depending on a Vector3 data value.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Local Position Setter")]
    public class LocalPositionSetter : GameObjectSingleSetter<Vector3>
    {
        #region Methods

        protected override void OnValueChanged(Vector3 newValue)
        {
            this.Target.transform.localPosition = newValue;
        }

        #endregion
    }
}