namespace Slash.Unity.DataBind.Foundation.Setters
{
    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;

    using UnityEngine;

    /// <summary>
    ///   Base class which adds a game object instantiated from a prefab for the item of an ItemSetter.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Item Setter")]
    public class GameObjectItemSetter : ItemSetter
    {
        #region Fields

        /// <summary>
        ///   Prefab to create the item from.
        /// </summary>
        public GameObject Prefab;

        /// <summary>
        ///   Created item.
        /// </summary>
        private Item item;

        #endregion

        #region Methods

        protected override void ChangeContext(object itemContext)
        {
            if (this.item == null)
            {
                return;
            }

            // Set item data context.
            var itemContextHolder = this.item.GameObject.GetComponent<ContextHolder>();
            if (itemContextHolder == null)
            {
                itemContextHolder = this.item.GameObject.AddComponent<ContextHolder>();
            }
            var path = this.Data.Type == DataBindingType.Context ? this.Data.Path : null;
            itemContextHolder.SetContext(itemContext, path);
        }

        protected override void ClearItem()
        {
            if (this.item == null)
            {
                return;
            }

            Destroy(this.item.GameObject);
            this.item = null;
        }

        protected override void CreateItem(object itemContext)
        {
            var itemGameObject = this.gameObject.AddChild(this.Prefab, false);
            this.item = new Item { GameObject = itemGameObject };
            this.ChangeContext(itemContext);
        }

        #endregion

        private class Item
        {
            #region Properties

            public GameObject GameObject { get; set; }

            #endregion
        }
    }
}