using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFxManager : MonoBehaviour
{
    private AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundFX(AudioClip soundFX, float volume = 1, bool randomizePitch = true, float pitchRandom = 0.1f)
    {
        audioSource.PlayOneShot(soundFX, volume);
        // 피치 리셋
        audioSource.pitch = 1;

        if (randomizePitch)
        {
            // +-10%레인지로 랜더마이즈
            audioSource.pitch += Random.Range(-pitchRandom, pitchRandom);
        }
    }

    public void PlayRollSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.Instance.rollSFX);
    }
}
