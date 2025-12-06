using System;
using System.Collections.Generic;
using Framework.Events;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Framework.Loop
{
    /// <summary>
    /// A RunLoop implementation that hooks directly into Unity's PlayerLoop.
    /// This allows for update events without an associated MonoBehaviour.
    /// </summary>
    public class PlayerRunLoop : IRunLoop, IDisposable
    {
        private readonly BaseEventProducer<float> _onUpdateProducer = new BaseEventProducer<float>();
        private readonly BaseEventProducer<float> _onLateUpdateProducer = new BaseEventProducer<float>();
        private readonly BaseEventProducer<float> _onFixedUpdateProducer = new BaseEventProducer<float>();

        /// <inheritdoc />
        public IEventListener<float> onUpdate => _onUpdateProducer.listener;
        
        /// <inheritdoc />
        public IEventListener<float> onFixedUpdate => _onFixedUpdateProducer.listener;
        
        /// <inheritdoc />
        public IEventListener<float> onLateUpdate => _onLateUpdateProducer.listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRunLoop"/> class.
        /// This automatically registers the loop callbacks with Unity's PlayerLoop.
        /// </summary>
        public PlayerRunLoop()
        {
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            InjectHook(ref loop, typeof(UnityEngine.PlayerLoop.Update), OnUpdate);
            InjectHook(ref loop, typeof(UnityEngine.PlayerLoop.PreLateUpdate), OnLateUpdate);
            InjectHook(ref loop, typeof(UnityEngine.PlayerLoop.FixedUpdate), OnFixedUpdate);
            PlayerLoop.SetPlayerLoop(loop);
        }

        /// <summary>
        /// Disposes the RunLoop. 
        /// Note: Currently does not unregister from PlayerLoop as that requires complex tree rebuilding.
        /// Ensure lifecycle is managed appropriately.
        /// </summary>
        public void Dispose()
        {
            // Note: Removing hooks from PlayerLoop is complex because we'd need to find our specific delegate
            // For now, simpler to just stop firing events or let it run. 
            // If strictly needed, we would need to rebuild the loop without our hooks.
        }

        private void InjectHook(ref PlayerLoopSystem loop, Type loopType, PlayerLoopSystem.UpdateFunction action)
        {
            if (loop.type == loopType)
            {
                var subSystems = new List<PlayerLoopSystem>(loop.subSystemList);
                subSystems.Add(new PlayerLoopSystem
                {
                    type = typeof(PlayerRunLoop),
                    updateDelegate = action
                });
                loop.subSystemList = subSystems.ToArray();
                return;
            }

            if (loop.subSystemList != null)
            {
                for (int i = 0; i < loop.subSystemList.Length; i++)
                {
                    InjectHook(ref loop.subSystemList[i], loopType, action);
                }
            }
        }

        private void OnUpdate()
        {
            _onUpdateProducer.Publish(this, Time.deltaTime);
        }

        private void OnLateUpdate()
        {
            _onLateUpdateProducer.Publish(this, Time.deltaTime);
        }

        private void OnFixedUpdate()
        {
            _onFixedUpdateProducer.Publish(this, Time.fixedDeltaTime);
        }
    }
}
