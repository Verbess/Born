using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public class UnityLearningDocu : MonoBehaviour {

        [SerializeField]
        GameObject privateInInspector;

        [HideInInspector]
        public GameObject publicHideInInspector;

        [UnityEngine.Serialization.FormerlySerializedAs("hp")]
        public int renameButKeepValue;

        [Range(0, 2f)]
        public float range;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        void RunWithoutBinding() {
            print("Run Before Scene");
        }

        bool GetARandomBool = Random.value > 0.5f;

        bool IsOverDistance(Vector3 pos1, Vector3 pos2, float dis) {
            return (pos1 - pos2).sqrMagnitude < dis * dis;
        }

        bool CompareTagInsteadOfEqual(GameObject gameObject, string targetTag) {
            return gameObject.CompareTag(targetTag);
        }

        void HighLightDebugGo(GameObject go) {
            Debug.Log("Something", go);
        }

        readonly string fire1 = "Fire1";
        void Update() {
            Input.GetAxis(fire1);
        }

    }
}