// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorObject.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Objects
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Provides a plain color object.
    ///   <para>Output: Color.</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Objects/[DB] Color Object")]
    public class ColorObject : DataProvider
    {
        #region Fields

        [Tooltip("Color this provider holds.")]
        public Color Color;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                return this.Color;
            }
        }

        #endregion

        #region Methods

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Color);
        }

        #endregion
    }
}