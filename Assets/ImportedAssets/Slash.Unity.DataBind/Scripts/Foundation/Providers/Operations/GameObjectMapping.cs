namespace Slash.Unity.DataBind.Foundation.Providers.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    [Serializable]
    public class StringGameObjectPair
    {
        #region Fields

        public string Key;

        public GameObject Value;

        #endregion
    }

    /// <summary>
    ///   Maps a string key on to a game object.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Operations/[DB] Game Object Mapping")]
    public class GameObjectMapping : DataProvider
    {
        #region Fields

        /// <summary>
        ///   Default value to use if no mapping found.
        /// </summary>
        public GameObject Default;

        /// <summary>
        ///   Key to do mapping with.
        /// </summary>
        [Tooltip("Key to do mapping with.")]
        public DataBinding Key;

        /// <summary>
        ///   Mappings between string keys and game object values.
        /// </summary>
        public List<StringGameObjectPair> Mapping;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var key = this.Key.GetValue<string>();
                var pair = this.Mapping.FirstOrDefault(existingPair => existingPair.Key == key);
                return pair != null ? pair.Value : this.Default;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void Awake()
        {
            this.AddBinding(this.Key);
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}