using System;
using TMPro;
using UnityEngine;

namespace _PROJECT.Scripts
{
    public class VideoSwipeContentPlayer : MonoBehaviour
    {
        public VideoSwipeContent Content;
        public SpriteRenderer spriteRenderer;
        public AudioSource audioSource;
        public bool playing = false;
        public float playProgressTime = 0;
        public int frameIndex = 0;
        public event Action<VideoSwipeContent> OnRestart;
        public float audioClipTime = 0;
        public void OnGUI()
        {
            if(GUILayout.Button("Load frame"))
            {
                InitVideoFrame(Content);
            }
            if(GUILayout.Button("Play"))
            {
                Play();
            }
            if(GUILayout.Button("Stop"))
            {
                Stop();
            }
        }

        public void InitVideoFrame(VideoSwipeContent content)
        {
            spriteRenderer.sprite = content.frames[0];
            audioSource.clip = content.audioClip;
            this.Content = content;
            this.Stop();
            this.audioClipTime = 0;
            playProgressTime = 0;
        }

        public void Play()
        {
            playing = true;
            audioSource.Play();
            audioSource.time = audioClipTime;
        }

        public void Stop()
        {
            audioClipTime = audioSource.time;
            audioSource.Stop();
            playing = false;
        }

        public void Update()
        {
            if (playing)
            {
                playProgressTime += Time.deltaTime;
                frameIndex = Mathf.FloorToInt(playProgressTime/Content.secondsPerFrame);
                if (frameIndex >= Content.frames.Count)
                {
                    Restart();
                }
                spriteRenderer.sprite = Content.frames[frameIndex];
            }
        }

        private void Restart()
        {
            frameIndex = 0;
            audioClipTime = 0;
            playProgressTime = 0;
            audioSource.Stop();
            audioSource.Play();
            OnRestart?.Invoke(Content);
        }
    }
}