﻿namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using System.Collections.Generic;

    using Slash.Unity.DataBind.Foundation.Providers.Formatters;
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    ///   Sets the items of a LayoutGroup depending on the items of the collection data value.
    ///   Creates the items one after another instead of all at once.
    ///   If you don't use any parent references in your item contexts,
    ///   use <see cref="SmoothCollectionChangesFormatter"/> instead.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Smooth Layout Group Items Setter (Unity)")]
    public class SmoothLayoutGroupItemsSetter : GameObjectItemsSetter<LayoutGroup>
    {
        #region Fields

        /// <summary>
        ///   Items to add one by one to the smoothed version of the collection.
        /// </summary>
        private readonly List<QueuedItem> queue = new List<QueuedItem>();

        /// <summary>
        ///   Interval between two items to be added, in seconds.
        /// </summary>
        [Tooltip("Interval between two items to be added, in seconds.")]
        public float Interval;

        /// <summary>
        ///   Time remaining before the next item is added to the smoothed collection, in seconds.
        /// </summary>
        private float timeRemaining;

        #endregion

        #region Methods

        protected override sealed void ClearItems()
        {
            base.ClearItems();

            this.queue.Clear();
        }

        protected override sealed void CreateItem(object itemContext, int itemIndex)
        {
            this.queue.Add(new QueuedItem { ItemContext = itemContext, ItemIndex = itemIndex });
        }

        protected override sealed void RemoveItem(object itemContext)
        {
            base.RemoveItem(itemContext);

            this.queue.RemoveAll(item => item.ItemContext == itemContext);
        }

        private void Update()
        {
            if (this.queue.Count == 0)
            {
                return;
            }

            this.timeRemaining -= Time.deltaTime;

            if (this.timeRemaining > 0)
            {
                return;
            }

            // Add next item to smoothed collection.
            var item = this.queue[0];
            this.queue.RemoveAt(0);

            base.CreateItem(item.ItemContext, item.ItemIndex);

            // Update timer.
            this.timeRemaining += this.Interval;
        }

        #endregion

        private class QueuedItem
        {
            #region Properties

            public object ItemContext { get; set; }

            public int ItemIndex { get; set; }

            #endregion
        }
    }
}