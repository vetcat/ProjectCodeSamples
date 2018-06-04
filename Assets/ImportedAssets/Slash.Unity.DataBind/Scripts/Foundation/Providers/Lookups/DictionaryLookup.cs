namespace Slash.Unity.DataBind.Foundation.Providers.Lookups
{
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;

    public class DictionaryLookup : DataProvider
    {
        #region Fields

        /// <summary>
        ///   Default value if key wasn't found in dictionary.
        /// </summary>
        public string DefaultValue;

        public DataBinding Dictionary;

        public DataBinding Key;

        private DataDictionary dataDictionary;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                if (this.DataDictionary == null)
                {
                    return string.IsNullOrEmpty(this.DefaultValue) ? null : this.DefaultValue;
                }

                var key = this.Key.GetValue(this.DataDictionary.KeyType);

                object value;
                if (!this.DataDictionary.TryGetValue(key, out value))
                {
                    ReflectionUtils.TryConvertValue(this.DefaultValue, this.DataDictionary.ValueType, out value);
                }

                return value;
            }
        }

        private DataDictionary DataDictionary
        {
            get
            {
                return this.dataDictionary;
            }
            set
            {
                if (value == this.dataDictionary)
                {
                    return;
                }

                if (this.dataDictionary != null)
                {
                    this.dataDictionary.CollectionChanged -= this.OnDataDictionaryChanged;
                }

                this.dataDictionary = value;

                if (this.dataDictionary != null)
                {
                    this.dataDictionary.CollectionChanged += this.OnDataDictionaryChanged;
                }

                this.UpdateValue();
            }
        }

        #endregion

        #region Methods

        protected void Awake()
        {
            this.AddBinding(this.Key);
            this.AddBinding(this.Dictionary);
        }

        protected void OnDestroy()
        {
            this.RemoveBinding(this.Key);
            this.RemoveBinding(this.Dictionary);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            this.Dictionary.ValueChanged -= this.OnDictionaryChanged;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.Dictionary.ValueChanged += this.OnDictionaryChanged;
            this.DataDictionary = this.Dictionary.GetValue<DataDictionary>();
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        private void OnDataDictionaryChanged()
        {
            this.UpdateValue();
        }

        private void OnDictionaryChanged(object newValue)
        {
            this.DataDictionary = this.Dictionary.GetValue<DataDictionary>();
        }

        #endregion
    }
}