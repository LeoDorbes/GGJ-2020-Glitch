using FMOD.Studio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EventInstance _musicManager;
    void Start()
    {
        _musicManager = Utils.Sound.CreateSoundInstance("event:/Music/Music_Generative");
        _musicManager.start();
    }
}
