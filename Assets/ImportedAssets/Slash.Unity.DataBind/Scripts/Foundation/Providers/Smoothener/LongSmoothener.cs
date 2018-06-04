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
    [AddComponentMenu("Data Bind/Foundation/Smootheners/[DB] Long Smoothener")]
    public class LongSmoothener : DataProvider
    {
        #region Fields

        public long MinStep = 1;

        [Tooltip("If set to smaller as 0, it is ignored. If set to 0 values are instant updated.")]
        public float MaxUpdateTime = -1f;

        public DataBinding Data;

        private long targetValue;
        private long actualValue;
        private long updateIntervalValue;

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
            if (this.Data.Value is long)
                this.actualValue = this.targetValue = (long)this.Data.Value;
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
            // TODO: check for different types like int, float
            if (newValue is long)
            {
                this.targetValue = (long)newValue;

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
                    long difference = Math.Abs(this.actualValue - this.targetValue);

                    this.updateIntervalValue = updateCirclesInMaxUpdateTime * this.MinStep >= difference 
                        ? this.MinStep
                        : (long)Mathf.Ceil(difference / updateCirclesInMaxUpdateTime);
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