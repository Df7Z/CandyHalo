﻿using System;
using System.Threading;

namespace MonoSystemCache
{
    public static class GetInfo<T>
    {
        public static Type Type { get; }
        public static int Index { get; }
        
        static GetInfo()
        {
            Type = typeof(T);
            Index = Interlocked.Increment(ref IndexIncrement.Index);
        }
    }
}