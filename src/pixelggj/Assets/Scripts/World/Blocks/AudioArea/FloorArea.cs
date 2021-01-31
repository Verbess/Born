using UnityEngine;

namespace PixelGGJNS {

    public class FloorArea : MonoBehaviour {

        public bool isActorIn;

        void Awake() {
            isActorIn = false;
        }
        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                Actor actor = other.GetComponent<Actor>();
                actor.inFloor = true;
                isActorIn = true;
            }
        }

        void OnTriggerStay2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                Actor actor = other.GetComponent<Actor>();
                actor.inFloor = true;
                isActorIn = true;
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                Actor actor = other.GetComponent<Actor>();
                actor.inFloor = false;
                isActorIn = false;
            }
        }
    }
}