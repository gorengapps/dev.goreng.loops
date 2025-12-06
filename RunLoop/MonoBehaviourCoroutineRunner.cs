using System.Collections;
using UnityEngine;

namespace Framework.Loop
{
    /// <summary>
    /// A MonoBehaviour implementation of ICoroutineRunner on a GameObject.
    /// </summary>
    public class MonoBehaviourCoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        /// <inheritdoc />
        public void Coroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}
