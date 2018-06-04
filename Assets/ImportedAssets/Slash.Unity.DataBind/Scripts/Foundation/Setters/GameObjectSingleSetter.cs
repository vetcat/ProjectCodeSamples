// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObjectSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using UnityEngine;

    /// <summary>
    ///   Base class for a setter for a game object.
    /// </summary>
    /// <typeparam name="T">Type of data to set.</typeparam>
    public abstract class GameObjectSingleSetter<T> : SingleSetter<T>
    {
        #region Fields

        /// <summary>
        ///   Target object.
        /// </summary>
        public GameObject Target;

        #endregion

        #region Methods

        protected override void Awake()
        {
            base.Awake();

            if (this.Target == null)
            {
                this.Target = this.gameObject;
            }
        }

        #endregion
    }
}