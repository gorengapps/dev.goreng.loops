using System;
using System.Collections;
using UnityEngine;

namespace Framework.Loop
{
    public interface IRunLoop
    {
        /// <summary>
        /// Wrapper for RepeatInvoke
        /// </summary>
        /// <param name="method"></param>
        /// <param name="interval"></param>
        public Coroutine StartRoutine(Action method, float interval);

        /// <summary>
        /// Starts a coroutine
        /// </summary>
        /// <param name="routine"></param>
        public void CallCoroutine(IEnumerator routine);
        
        /// <summary>
        /// Stops a coroutine
        /// </summary>
        /// <param name="routine"></param>
        public void StopCoroutine(Coroutine routine);
        
        /// <summary>
        /// Subscribe to runloop notifications
        /// </summary>
        /// <param name="callback"></param>
        public void Subscribe(Action<float> callback);
        
        /// <summary>
        /// Subscribe to runloop notifications
        /// </summary>
        /// <param name="callback"></param>
        public void SubscribeFixed(Action<float> callback);
                
        /// <summary>
        /// Subscribe to runloop notifications
        /// </summary>
        /// <param name="callback"></param>
        public void SubscribeLate(Action<float> callback);
        
        /// <summary>
        /// Unsubscribe from runloop notifications
        /// </summary>
        /// <param name="callback"></param>
        public void UnSubscribe(Action<float> callback);

        /// <summary>
        /// Starts a routine
        /// </summary>
        /// <param name="routine"></param>
        public void Coroutine(IEnumerator routine);
        
        /// <summary>
        /// Stops a routine
        /// </summary>
        /// <param name="routine"></param>
        public void StopCoroutine(IEnumerator routine);
    }
}