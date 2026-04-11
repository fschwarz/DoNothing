using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class VideoHelper : EditorWindow
{
    public List<Sprite> sprites = new List<Sprite>();
    public Sprite[] allSprites;
    public List<Sprite> editingListOfSprites = new List<Sprite>();
    public VideoSwipeContent videoSwipeContent;
    public Sprite sprite;
    
    [MenuItem("Tools/VideoHelper")]
    public static void ShowExample()
    {
        VideoHelper wnd = GetWindow<VideoHelper>();
        wnd.titleContent = new GUIContent("VideoHelper");
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Clear Palette"))
        {
            sprites.Clear();
        }
        if (GUILayout.Button("Clear"))
        {
            editingListOfSprites.Clear();
        }

        GUILayout.Label("Add to list:");
        foreach (Sprite sprite in sprites)
        {
            if (GUILayout.Button(sprite.name))
            {
                editingListOfSprites.Add(sprite);
                allSprites = editingListOfSprites.ToArray();
                videoSwipeContent.frames = allSprites.ToList();
            }
        }
        sprite = (Sprite) EditorGUILayout.ObjectField(sprite, typeof(Sprite), false);
        if (GUILayout.Button("Add"))
        {
            sprites.Add(sprite);
            sprite = null;
        }
        foreach (Sprite sprite in editingListOfSprites)
        {
            GUILayout.Label(sprite.name);
        }
        videoSwipeContent = (VideoSwipeContent) EditorGUILayout.ObjectField(videoSwipeContent, typeof(VideoSwipeContent), false);
    }
}