using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public interface IWorldPanel {
        void SetCanvas(Canvas uiCanvas, Camera camera);
    }
}