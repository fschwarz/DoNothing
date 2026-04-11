using UnityEngine;

namespace _PROJECT.Scripts
{
    public class AnimatorPlaybackControl : PlaybackControl
    {
        public Animation animator;
        public void Awake()
        {
            animator = GetComponent<Animation>();
        }
        public override void Play()
        {
            base.Play();
            animator.Play();
        }
        public override void Pause()
        {
            base.Pause();
            animator.Stop();
        }
    }
}