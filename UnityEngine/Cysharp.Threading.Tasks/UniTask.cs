using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
    [AsyncMethodBuilder(typeof(UniTaskMethodBuilder))]
    public struct UniTask
    {
        public void Forget() { }
        public TaskAwaiter GetAwaiter() => throw new NotSupportedException();
    }
    public struct UniTaskMethodBuilder
    {
        public static UniTaskMethodBuilder Create() => default;
        public UniTask Task => default(UniTask);
        public void SetResult() { }
        public void SetException(Exception exception) { }

        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            stateMachine.MoveNext();
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
        }
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
        }
    }

    [AsyncMethodBuilder(typeof(UniTaskMethodBuilder<>))]
    public struct UniTask<T>
    {
        public TaskAwaiter<T> GetAwaiter() => throw new NotSupportedException();
    }
    public struct UniTaskMethodBuilder<T>
    {
        public static UniTaskMethodBuilder Create() => default;
        public UniTask<T> Task => default(UniTask<T>);
        public void SetResult() { }
        public void SetException(Exception exception) { }

        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            stateMachine.MoveNext();
        }


        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
        }
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
        }
    }

    [AsyncMethodBuilder(typeof(UniTaskVoidMethodBuilder))]
    public struct UniTaskVoid
    {
        public TaskAwaiter GetAwaiter() => throw new NotSupportedException();

        public void Forget() { }
    }
    public struct UniTaskVoidMethodBuilder
    {
        public static UniTaskVoidMethodBuilder Create() => default;
        public UniTaskVoid Task => default(UniTaskVoid);
        public void SetResult() { }
        public void SetException(Exception exception) { }

        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            stateMachine.MoveNext();
        }


        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
        }
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
        }
    }
}
