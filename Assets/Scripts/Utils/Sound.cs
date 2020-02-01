using FMOD.Studio;
using UnityEngine;

namespace Utils
{
    public class Sound
    {
        public static void PlaySoundOneShot(string soundPath, Transform location = null)
        {
            if (location == null)
            {
                FMODUnity.RuntimeManager.PlayOneShot(soundPath, location.position);
                return;
            }
            FMODUnity.RuntimeManager.PlayOneShot(soundPath, location.position);
        }

        /// <summary>
        /// Returns a sound or music instance that you have control with. Examples :
        /// EventInstance.start()
        /// EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
        /// </summary>
        /// <param name="soundName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static EventInstance CreateSoundInstance(string soundPath, Transform location = null)
        {
            return FMODUnity.RuntimeManager.CreateInstance(soundPath);
        }
    }
}