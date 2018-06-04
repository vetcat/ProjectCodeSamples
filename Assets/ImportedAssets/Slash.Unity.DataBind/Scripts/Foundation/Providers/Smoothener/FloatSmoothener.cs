// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringFormatter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Smootheners
{
    using System;
    using System.Linq;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Formats arguments by a specified format string to create a new string value.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Smootheners/[DB] Float Smoothener")]
    public class FloatSmoothener : DataProvider
    {
        #region Fields

        public float MinStep = 0.01f;

        [Tooltip("If set to smaller as 0, it is ignored. If set to 0 values are instant updated.")]
        public float MaxUpdateTime = -1f;

        public DataBinding Data;

        private float targetValue;
        private float actualValue;
        private float updateIntervalValue;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                return this.actualValue;
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
            if (this.Data.Value is float)
                this.actualValue = this.targetValue = (float)this.Data.Value;
            this.Data.ValueChanged += Data_ValueChanged;
        }

        private void Update()
        {
            if (this.actualValue != this.targetValue)
            {
                if (this.updateIntervalValue > Math.Abs(this.actualValue - this.targetValue))
                    this.updateIntervalValue = Math.Abs(this.actualValue - this.targetValue);

                this.actualValue = this.actualValue < this.targetValue
                        ? this.actualValue + this.updateIntervalValue
                        : this.actualValue - this.updateIntervalValue;
                
                this.OnValueChanged(this.actualValue);
            }
        }

        private void Data_ValueChanged(object newValue)
        {
            if (newValue is float)
            {
                this.targetValue = (float)newValue;

                if (this.MaxUpdateTime == 0)
                {
                    this.actualValue = this.targetValue;
                }
                else if (this.MaxUpdateTime < 0)
                {
                    this.updateIntervalValue = this.MinStep;
                }
                else
                {
                    float updateCirclesInMaxUpdateTime = this.MaxUpdateTime / Time.deltaTime;
                    float difference = Math.Abs(this.actualValue - this.targetValue);

                    this.updateIntervalValue = updateCirclesInMaxUpdateTime * this.MinStep >= difference 
                        ? this.MinStep
                        : difference / updateCirclesInMaxUpdateTime;
                }
            }
        }

        protected override void UpdateValue()
        {
            // TODO(co): Cache current value and check if really changed?
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}