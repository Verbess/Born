using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackUtil {

    public abstract class AudioManagerBase : MonoBehaviour {

        [SerializeField] AudioSource bgmPlayer;
        Dictionary<int, AudioClip> bgmDic { get; } = new Dictionary<int, AudioClip>();

        [SerializeField] AudioSource actorPlayer;
        Dictionary<int, AudioClip> acotrDic { get; } = new Dictionary<int, AudioClip>();

        [SerializeField] AudioSource mapPlayer;
        Dictionary<int, AudioClip> mapDic { get; } = new Dictionary<int, AudioClip>();

        [SerializeField] AudioSource uiPlayer;
        Dictionary<int, AudioClip> uiDic { get; } = new Dictionary<int, AudioClip>();

        protected virtual void Awake() {
            
        }

        protected void RegisterBGM(int key, AudioClip audio) {
            bgmDic.AddOrReplace(key, audio);
        }

        protected void RegisterActorSound(int key, AudioClip audio) {
            acotrDic.AddOrReplace(key, audio);
        }

        protected void RegisterMapSound(int key, AudioClip audio) {
            mapDic.AddOrReplace(key, audio);
        }

        protected void RegisterUISound(int key, AudioClip audio) {
            uiDic.AddOrReplace(key, audio);
        }

        public void PauseBGM(bool isPause) {
            if (isPause) {
                bgmPlayer.Stop();
            } else {
                bgmPlayer.Play();
            }
        }

        public void PlayBGM(int key) {
            AudioClip clip = bgmDic.GetValue(key);
            if (bgmPlayer.clip != clip) {
                bgmPlayer.clip = clip;
                bgmPlayer.Play();
            }
        }

        public void PlayActorSound(int key) {
            AudioClip clip = acotrDic.GetValue(key);
            if (actorPlayer.isPlaying && actorPlayer.clip == clip) {
                return;
            }
            actorPlayer.clip = clip;
            actorPlayer.Play();
        }

        public void PlayMapSound(int key) {
            AudioClip clip = mapDic.GetValue(key);
            mapPlayer.PlayOneShot(clip);
        }

        public void PlayUISound(int key) {
            AudioClip clip = uiDic.GetValue(key);
            uiPlayer.PlayOneShot(clip);
        }

    }
}