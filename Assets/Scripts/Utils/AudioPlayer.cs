using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RunAndJump {

	public class AudioPlayer : Singleton<AudioPlayer> {
		
		private float _bgmVolume = 0.4f;
		//private Tweener _bgmFader = null;
		private AudioClip _bgmClip = null;
		private AudioSource _bgmSource = null;
		private Queue<AudioSource> availableSources = new Queue<AudioSource>();
		private List<AudioSource> playingSources = new List<AudioSource>();
		private float _sfxVolume = 1;
		
		public bool BgmMute {
			get { return _bgmSource.mute; }
			set { _bgmSource.mute = value; }
		}
		
		private void Awake () {
			gameObject.AddComponent<AudioListener>();
			_bgmSource = gameObject.AddComponent<AudioSource>();
		}
		
		public void PlayBgm(AudioClip bgm , bool loop = true) {
			_bgmSource.loop = loop;
			_bgmSource.volume = _bgmVolume;
			// If this clip is already playing, do nothing.
			if(bgm == _bgmClip) {
				return;
			}
			// If no bgm is playing, then start immediately.
			if(_bgmClip == null) {
				_bgmClip = bgm;
				_bgmSource.clip = bgm;
				_bgmSource.Play();
			} else {
				// Else, queue a cross fade.
				_bgmClip = bgm;
				_bgmClip = bgm;
				_bgmSource.clip = bgm;
				_bgmSource.Play();
			}
		}
		
		public void StopBgm() {
			_bgmSource.Stop();
			_bgmClip = null;
		}

		public void PlaySfx(AudioClip sfx) {
			if( sfx == null ) {
				return;
			}
			AudioSource source = GetAudioSource();
			source.clip = sfx;
			source.volume = _sfxVolume;
			source.loop = false;
			source.Play();
			StartCoroutine(CleanupSfx(source));
		}

		private AudioSource GetAudioSource() {
			if(availableSources.Count > 0) {
				return availableSources.Dequeue();
			}
			return gameObject.AddComponent<AudioSource>();
		}

		private IEnumerator CleanupSfx(AudioSource source) {
			playingSources.Add(source);
			while(source.isPlaying) {
				yield return 0;
			}
			playingSources.Remove(source);
			FreeAudioSource(source);
		}

		private void FreeAudioSource(AudioSource source) {
			source.Stop ();
			source.clip = null;
			availableSources.Enqueue (source);
		}
	}
}