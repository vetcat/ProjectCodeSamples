// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ButtonClickCommand.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    /// <summary>
    ///   Command which is invoked when a button was clicked.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] Button Click Command (Unity)")]
    public class ButtonClickCommand : UnityEventCommand<Button>
    {
        #region Methods

        protected override UnityEvent GetEvent(Button target)
        {
            return target.onClick;
        }

        #endregion
    }
}