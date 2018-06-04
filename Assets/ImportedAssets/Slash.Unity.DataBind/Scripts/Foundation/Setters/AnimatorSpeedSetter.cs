namespace Slash.Unity.DataBind.Foundation.Setters
{
    using System.Collections;

    using UnityEngine;

    /// <summary>
    ///   Sets the speed of the animator to the float data value.
    ///   <para>Input: Boolean</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Animator Speed Setter")]
    public class AnimatorSpeedSetter : ComponentSingleSetter<Animator, float>
    {
        #region Fields

        /// <summary>
        ///   Coroutine which will set the initial value.
        /// </summary>
        private Coroutine initializerCoroutine;

        #endregion

        #region Methods

        protected override void OnValueChanged(float newValue)
        {
            // Stop previous initializer.
            if (this.initializerCoroutine != null)
            {
                this.StopCoroutine(this.initializerCoroutine);
                this.initializerCoroutine = null;
            }

            if (this.Target.isInitialized)
            {
                this.SetAnimatorParameter(newValue);
            }
            else
            {
                // Delay setting parameter.
                this.initializerCoroutine = this.StartCoroutine((IEnumerator)this.InitializeAnimatorParameter(newValue));
            }
        }

        private void SetAnimatorParameter(float newValue)
        {
            this.Target.speed = newValue;
        }

        private IEnumerator InitializeAnimatorParameter(float value)
        {
            while (!this.Target.isInitialized)
            {
                yield return new WaitForEndOfFrame();
            }

            this.SetAnimatorParameter(value);

            this.initializerCoroutine = null;
        }

        #endregion
    }
}