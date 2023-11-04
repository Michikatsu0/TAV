using UnityEngine;


public static class PlayAudioAtPosition
{
    public static void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 100;
        float distance = Vector3.Distance(position, Camera.main.transform.position);
        float adjustedVolume = Mathf.Clamp01(1f - (distance / audioSource.maxDistance)) * volume;

        if (adjustedVolume < 0.3f)
            adjustedVolume = 0.3f;
        audioSource.volume = adjustedVolume;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.Play();
        Object.Destroy((Object)gameObject, clip.length * ((double)Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
    }
}