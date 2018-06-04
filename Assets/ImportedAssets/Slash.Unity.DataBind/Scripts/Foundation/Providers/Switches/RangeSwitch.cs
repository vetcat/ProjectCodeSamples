namespace Slash.Unity.DataBind.Foundation.Providers.Switches
{
    using Slash.Unity.DataBind.Core.Presentation;

    public abstract class RangeSwitch<T> : DataProvider
        where T : SwitchOption
    {
        #region Fields

        /// <summary>
        ///   Data value to use as switch.
        /// </summary>
        public DataBinding Switch;

        /// <summary>
        ///   Default data value to use if no option is valid.
        /// </summary>
        public DataBinding Default;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var value = this.Switch.Value;
                var option = this.SelectOption(value);
                if (option == null)
                {
                    return this.Default.Value;
                }
                
                // Init if not done.
                if (!option.Value.IsInitialized)
                {
                    option.Value.Init(this.gameObject);
                }

                return option.Value.Value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void Awake()
        {
            this.AddBinding(this.Switch);
            this.AddBinding(this.Default);
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void OnDestroy()
        {
            this.RemoveBinding(this.Switch);
            this.RemoveBinding(this.Default);
        }

        protected abstract SwitchOption SelectOption(object value);

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}