using Framework.Events;

namespace Framework.Loop
{
    /// <summary>
    /// Represents a loop that fires update events for standard Unity lifecycle hooks.
    /// </summary>
    public interface IRunLoop
    {
        /// <summary>
        /// Event fired during the Update loop. Payload is deltaTime.
        /// </summary>
        public IEventListener<float> onUpdate { get; }

        /// <summary>
        /// Event fired during the FixedUpdate loop. Payload is fixedDeltaTime.
        /// </summary>
        public IEventListener<float> onFixedUpdate { get; }

        /// <summary>
        /// Event fired during the LateUpdate loop. Payload is deltaTime.
        /// </summary>
        public IEventListener<float> onLateUpdate { get; }
    }
}