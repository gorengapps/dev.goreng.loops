using System.Collections;
using Framework.Events;
using UnityEngine;

namespace Framework.Loop
{
    public partial class BaseRunLoop: MonoBehaviour
    {
        private readonly BaseEventProducer<float> _onUpdateProducer = new BaseEventProducer<float>();
        private readonly BaseEventProducer<float> _onLateUpdateProducer = new BaseEventProducer<float>();
        private readonly BaseEventProducer<float> _onFixedUpdateProducer = new BaseEventProducer<float>();

        private void Update()
        {
            _onUpdateProducer.Publish(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _onLateUpdateProducer.Publish(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _onFixedUpdateProducer.Publish(Time.fixedDeltaTime);
        }
    }

    public partial class BaseRunLoop : IRunLoop
    {
        public IEventListener<float> onUpdate => _onUpdateProducer.listener;
        public IEventListener<float> onFixedUpdate => _onFixedUpdateProducer.listener;
        public IEventListener<float> onLateUpdate => _onLateUpdateProducer.listener;
        
        public void Coroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}