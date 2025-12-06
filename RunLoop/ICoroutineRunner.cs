using System.Collections;

namespace Framework.Loop
{
    /// <summary>
    /// Interface for an object capable of running coroutines (usually via MonoBehaviour).
    /// </summary>
    public interface ICoroutineRunner
    {
        /// <summary>
        /// Starts a coroutine.
        /// </summary>
        /// <param name="routine">The enumerator to run.</param>
        public void Coroutine(IEnumerator routine);

        /// <summary>
        /// Stops a specified coroutine.
        /// </summary>
        /// <param name="routine">The enumerator to stop.</param>
        public void StopCoroutine(IEnumerator routine);
    }
}
