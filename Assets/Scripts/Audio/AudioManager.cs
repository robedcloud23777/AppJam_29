using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioManager() => Instance = this;
    Pooler<AudioSource> pooler;
    List<AudioSource> playing = new();
    private void Awake()
    {
        pooler = new(() => new GameObject("AudioSource").AddComponent<AudioSource>(), 10000, 0);
    }
    public void PlaySound(Sound sound)
    {
        AudioSource tmp = pooler.GetObject();
        tmp.clip = sound.clip;
        tmp.volume = sound.volume;
        tmp.Play();
        playing.Add(tmp);
    }
    List<AudioSource> removeQueue = new();
    private void Update()
    {
        foreach(var i in playing)
        {
            if (!i.isPlaying) removeQueue.Add(i);
        }
        foreach(var i in removeQueue)
        {
            playing.Remove(i);
            pooler.ReleaseObject(i);
        } removeQueue.Clear();
    }
}
[System.Serializable]
public struct Sound
{
    public AudioClip clip;
    public float volume;
}