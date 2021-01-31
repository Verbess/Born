using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackUtil;

namespace PixelGGJNS {

    public enum ActorSFX {
        FootStepFloor,
        FootStepGrass,
    }

    public enum MapSFX {
        Switcher,
        PickWood,
        PickMatal,
        UseAxe,
        PutDownBridge,
        SlideDownRope,
        PutItem,
        Machinary,
        WoodOnGrass,
        Unlock,
        Locked,
    }

    public enum UISFX {
        ButtonClick,
    }

    public class AudioManager : AudioManagerBase {

        [Header("BGM")]
        [SerializeField] AudioClip introBGM;

        [Header("MapSFX")]
        [SerializeField] AudioClip switcher1;
        [SerializeField] AudioClip pickWood2;
        [SerializeField] AudioClip pickMatal3;
        [SerializeField] AudioClip useAxe4;
        [SerializeField] AudioClip putDownBridge5;
        [SerializeField] AudioClip slideDownRope;
        [SerializeField] AudioClip putItem;
        [SerializeField] AudioClip machinary;
        [SerializeField] AudioClip woodOnGrass;
        [SerializeField] AudioClip unlock;
        [SerializeField] AudioClip locked;

        [Header("UISFX")]
        [SerializeField] AudioClip buttonClick;

        [Header("ActorSFX")]
        [SerializeField] AudioClip footStepGrass;
        [SerializeField] AudioClip footStepFloor;

        protected override void Awake() {

            base.Awake();

            RegisterBGM(0, introBGM);

            RegisterMapSound((int)MapSFX.Switcher, switcher1);
            RegisterMapSound((int)MapSFX.PickWood, pickWood2);
            RegisterMapSound((int)MapSFX.PickMatal, pickMatal3);
            RegisterMapSound((int)MapSFX.UseAxe, useAxe4);
            RegisterMapSound((int)MapSFX.PutDownBridge, putDownBridge5);
            RegisterMapSound((int)MapSFX.SlideDownRope, slideDownRope);
            RegisterMapSound((int)MapSFX.PutItem, putItem);
            RegisterMapSound((int)MapSFX.Machinary, machinary);
            RegisterMapSound((int)MapSFX.WoodOnGrass, woodOnGrass);
            RegisterMapSound((int)MapSFX.Unlock, unlock);
            RegisterMapSound((int)MapSFX.Locked, locked);

            RegisterActorSound((int)ActorSFX.FootStepFloor, footStepFloor);
            RegisterActorSound((int)ActorSFX.FootStepGrass, footStepGrass);

            RegisterUISound((int)UISFX.ButtonClick, buttonClick);

        }

        public void PlayMapSound(MapSFX sfx) {
            PlayMapSound((int)sfx);
        }

        public void PlayActorSound(ActorSFX sfx) {
            PlayActorSound((int)sfx);
        }

        public void PlayUISound(UISFX sfx) {
            PlayUISound((int)sfx);
        }
        
    }

}