namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] Event Trigger Command (Unity)")]
    public class EventTriggerCommand : UnityEventCommand<EventTrigger, BaseEventData>
    {
        #region Methods

        protected override UnityEvent<BaseEventData> GetEvent(EventTrigger target)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<UnityEvent<BaseEventData>> GetEvents(EventTrigger target)
        {
            return target.triggers.Select(trigger => trigger.callback as UnityEvent<BaseEventData>);
        }

        protected override void OnEvent(BaseEventData arg0)
        {
            base.OnEvent();
        }

        #endregion
    }
}