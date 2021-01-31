using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    public class ActorDetector : MonoBehaviour {

        [SerializeField] BlockBase block;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                block.isActorColl = true;
            }
        }

        void OnTriggerStay2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                block.isActorColl = true;
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                block.isActorColl = false;
            }
        }

    }
}