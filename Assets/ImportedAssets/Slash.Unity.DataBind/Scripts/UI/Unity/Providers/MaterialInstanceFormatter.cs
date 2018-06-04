namespace Slash.Unity.DataBind.UI.Unity.Providers
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Creates a new material instance from the specified material.
    /// </summary>
    [AddComponentMenu("Data Bind/Unity/Formatters/[DB] Material Instance Formatter")]
    public class MaterialInstanceFormatter : DataProvider
    {
        #region Fields

        public DataBinding Material;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var material = this.Material.GetValue<Material>();
                return material != null ? new Material(material) : null;
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
            this.AddBinding(this.Material);
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}