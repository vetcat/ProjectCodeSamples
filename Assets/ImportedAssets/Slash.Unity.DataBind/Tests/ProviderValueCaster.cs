// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProviderValueCaster.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Tests
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    public class ProviderValueCaster<T> : MonoBehaviour
    {
        #region Fields

        public DataProvider Provider;

        #endregion

        #region Properties

        public T Value
        {
            get
            {
                return (T)this.Provider.Value;
            }
        }

        #endregion
    }
}