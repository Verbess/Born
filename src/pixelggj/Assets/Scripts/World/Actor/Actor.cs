using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public enum AnimType {
        Idle = 0,
        Climb = 1,
        InRope = 2,
    }

    public class Actor : MonoBehaviour {

        DataManager data => Container.Instance.data;
        WorldManager world => Container.Instance.world;
        AudioManager audioManager => Container.Instance.audioManager;

        [SerializeField] ActorController controller;
        [SerializeField] Rigidbody2D rig;
        [SerializeField] Collider2D coll;
        public SpriteRenderer sr;

        // 寻路
        [SerializeField] AIPath aiPath;
        [SerializeField] AIDestinationSetter setter;
        BlockBase block;
        Vector3 lastPos;
        int sameTimes;
        public bool isStartSearching;
        Sequence timeAction;

        // 动画
        public Animator anim;
        bool isFlip;
        public bool isLock;

        public bool inFloor;

        void Awake() {
            sameTimes = 0;
            isStartSearching = false;
            isLock = false;
            inFloor = false;
            aiPath.enabled = false;
            isFlip = false;
            anim.SetInteger("Dir", 0);
        }

        public void SetTarget(Vector2 pos) {

            world.mouseAnchor.transform.position = pos;

            if (isLock) return;

            aiPath.enabled = true;
            setter.target = world.mouseAnchor.transform;
            timeAction?.Kill();
            timeAction = DOTween.Sequence();
            timeAction.AppendInterval(0.5f);
            timeAction.AppendCallback(() => {
                isStartSearching = true;
            });
        }

        public void SetExchangeBlock(BlockBase block) {
            this.block = block;
        }

        public void LockAct(bool isLock) {
            this.isLock = isLock;
            coll.enabled = !isLock;
            if (isLock) {
                CancelMove();
            }
        }

        public void PlayAni(AnimType action) {
            sr.flipX = false;
            anim.Play(action.ToString());
            anim.SetInteger("Dir", 0);
        }

        public void Execute() {

            controller?.Execute();

            if (isLock) {
                return;
            }

            if (aiPath.enabled == false) {
                anim.SetInteger("Dir", 0);
                sr.flipX = isFlip;
                return;
            }

            if (aiPath.desiredVelocity.x >= 0.01f) {
                PlayMoveAni(true);
            } else if(aiPath.desiredVelocity.x <= 0.01f) {
                PlayMoveAni(false);
            } else {
                anim.SetInteger("Dir", 0);
                sr.flipX = isFlip;
            }

        }

        public void PlayMoveAni(bool isRight) {
            anim.SetInteger("Dir", 1);
            if (isRight) {
                sr.flipX = false;
            } else {
                sr.flipX = true;
            }
            isFlip = sr.flipX;
        }

        public bool IsReachBlock() {
            if (block == null) {
                return false;
            }
            if (block.IsActorBeside(this, 1.44f)) {
                return true;
            } else {
                return false;
            }
        }

        void ReachTarget() {
            timeAction?.Kill();
            CancelMove();
            data.gameData.SetPos(transform.position);
            data.SaveData();
            if (block != null && block.isActorColl) {
                block.Exchange(this);
                block = null;
            }
        }

        void CancelMove() {
            world.mouseAnchor.transform.position = transform.position;
            aiPath.enabled = false;
            isStartSearching = false;
            anim.SetInteger("Dir", 0);
        }

        public void FixedExecute() {

            // Move();

            if (isLock) {
                return;
            }

            if (aiPath.enabled) {
                if (inFloor) {
                    audioManager.PlayActorSound(ActorSFX.FootStepFloor);
                } else {
                    audioManager.PlayActorSound(ActorSFX.FootStepGrass);
                }
                
            } else {
                anim.SetInteger("Dir", 0);
            }

            if (IsReachBlock()) {

                ReachTarget();

            } else {

                if (transform.position == lastPos && isStartSearching && aiPath.enabled) {
                    sameTimes += 1;
                    if (sameTimes > 5) {
                        sameTimes = 0;
                        ReachTarget();
                    }
                } else {
                    sameTimes = 0;
                }

            }

            lastPos = transform.position;

        }

        void Move() {
            rig.MoveAndSlide(controller.moveAxis.x, controller.moveAxis.y);
        }

        void OnDestroy() {
            timeAction?.Kill();
        }
        
    }

}