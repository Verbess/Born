using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace JackUtil {

    public abstract class FSMBase<T> {

        public bool isRunning;
        public FSMStateBase<T> currentState;
        Dictionary<int, FSMStateBase<T>> stateDic;

        T actor;

        public FSMBase(T actor) {
            this.actor = actor;
            stateDic = new Dictionary<int, FSMStateBase<T>>();
            isRunning = true;
        }

        public void RegisterState(FSMStateBase<T> state) {
            stateDic.Add(state.stateId, state);
        }

        public void EnterState(int stateId) {

            if (currentState != null && currentState.stateId == stateId) {
                // 状态相同
                return;
            }

            FSMStateBase<T> targetState = stateDic.GetValue(stateId);
            if (currentState != null) {
                currentState.Exit(actor);
            }
            currentState = targetState;
            currentState.Enter(actor);

        }

        public void Execute() {

            if (!isRunning) return;

            if (currentState == null) return;

            currentState.Execute(actor);

        }

    }
}