using System;

namespace Foolish.Stats
{
    public interface ICyclicDisposable : IDisposable
    {
        bool IsDisposed { get; }
        bool NeedCyclicDispose { get; }
    }
}