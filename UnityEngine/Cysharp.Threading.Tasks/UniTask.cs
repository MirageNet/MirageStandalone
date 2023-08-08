using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public static class UniTaskExtensions
    {
        public static UniTask ToUniTask(this AsyncOperation op) => throw new NotSupportedException();


        public static async UniTask<(bool, T)> TimeoutWithoutException<T>(this UniTask<T> task, TimeSpan timeout, DelayType delayType)
        {
            var delayCancellationTokenSource = new CancellationTokenSource();
            var timeoutTask = Delay(timeout, delayCancellationTokenSource.Token);

            int winArgIndex;
            (bool IsCanceled, T Result) taskResult;
            try
            {
                (var leftReturned, taskResult) = await UniTask.WhenAny(task.SuppressCancellationThrow(), timeoutTask);
                winArgIndex = leftReturned ? 0 : 1;
            }
            catch
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
                return (true, default);
            }

            // timeout
            if (winArgIndex == 1)
            {
                return (true, default);
            }
            else
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
            }

            if (taskResult.IsCanceled)
            {
                return (true, default);
            }

            return (false, taskResult.Result);
        }

        internal static async UniTask Delay(TimeSpan timeout, CancellationToken token)
        {
            await Task.Delay(timeout, token);
        }
    }


    public enum DelayType
    {
        UnscaledDeltaTime
    }
}
