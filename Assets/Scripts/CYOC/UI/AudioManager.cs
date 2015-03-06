using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using UnityEngine;
using System.Collections;
using Assets.Scripts.ChoiceEngine.EntryActions;

namespace Assets.Scripts.CYOC.UI
{
	public class AudioManager : MonoBehaviour 
	{
		private AudioSource m_soundEffectAudioSource;
		private AudioSource m_musicAudioSource;

		private void Start()
		{
            m_musicAudioSource = GameObject.Find("Music").GetComponent<AudioSource>();
            m_soundEffectAudioSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
		}

		private void Awake() 
		{
			MessageSystem.SubscribeMessage<PlayMusicCommand>(MessageSystem.ServiceContext, OnPlayMusicCommand);
			MessageSystem.SubscribeMessage<PlaySoundCommand>(MessageSystem.ServiceContext, OnPlaySoundCommand);
            MessageSystem.SubscribeMessage<StopSoundEffectCommand>(MessageSystem.ServiceContext, OnStopSoundEffectCommand);
            
		}

		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayMusicCommand>(MessageSystem.ServiceContext, OnPlayMusicCommand);
            MessageSystem.UnsubscribeMessage<PlaySoundCommand>(MessageSystem.ServiceContext, OnPlaySoundCommand);
            MessageSystem.UnsubscribeMessage<StopSoundEffectCommand>(MessageSystem.ServiceContext, OnStopSoundEffectCommand);
		}
        
		private void OnPlayMusicCommand(PlayMusicCommand command)
		{
            if (m_musicAudioSource.isPlaying)
			{
                m_musicAudioSource.Stop();
			}
            m_musicAudioSource.clip = Resources.Load(command.ClipName, typeof(AudioClip)) as AudioClip;
            m_musicAudioSource.Play();
		}

		private void OnPlaySoundCommand(PlaySoundCommand command)
        {
            if (m_soundEffectAudioSource.isPlaying)
            {
                m_soundEffectAudioSource.Stop();
            }
            m_soundEffectAudioSource.clip = Resources.Load(command.SoundClipName, typeof(AudioClip)) as AudioClip;
            m_soundEffectAudioSource.Play();
		}

        private void OnStopSoundEffectCommand(StopSoundEffectCommand message)
        {
            if (m_soundEffectAudioSource.isPlaying)
            {
                m_soundEffectAudioSource.Stop();
            }
        }
	}
}