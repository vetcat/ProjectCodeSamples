// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Property.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Core.Data
{
    /// <summary>
    ///   Data property to monitor if a data value changed.
    /// </summary>
    public class Property
    {
        #region Fields

        /// <summary>
        ///   Current data value.
        /// </summary>
        private object value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructor.
        /// </summary>
        /// <param name="value">Initial value.</param>
        public Property(object value)
        {
            this.Value = value;
        }

        /// <summary>
        ///   Constructor.
        /// </summary>
        public Property()
        {
        }

        #endregion

        #region Delegates

        /// <summary>
        ///   Delegate for ValueChanged event.
        /// </summary>
        /// <param name="oldValue">Old property value.</param>
        public delegate void ValueChangedDelegate(object oldValue);

        #endregion

        #region Events

        /// <summary>
        ///   Called when the value of the property changed.
        /// </summary>
        public event ValueChangedDelegate ValueChanged;

        #endregion

        #region Properties

        /// <summary>
        ///   Current data value.
        /// </summary>
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                var changed = !Equals(this.value, value);
                if (!changed)
                {
                    return;
                }

                var oldValue = this.value;
                this.value = value;

                this.OnValueChanged(oldValue);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Should be called after the data value changed.
        /// </summary>
        /// <param name="oldValue">Old value of property.</param>
        protected void OnValueChanged(object oldValue)
        {
            var handler = this.ValueChanged;
            if (handler != null)
            {
                handler(oldValue);
            }
        }

        #endregion
    }

    /// <summary>
    ///   Generic data property to monitor a data value.
    /// </summary>
    /// <typeparam name="T">Type of data.</typeparam>
    public sealed class Property<T> : Property
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Constructor.
        /// </summary>
        public Property()
        {
        }

        /// <summary>
        ///   Constructor.
        /// </summary>
        /// <param name="value">Initial value.</param>
        public Property(T value)
            : base(value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Current data value.
        /// </summary>
        public new T Value
        {
            get
            {
                var value = base.Value;
                return value != null ? (T)value : default(T);
            }
            set
            {
                base.Value = value;
            }
        }

        #endregion
    }
}