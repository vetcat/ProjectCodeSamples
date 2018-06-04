// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositionSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using UnityEngine;

    /// <summary>
    ///   Sets the position of a game object depending on a Vector3 data value.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Position Setter")]
    public class PositionSetter : GameObjectSingleSetter<Vector3>
    {
        #region Methods

        protected override void OnValueChanged(Vector3 newValue)
        {
            this.Target.transform.position = newValue;
        }

        #endregion
    }
}