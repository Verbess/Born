using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    public class InDoorArea : MonoBehaviour {

        [SerializeField] Door door;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                door.isActorStay = true;
            }
        }

        void OnTriggerStay2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                door.isActorStay = true;
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                door.isActorStay = false;
            }
        }
    }
}