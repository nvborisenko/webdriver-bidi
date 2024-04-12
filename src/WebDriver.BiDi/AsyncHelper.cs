using System;
using System.Threading.Tasks;
using System.Threading;

namespace OpenQA.Selenium.BiDi
{
    internal class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory.StartNew(() =>
            {
                return func();
            }).Unwrap().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            _myTaskFactory.StartNew(() =>
            {
                return func();
            }).Unwrap().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
