namespace Slash.Unity.DataBind.Foundation.Setters
{
    using System.Collections;

    using UnityEngine;

    public abstract class AnimatorParameterSetter<T> : ComponentSingleSetter<Animator, T>
    {
        #region Fields

        /// <summary>
        ///   Name of the animator parameter.
        /// </summary>
        [Tooltip("Name of an animator parameter.")]
        public string AnimatorParameterName;

        /// <summary>
        ///   Coroutine which will set the initial value.
        /// </summary>
        private Coroutine initializerCoroutine;

        #endregion

        #region Methods

        protected override void OnValueChanged(T newValue)
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

        protected abstract void SetAnimatorParameter(T newValue);

        private IEnumerator InitializeAnimatorParameter(T value)
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