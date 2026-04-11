using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _PROJECT.Scripts
{
    public class VideoSwipeApp : App
    {
        public RectTransform video;
        public List<VideoSwipeContent> contents = new List<VideoSwipeContent>();
        public VideoSwipeContentPlayer currentContentPlayer,nextContentPlayer;
        public VideoSwipeContent queuedContent;
        public VideoSwipeContent currentContent;
        public bool preloaded = false;
        public float ignoreSwipesCD = 0.01f;
        public TMP_Text likeCountText;
        public TMP_Text commentCountText;
        public TMP_Text titleText;

        private AudioSource _source;
        public virtual VideoSwipeContent GetNextContent()
        {
            return contents[Random.Range(0, contents.Count)];
        }
        
        public override void SwipeFinished(Vector2 swipeInteraction)
        {
            base.SwipeFinished(swipeInteraction);
            Debug.Log("SwipeFinished " + swipeInteraction);
            bool goUp = swipeInteraction.y > 3f;
            if (goUp)
            {
                commentCountText.text = "";
                likeCountText.text = "";
                titleText.text = "";
            }
            video.DOAnchorPosY(goUp ? 1f * video.rect.height : 0f, 0.2f).OnComplete(() =>
                {
                    commentCountText.text = queuedContent.commentCount.ToString();
                    likeCountText.text = queuedContent.likeCount.ToString();
                    titleText.text = queuedContent.title;
                    if (!goUp) return;
                    video.anchoredPosition = new Vector2(video.anchoredPosition.x, 0);
                    currentContentPlayer.InitVideoFrame(queuedContent);
                    currentContentPlayer.Play();
                    Debug.Log("Reset swipe interaction y");
                    preloaded = false;
                    ignoreSwipesCD = 0.01f;
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
            video.anchoredPosition = new Vector2(video.anchoredPosition.x, swipeProgress.y);
            if (preloaded) return;
            Debug.Log("Preloading");
            queuedContent = GetNextContent();
            preloaded = true;
            nextContentPlayer.InitVideoFrame(queuedContent);
        }
    }
}