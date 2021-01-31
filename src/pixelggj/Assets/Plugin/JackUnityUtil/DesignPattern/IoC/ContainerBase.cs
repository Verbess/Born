using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace JackUtil {

    public abstract class ContainerBase {

        Dictionary<string, object> dic;

        public abstract T GetInstance<T>(string className);

    }

}