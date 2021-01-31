using UnityEngine;

namespace JackUtil {

    public static class AudioExtention {

        public static AudioSource AdjustVolumn(this AudioSource audio, float addition) {

            if (audio.volume + addition > audio.maxDistance) {

                audio.volume = audio.maxDistance;

            } else if (audio.volume + addition < audio.minDistance) {

                audio.volume = 0;

            } else {

                audio.volume += addition;

                audio.volume = Mathf.RoundToInt(audio.volume);

            }

            return audio;
            
        }
    }
}