using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JackUtil;

namespace PixelGGJNS {

    public enum BlockType : byte {
        Axe,
        Anchor,
        GeneralSwitch,
        TentSwitch,
        DrawBridgeController,
        DrawBridge,
        PlankBridge,
        Door,
        RIP,
        Swing,
        SwingRope,
        SwingHeap,
        RopePillar,
        Stair,
        Chest,
        Box,
        BoxHook,
        Cave,
        Chain,
        WoodBoard,
        Step,
        Stele,
    }

    public static class BlockTypeExtention {
        public static int ToInt(this BlockType block) {
            return (int)block;
        }
    }

    public abstract class BlockBase : MonoBehaviour {

        protected CameraManager cameraManager => Container.Instance.cameraManager;
        protected WorldManager world => Container.Instance.world;
        protected DataManager data => Container.Instance.data;
        protected UIManager ui => Container.Instance.ui;
        protected AudioManager audioManager => Container.Instance.audioManager;

        [SerializeField] protected SpriteRenderer sr;
        // [SerializeField] protected ActorDetector actorDetector;

        public abstract int id { get; }

        public bool isActorColl;

        protected virtual void Awake() {
            isActorColl = false;
            sr = sr == null ? GetComponent<SpriteRenderer>() : sr;
        }

        protected BlockModel blockModel;

        public virtual InventoryModel GatherInventory() {
            return null;
        }

        public virtual BlockModel GenerateBlockModel() {
            return new BlockModel(id, false, false);
        }

        public void LoadBlockModel(BlockModel model) {
            blockModel = model;
        }

        public abstract void Render();

        public virtual void Exchange(Actor actor) {

        }

        protected virtual void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == TagCollection.PLAYER) {
                isActorColl = true;
            }
        }

        protected virtual void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.tag == TagCollection.PLAYER) {
                isActorColl = true;
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D other) {
            if (other.gameObject.tag == TagCollection.PLAYER) {
                isActorColl = false;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == TagCollection.PLAYER) {
                isActorColl = true;
            }
        }

        protected virtual void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.tag == TagCollection.PLAYER) {
                isActorColl = true;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.tag == TagCollection.PLAYER) {
                isActorColl = false;
            }
        }

        public bool IsActorColl() {
            return isActorColl;
        }

        public bool IsActorBeside(Actor actor, float distance) {
            if (isActorColl) {
                return true;
            }
            return IsBeside(actor.transform.position, distance);
        }

        public bool IsBeside(Vector2 otherPos, float distance) {
            if (sr == null) {
                if (Vector2.Distance(otherPos, transform.position) <= distance) {
                    return true;
                } else {
                    return false;
                }
            } else {
                Vector2 size = sr.size;
                Vector2 originPos = transform.position;
                if (Vector2.Distance(otherPos, originPos) <= distance) {
                    return true;
                }
                if (Vector2.Distance(otherPos, originPos + new Vector2(size.x, 0)) <= distance) {
                    return true;
                }
                if (Vector2.Distance(otherPos, originPos + new Vector2(size.x, size.y)) <= distance) {
                    return true;
                }
                if (Vector2.Distance(otherPos, originPos + new Vector2(0, size.y)) <= distance) {
                    return true;
                }
                return false;
            }
        }

    }
}