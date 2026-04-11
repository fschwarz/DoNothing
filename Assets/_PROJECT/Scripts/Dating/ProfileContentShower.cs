using UnityEngine;

namespace _PROJECT.Scripts.Dating
{
    public class ProfileContentShower : MonoBehaviour
    {
        public SpriteRenderer profileIcon;
        
        public void InitProfile(ProfileContent queuedContent)
        {
            profileIcon.sprite = queuedContent.picture;
        }
    }
}