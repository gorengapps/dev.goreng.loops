using System.Collections;
using Framework.Events;

namespace Framework.Loop
{
    public interface IRunLoop
    {
        public IEventListener<float> onUpdate { get; }
        public IEventListener<float> onFixedUpdate { get; }
        public IEventListener<float> onLateUpdate { get; }

        public void Coroutine(IEnumerator routine);
    }
}