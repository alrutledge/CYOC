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
		private AudioSource m_audioSource;
		//private AudioSource m_audioSource2;

		private void Start()
		{
			m_audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
		}

		private void Awake() 
		{
			MessageSystem.SubscribeMessage<PlayMusicCommand>(MessageSystem.ServiceContext, OnPlayMusicCommand);
			MessageSystem.SubscribeMessage<PlaySoundCommand>(MessageSystem.ServiceContext, OnPlaySoundCommand);
		}

		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayMusicCommand>(MessageSystem.ServiceContext, OnPlayMusicCommand);
			MessageSystem.UnsubscribeMessage<PlaySoundCommand>(MessageSystem.ServiceContext, OnPlaySoundCommand);
		}

		private void OnPlayMusicCommand(PlayMusicCommand command)
		{
			if (m_audioSource.isPlaying)
			{
				m_audioSource.Stop();
			}
			m_audioSource.clip = Resources.Load(command.ClipName, typeof(AudioClip)) as AudioClip;
			m_audioSource.Play();
		}

		private void OnPlaySoundCommand(PlaySoundCommand command)
		{
			m_audioSource.clip = Resources.Load(command.SoundClipName, typeof(AudioClip)) as AudioClip;
			m_audioSource.Play();
		}
	}
}