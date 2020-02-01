using FMOD.Studio;
using UnityEngine;

namespace Utils
{
    public class Sound
    {
        public static void PlaySoundOneShot(string soundName, Transform location = null)
        {
            var eventName = "event:" + soundName;
            if (location == null)
            {
                FMODUnity.RuntimeManager.PlayOneShot(eventName, location.position);
                return;
            }
            FMODUnity.RuntimeManager.PlayOneShot(eventName, location.position);
        }

        /// <summary>
        /// Returns a sound or music instance that you have control with. Examples :
        /// EventInstance.start()
        /// EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
        /// </summary>
        /// <param name="soundName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static EventInstance CreateSoundInstance(string soundName, Transform location = null)
        {
            var eventName = "event:" + soundName;
            return FMODUnity.RuntimeManager.CreateInstance(eventName);
        }
    }
}