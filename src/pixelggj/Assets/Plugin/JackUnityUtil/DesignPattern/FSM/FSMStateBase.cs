using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace JackUtil {

    public abstract class FSMStateBase<T> {

        public abstract int stateId { get; }
        public abstract void Enter(T actor);
        public abstract void Execute(T actor);
        public abstract void Exit(T actor);

    }
}