﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RamjetAnvil.Coroutine {

    public class ObjectPool<T> where T : IResetable {

        private readonly int _growthStep;
        private readonly Func<T> _factory;
        private readonly Stack<T> _pool;

        public ObjectPool(Func<T> factory, int growthStep) {
            _growthStep = growthStep;
            _factory = factory;
            _pool = new Stack<T>(growthStep);
            GrowPool();
        }

        public T Take() {
            if (_pool.Count == 0) {
                GrowPool();
            }
            return _pool.Pop();
        }

        public void Return(T o) {
            o.Reset();
            _pool.Push(o);
        }

        private void GrowPool() {
            for (int i = 0; i < _growthStep; i++) {
                var o = _factory();
                o.Reset();
                _pool.Push(o);
            }
        }
    }
}
