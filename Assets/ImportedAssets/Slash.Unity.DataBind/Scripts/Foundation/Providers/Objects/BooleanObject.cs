namespace Slash.Unity.DataBind.Foundation.Providers.Objects
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Provides a plain boolean object.
    ///   <para>Output: Boolean.</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Objects/[DB] Boolean Object")]
    public class BooleanObject : DataProvider
    {
        #region Fields

        [Tooltip("Boolean this provider holds.")]
        public bool Boolean;

        private bool currentBoolean;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                return this.Boolean;
            }
        }

        #endregion

        #region Methods

        protected void Update()
        {
            if (this.Boolean != this.currentBoolean)
            {
                this.UpdateValue();
            }
        }

        protected override void UpdateValue()
        {
            this.currentBoolean = this.Boolean;
            this.OnValueChanged(this.currentBoolean);
        }

        #endregion
    }
}