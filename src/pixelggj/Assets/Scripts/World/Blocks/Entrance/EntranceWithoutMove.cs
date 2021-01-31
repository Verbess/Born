using System;
using System.Collections.Generic;
using UnityEngine;
using JackUtil;

namespace PixelGGJNS {

    public class EntranceWithoutMove : MonoBehaviour {

        CameraManager cameraManager => Container.Instance.cameraManager;
        WorldManager world => Container.Instance.world;

        public MapGo target;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                world.EnterMap(target);
            }
        }

    }
}