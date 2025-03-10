using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool isSlow(int num)
    {
        return num == 6 || num == 2 || num == 3 || num == 11 || num == 4 || num == 15;
    }

    private void Start()
    {
        SoundManager.THIS = this;
        SoundManager.SoundOn = false;
        SoundManager.MusicOn = false;
        this.musicSource = base.gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < 10; i++)
        {
            this.fast_sources[i] = base.gameObject.AddComponent<AudioSource>();
            this.fast_sources[i].bypassEffects = true;
            this.fast_sources[i].bypassListenerEffects = true;
            this.fast_sources[i].bypassReverbZones = true;
        }
        for (int j = 0; j < 10; j++)
        {
            this.slow_sources[j] = base.gameObject.AddComponent<AudioSource>();
            this.slow_sources[j].bypassEffects = true;
            this.slow_sources[j].bypassListenerEffects = true;
            this.slow_sources[j].bypassReverbZones = true;
        }
        this.musicSource.bypassEffects = true;
        this.musicSource.bypassListenerEffects = true;
        this.musicSource.bypassReverbZones = true;
    }

    public void UpdateMusic()
    {
        if (this.musicPlaying)
        {
            if (!SoundManager.MusicOn)
            {
                this.musicSource.Pause();
                return;
            }
            this.musicSource.UnPause();
        }
    }

    public void PlayMusic()
    {
        if (!this.musicPlaying)
        {
            this.musicSource.clip = this.music;
            this.musicSource.loop = true;
            this.musicSource.volume = 0.1f;
            this.musicSource.Play();
            this.musicSource.Pause();
            this.musicPlaying = true;
        }
    }

    public void PlayBibika()
    {
        if (this.slow_sources[0].isPlaying && this.slow_sources[0].clip == this.sounds[1])
        {
            return;
        }
        this.slow_sources[0].clip = this.sounds[1];
        this.slow_sources[0].volume = 1f;
        this.slow_sources[0].PlayDelayed(UnityEngine.Random.value * 0.05f);
        this.slow_sources[0].playOnAwake = false;
    }

    public void PlaySound(int num, float volume = 1f)
    {
        if (!SoundManager.SoundOn)
        {
            return;
        }
        if (this.lastPlays.ContainsKey(num) && this.lastPlays[num] >= Time.unscaledTime - 0.05f)
        {
            return;
        }
        this.lastPlays[num] = Time.unscaledTime;
        if (this.isSlow(num))
        {
            this.last_slow++;
            this.last_slow %= 10;
            this.slow_sources[this.last_slow].clip = this.sounds[num];
            this.slow_sources[this.last_slow].volume = volume;
            this.slow_sources[this.last_slow].PlayDelayed(UnityEngine.Random.value * 0.05f);
            this.slow_sources[this.last_slow].playOnAwake = false;
            return;
        }
        this.last_fast++;
        this.last_fast %= 10;
        this.fast_sources[this.last_fast].clip = this.sounds[num];
        this.fast_sources[this.last_fast].volume = volume;
        this.fast_sources[this.last_fast].PlayDelayed(UnityEngine.Random.value * 0.05f);
        this.fast_sources[this.last_fast].playOnAwake = false;
    }

    private void Update()
    {
    }

    public const int SOUND_BASKET = 0;

	public const int SOUND_SIGNAL = 1;

	public const int SOUND_BOMB = 2;

	public const int SOUND_BOMBTICK = 3;

	public const int SOUND_DEATH = 4;

	public const int SOUND_DESTROY = 5;

	public const int SOUND_EMI = 6;

	public const int SOUND_GEOLOGY = 7;

	public const int SOUND_HEAL = 8;

	public const int SOUND_HURT = 9;

	public const int SOUND_MINING = 10;

	public const int SOUND_DIZZ = 11;

	public const int SOUND_TP_IN = 12;

	public const int SOUND_TP_OUT = 13;

	public const int SOUND_VOLC = 14;

	public const int SOUND_C190 = 15;

	public static bool SoundOn = true;

	public static bool MusicOn = true;

	public AudioClip[] sounds;

	public AudioClip music;

	private AudioSource[] fast_sources = new AudioSource[10];

	private AudioSource[] slow_sources = new AudioSource[10];

	private Dictionary<int, float> lastPlays = new Dictionary<int, float>();

	private AudioSource musicSource;

	public static SoundManager THIS;

	private bool musicPlaying;

	private int last_fast;

	private int last_slow;
}
