using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryTypingEffect : MonoBehaviour
{
    public string[] txtStory;
    public static int i;
    public static int num;
    public static bool bTyping;
    public static bool endTyping;
    public GameObject btnPrev;
    public GameObject btnNext;
    public GameObject btnScenePrev;
    public GameObject btnSceneNext;
    public AudioClip ClipKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        bTyping = true;
        endTyping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (num <= 0)
        {
            btnPrev.SetActive(false);
            btnScenePrev.SetActive(false);
        }
        else
        {
            btnPrev.SetActive(true);
            btnScenePrev.SetActive(false);
        }
        if (num >= txtStory.Length - 1)
        {
            btnNext.SetActive(false);
            btnSceneNext.SetActive(true);
        }
        else
        {
            btnNext.SetActive(true);
            btnSceneNext.SetActive(false);
        }
        if (bTyping)
        {
            bTyping = false;
            StartCoroutine(_typing(num));
        }
    }
    IEnumerator _typing(int num)
    {
        if (!endTyping)
        {
            GetComponent<AudioSource>().PlayOneShot(ClipKeyboard);
            for (i = 0; i <= txtStory[num].Length; i++)
            {
                GetComponent<TextMeshProUGUI>().text = txtStory[num].Substring(0, i);
                yield return new WaitForSeconds(0.1f);
                if (i == txtStory[num].Length)
                {
                    endTyping = true;
                    GetComponent<AudioSource>().Pause();
                }
                if (endTyping) break;
            }
        }
        else
        {
            GetComponent<AudioSource>().Pause();
            GetComponent<TextMeshProUGUI>().text = txtStory[num];
        }
    }
}
