# Run Loop

A lightweight Unity package that allows non-MonoBehaviour classes to access Unity's update loops.

## Features

- **Decoupled Update Loops**: Access `Update`, `LateUpdate`, and `FixedUpdate` without inheriting from `MonoBehaviour`.
- **Pure C# Implementation**: Use `PlayerRunLoop` to hook directly into Unity's PlayerLoop system.
- **Coroutine Support**: dedicated `ICoroutineRunner` interface for handling coroutines.
- **Event-Driven**: Subscribe to update events using the `Framework.Events` system.

## Installation

Add the package to your `manifest.json` or install via UPM.

## Usage

### 1. The RunLoop Interface (`IRunLoop`)

The core interface provides access to standard Unity update events.

```csharp
public interface IRunLoop
{
    IEventListener<float> onUpdate { get; }
    IEventListener<float> onFixedUpdate { get; }
    IEventListener<float> onLateUpdate { get; }
}
```

### 2. Using `PlayerRunLoop`

`PlayerRunLoop` is a pure C# class that injects itself into Unity's low-level PlayerLoop. It does not require a GameObject.

```csharp
using Framework.Loop;

public class MySystem : IDisposable
{
    private readonly PlayerRunLoop _runLoop;

    public MySystem()
    {
        _runLoop = new PlayerRunLoop();
        _runLoop.onUpdate.Subscribe(OnUpdate);
    }

    private void OnUpdate(float deltaTime)
    {
        // Custom update logic here
    }

    public void Dispose()
    {
        _runLoop.Dispose();
    }
}
```

### 3. Coroutines (`ICoroutineRunner`)

The responsibility for running coroutines has been separated into `ICoroutineRunner`.

```csharp
public class MyService
{
    private readonly ICoroutineRunner _runner;

    public MyService(ICoroutineRunner runner)
    {
        _runner = runner;
    }

    public void StartWork()
    {
        _runner.Coroutine(MyRoutine());
    }

    private IEnumerator MyRoutine()
    {
        yield return null;
    }
}
```

You can use `MonoBehaviourCoroutineRunner` to bridge this capability from a GameObject.

## Architecture Guidelines

- **Prefer `PlayerRunLoop`** for pure logic classes that need update ticks.
- **Use `ICoroutineRunner`** only if you specifically need Unity Coroutines. Consider using `async/await` for modern asynchronous code where possible.
- **Migration**: If you were using `BaseRunLoop`, note that it now implements both `IRunLoop` and `ICoroutineRunner`, but `IRunLoop` itself no longer has `Coroutine()` methods.

## Changelog

### 1.1.0
- **Breaking Change**: `IRunLoop` no longer includes `Coroutine` and `StopCoroutine` methods.
- **New Feature**: Added `ICoroutineRunner` interface.
- **New Feature**: Added `PlayerRunLoop` for non-MonoBehaviour update hooks.
- **Refactor**: Renamed `PlayerLoopRunLoop` to `PlayerRunLoop`.
