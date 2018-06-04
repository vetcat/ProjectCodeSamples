// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvertBoolOperation.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Operations
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;
    using UnityEngine.Serialization;

    /// <summary>
    ///   Inverts a boolean data value.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Formatters/[DB] Invert Bool Formatter")]
    public class InvertBoolOperation : DataProvider
    {
        #region Fields

        /// <summary>
        ///   Data value to invert.
        /// </summary>
        [FormerlySerializedAs("Argument")]
        public DataBinding Data;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var argument = this.Data.GetValue<bool>();
                return !argument;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void Awake()
        {
            // Add bindings.
            this.AddBinding(this.Data);
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}