// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentSingleSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using System.Diagnostics.CodeAnalysis;

    using UnityEngine;

    /// <summary>
    ///   Base class for a setter for a component.
    /// </summary>
    /// <typeparam name="TComponent">Type of component.</typeparam>
    /// <typeparam name="TData">Type of data to set.</typeparam>
    public abstract class ComponentSingleSetter<TComponent, TData> : SingleSetter<TData>
        where TComponent : Component
    {
        #region Fields

        /// <summary>
        ///   Target component.
        /// </summary>
        public TComponent Target;

        #endregion

        #region Methods

        protected override void Awake()
        {
            base.Awake();

            if (this.Target == null)
            {
                this.Target = this.GetComponent<TComponent>();
            }
        }

        protected override void OnObjectValueChanged(object newValue)
        {
            if (this.Target != null)
            {
                base.OnObjectValueChanged(newValue);
            }
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual void Reset()
        {
            if (this.Target == null)
            {
                this.Target = this.GetComponent<TComponent>();
            }
        }

        #endregion
    }
}