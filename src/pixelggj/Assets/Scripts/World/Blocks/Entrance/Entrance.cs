using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public class Entrance : MonoBehaviour {

        CameraManager cameraManager => Container.Instance.cameraManager;
        WorldManager world => Container.Instance.world;

        public Entrance target;
        public GameObject pos;

        Sequence action;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == TagCollection.PLAYER) {
                Actor actor = world.actor;
                if (actor.isLock) {
                    return;
                }
                MapGo targetMap = target.transform.parent.GetComponent<MapGo>();
                world.EnterMap(targetMap);
                actor.LockAct(true);
                actor.PlayMoveAni(target.pos.transform.position.x >= actor.transform.position.x);
                action?.Kill();
                action = DOTween.Sequence();
                action.Append(actor.transform.DOMove(target.pos.transform.position, 2f));
                action.AppendCallback(() => {
                    actor.SetTarget(target.pos.transform.position);
                    actor.LockAct(false);
                });
            }
        }

        void OnDestroy() {
            action?.Kill();
        }

    }
}