using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Video Swipe Content")]
public class VideoSwipeContent : Content
{
    public List<Sprite> frames;
    public AudioClip audioClip;
    public float secondsPerFrame;
    public int likeCount;
    public int commentCount;
}

public class Content : ScriptableObject
{
    public string title;
    public ContentDescription content;
}

[Flags]
public enum ContentDescription
{
    Cats = 0b1,
    Cheese = 0b10,
    Politics = 0b100,
    HotWomen = 0b1000,
    HotMen = 0b10000,
    HotPeople = 0b11000,
    UnnamedPopularYoutuber = 0b100000,
    Ads = 0b1000000,
    Ragebait = 0b10000000,
    Slop = 0b100000000,
    Informative = 0b1000000000,
    Storytime = 0b10000000000,
    Humour = 0b100000000000,
    // TODO: Add more here!
}
