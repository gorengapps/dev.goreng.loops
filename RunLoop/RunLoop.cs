using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Loop
{
    public partial class RunLoop: MonoBehaviour
    {
        private readonly List<Action<float>> _subscribers = new List<Action<float>>();
        private readonly List<Action<float>> _deletions = new List<Action<float>>();
        private readonly List<Action<float>> _lateSubscribers = new List<Action<float>>();
        private readonly List<Action<float>> _fixedSubscribers = new List<Action<float>>();
        
        
        private void Update()
        {
            foreach (var action in _deletions)
            {
                _subscribers.Remove(action);
            }
            
            _deletions.Clear();
            _subscribers.ForEach(x => x?.Invoke(Time.deltaTime));
        }

        private void LateUpdate()
        {
            _lateSubscribers.ForEach(x => x?.Invoke(Time.deltaTime));
        }

        private void FixedUpdate()
        {
            _fixedSubscribers.ForEach(x => x?.Invoke(Time.deltaTime));
        }
    }
    
    public partial class RunLoop: IRunLoop
    {
        public Coroutine StartRoutine(Action method, float interval)
        {
            return StartCoroutine(R());

            IEnumerator R()
            {
                method?.Invoke();
                
                while (true)
                {
                    yield return new WaitForSeconds(interval);
                    method?.Invoke();
                }
            }
        }

        public void CallCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
        
        public void Subscribe(Action<float> callback)
        {
            if (_subscribers.Contains(callback))
            {
                return;
            }
            
            _subscribers.Add(callback);
        }

        public void SubscribeFixed(Action<float> callback)
        {
            _fixedSubscribers.Add(callback);
        }

        public void SubscribeLate(Action<float> callback)
        {
            _lateSubscribers.Add(callback);
        }

        public void UnSubscribe(Action<float> callback)
        {
            _deletions.Add(callback);
        }

        public void Coroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}