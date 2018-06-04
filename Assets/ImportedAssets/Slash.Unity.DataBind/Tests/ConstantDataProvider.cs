// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantDataProvider.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Tests
{
    using Slash.Unity.DataBind.Core.Presentation;

    public class ConstantDataProvider<T> : DataProvider
    {
        #region Fields

        public T ConstantValue;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                return this.ConstantValue;
            }
        }

        #endregion

        #region Methods

        protected override void UpdateValue()
        {
        }

        #endregion
    }
}