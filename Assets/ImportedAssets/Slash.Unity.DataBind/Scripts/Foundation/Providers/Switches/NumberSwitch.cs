namespace Slash.Unity.DataBind.Foundation.Providers.Switches
{
    using System;
    using System.Linq;

    using UnityEngine;

    [Serializable]
    public class NumberSwitchOption : SwitchOption
    {
        #region Fields

        /// <summary>
        ///   Required number to choose this option.
        /// </summary>
        [Tooltip("Required number to choose this option.")]
        public int Number;

        #endregion
    }

    /// <summary>
    ///   Data provider which chooses from specified integers.
    ///   <para>Input: Number.</para>
    ///   <para>Output: Object (Value of chosen range).</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Switches/[DB] Number Switch")]
    public class NumberSwitch : RangeSwitch<NumberRangeOption>
    {
        #region Fields

        /// <summary>
        ///   Options to choose from.
        /// </summary>
        public NumberSwitchOption[] Options;

        #endregion

        #region Methods

        protected override SwitchOption SelectOption(object value)
        {
            int number;
            try
            {
                number = Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return null;
            }

            if (this.Options == null)
            {
                return null;
            }

            // Return first valid range.
            return this.Options.FirstOrDefault(option => option.Number == number);
        }

        #endregion
    }
}