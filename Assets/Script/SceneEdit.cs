using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEdit : MonoBehaviour
{
    public int num;
    public AudioClip ClipClick;
    public void SceneMove()
    {
        SceneManager.LoadScene(num);
        GetComponent<AudioSource>().PlayOneShot(ClipClick);
    }
    public void GameQuit()
    {
        Application.Quit();
        GetComponent<AudioSource>().PlayOneShot(ClipClick);
    }
    public void NumPrev()
    {
        StoryTypingEffect.bTyping = true;
        StoryTypingEffect.endTyping = true;
        StoryTypingEffect.i = 50;
        StoryTypingEffect.num--;
        GetComponent<AudioSource>().PlayOneShot(ClipClick);
    }
    public void NumNext()
    {
        StoryTypingEffect.bTyping = true;
        if (StoryTypingEffect.endTyping)
        {
            StoryTypingEffect.num++;
            StoryTypingEffect.endTyping = false;
        }
        else StoryTypingEffect.endTyping = true;
        GetComponent<AudioSource>().PlayOneShot(ClipClick);
    }
    public void sceneMoveTyping()
    {
        StoryTypingEffect.bTyping = true;
        if (StoryTypingEffect.endTyping)
        {
            SceneManager.LoadScene(num);
        }
        else StoryTypingEffect.endTyping = true;
        GetComponent<AudioSource>().PlayOneShot(ClipClick);
    }
}
