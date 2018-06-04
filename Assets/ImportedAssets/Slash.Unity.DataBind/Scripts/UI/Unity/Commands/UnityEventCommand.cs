// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityEventCommand.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Slash.Unity.DataBind.Foundation.Commands;

    using UnityEngine;
    using UnityEngine.Events;

    public abstract class UnityEventCommandBase<TBehaviour> : Command
        where TBehaviour : MonoBehaviour
    {
        #region Fields

        /// <summary>
        ///   Target to work with.
        /// </summary>
        public TBehaviour Target;

        #endregion

        #region Methods

        protected override void Awake()
        {
            base.Awake();

            if (this.Target == null)
            {
                this.Target = this.GetComponent<TBehaviour>();
            }
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual void OnDisable()
        {
            if (this.Target == null)
            {
                return;
            }

            this.RemoveListeners(this.Target);
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual void OnEnable()
        {
            if (this.Target == null)
            {
                return;
            }

            this.RegisterListeners(this.Target);
        }

        /// <summary>
        ///   Called when the observed event occured.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual void OnEvent()
        {
            this.InvokeCommand();
        }

        protected abstract void RegisterListeners(TBehaviour target);

        protected abstract void RemoveListeners(TBehaviour target);

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual void Reset()
        {
            if (this.Target == null)
            {
                this.Target = this.GetComponent<TBehaviour>();
            }
        }

        #endregion
    }
    /// <summary>
    ///   Base class for a command which is called on a Unity event.
    /// </summary>
    /// <typeparam name="TBehaviour">Type of mono behaviour to observe for event.</typeparam>
    public abstract class UnityEventCommand<TBehaviour, TEventData1, TEventData2> : UnityEventCommandBase<TBehaviour>
        where TBehaviour : MonoBehaviour
    {
        #region Methods

        /// <summary>
        ///   Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected virtual UnityEvent<TEventData1, TEventData2> GetEvent(TBehaviour target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Returns the events from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Events from the specified target to observe.</returns>
        protected virtual IEnumerable<UnityEvent<TEventData1, TEventData2>> GetEvents(TBehaviour target)
        {
            yield return this.GetEvent(target);
        }

        protected override void RegisterListeners(TBehaviour target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.AddListener(this.OnEvent);
            }
        }

        protected override void RemoveListeners(TBehaviour target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.RemoveListener(this.OnEvent);
            }
        }

        private void OnEvent(TEventData1 arg1, TEventData2 arg2)
        {
            this.InvokeCommand(arg1, arg2);
        }

        #endregion
    }

    /// <summary>
    ///   Base class for a command which is called on a Unity event.
    /// </summary>
    /// <typeparam name="TBehaviour">Type of mono behaviour to observe for event.</typeparam>
    public abstract class UnityEventCommand<TBehaviour, TEventData> : UnityEventCommandBase<TBehaviour>
        where TBehaviour : MonoBehaviour
    {
        #region Methods

        /// <summary>
        ///   Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected virtual UnityEvent<TEventData> GetEvent(TBehaviour target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Returns the events from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Events from the specified target to observe.</returns>
        protected virtual IEnumerable<UnityEvent<TEventData>> GetEvents(TBehaviour target)
        {
            yield return this.GetEvent(target);
        }

        protected override void RegisterListeners(TBehaviour target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.AddListener(this.OnEvent);
            }
        }

        protected override void RemoveListeners(TBehaviour target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.RemoveListener(this.OnEvent);
            }
        }

        protected virtual void OnEvent(TEventData arg0)
        {
            this.InvokeCommand(arg0);
        }

        #endregion
    }

    /// <summary>
    ///   Base class for a command which is called on a Unity event.
    /// </summary>
    /// <typeparam name="TBehaviour">Type of mono behaviour to observe for event.</typeparam>
    public abstract class UnityEventCommand<TBehaviour> : UnityEventCommandBase<TBehaviour>
        where TBehaviour : MonoBehaviour
    {
        #region Methods

        /// <summary>
        ///   Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected abstract UnityEvent GetEvent(TBehaviour target);

        /// <summary>
        ///   Returns the events from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Events from the specified target to observe.</returns>
        protected virtual IEnumerable<UnityEvent> GetEvents(TBehaviour target)
        {
            yield return this.GetEvent(target);
        }

        protected override void RegisterListeners(TBehaviour target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.AddListener(this.OnEvent);
            }
        }

        protected override void RemoveListeners(TBehaviour target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.RemoveListener(this.OnEvent);
            }
        }

        #endregion
    }
}