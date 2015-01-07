using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.CYOC.UI
{
	public class AudioManager : MonoBehaviour 
	{
		private AudioSource m_audioSource;

		private void Start()
		{
			m_audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
		}

		private void Awake() 
		{
			MessageSystem.SubscribeMessage<PlayMusicCommand>(MessageSystem.ServiceContext, OnPlayMusicCommand);
		}

		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayMusicCommand>(MessageSystem.ServiceContext, OnPlayMusicCommand);
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
	}
}