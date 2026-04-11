using System;
using System.Collections.Generic;
using _PROJECT.Scripts.Dating;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _PROJECT.Scripts
{
    public class ImageSwipeApp : App
    {
        public RectTransform video;
        public List<ProfileContent> contents = new List<ProfileContent>();
        public ProfileContentShower currentContentPlayer,nextContent;
        public ProfileContent queuedContent;
        public bool preloaded = false;
        public float ignoreSwipesCD = 0.01f;
        public TMP_Text titleText;

        public virtual ProfileContent GetNextContent()
        {
            return contents[Random.Range(0, contents.Count)];
        }
        
        public override void SwipeFinished(Vector2 swipeInteraction)
        {
            base.SwipeFinished(swipeInteraction);
            Debug.Log("SwipeFinished " + swipeInteraction);
            bool go = Mathf.Abs(swipeInteraction.x) > 0.2f;
            bool left = go && swipeInteraction.x < 0f;
            bool right = go && swipeInteraction.x > 0f;
            bool bugged = Random.value < Difficulty.bugFrequency;
            if (Random.value < Difficulty.bugFrequency)
            {
                left = !left;
                right = !right;
            }
            if (Random.value < Difficulty.bugFrequency)
            {
                go = !go;
            }

            float sign = 0;
            if (left)
            {
                sign = -1;
            }
            else if (right)
            {
                sign = 1;
            }
            video.DOAnchorPosX(sign * (video.rect.width), 0.2f).OnComplete(() =>
                {
                    if (!go) return;
                    video.anchoredPosition = new Vector2(video.anchoredPosition.x, 0);
                    currentContentPlayer.InitProfile(queuedContent);
                    preloaded = false;
                    ignoreSwipesCD = 0.01f;
                    titleText.text = queuedContent.title;
                }
            );
        }

        public void LateUpdate()
        {
            ignoreSwipesCD-=Time.deltaTime;
        }

        public override void Swiping(Vector2 swipeProgress)
        {
            if (ignoreSwipesCD > 0) return;
            base.Swiping(swipeProgress);
            video.anchoredPosition = new Vector2((swipeProgress.x-0.5f)*video.rect.width, 0);
            if (preloaded) return;
            Debug.Log("Preloading");
            queuedContent = GetNextContent();
            preloaded = true;
            nextContent.InitProfile(queuedContent);
            titleText.text = "";
        }
    }
}