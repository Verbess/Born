using System;
using UnityEngine;

namespace JackUtil {

    public class DIContainerBase : MonoBehaviour {

        public virtual T GetInstance<T>(string className) {
            throw new NotImplementedException();
        }

        public virtual T GetInstance<T>(Type type) {
            throw new NotImplementedException();
        }

    }
}