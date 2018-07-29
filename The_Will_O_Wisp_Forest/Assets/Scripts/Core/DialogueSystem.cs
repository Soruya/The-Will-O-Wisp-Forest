using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public ELEMENTS elements;

    void Awake()
    {
        instance = this;    
    }

    // Use this for initialization
    void Start () {
		
	}
	
    // Say something and show on speech box
	public void Say(string speech, string speaker = "")
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech, false, speaker));
    }

    // For additive dialogue
    public void SayAdd(string speech, string speaker = "")
    {
        StopSpeaking();
        speechText.text = targetSpeech;
        StartCoroutine(Speaking(speech, true, speaker));
    }

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
            speaking = null;
        }
    }

    public bool isSpeaking { get { return speaking != null; } }
    // Hidden from Inspector, but still accessible in other scripts
    [HideInInspector] public bool isWaitingForUserInput = false;

    string targetSpeech = "";
    Coroutine speaking = null;
    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        speechPanel.SetActive(true);
        targetSpeech = speech;
        if (!additive)
        {
            speechText.text = "";
        }
        else
        {
            targetSpeech = speechText.text + targetSpeech;
        }
        speakerNameText.text = DetermineSpeaker(speaker);
        isWaitingForUserInput = false;

        while(speechText.text != targetSpeech)
        {
            // Adds one character to end of the speech on every frame
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        // Speech text finished printing and ready to go to next dialogue segment
        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
        {
            yield return new WaitForEndOfFrame();
        }

        // Got user input and no more dialogue needed
        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        // Default return value is current speaker's name
        string retVal = speakerNameText.text;
        if (s != speakerNameText.text && s != "")
        {
            retVal = (s.ToLower().Contains("narrator")) ? "" : s;
        }

        return retVal;
    }

    // Close entire speechPanel and stop all dialogue
    public void Close()
    {
        StopSpeaking();
        speechPanel.SetActive(false);
    }

    [System.Serializable]
    public class ELEMENTS
    {
        // Main panel containing all dialogue related elements in the UI
        public GameObject speechPanel;
        public Text speakerNameText;
        public Text speechText;
    }

    public GameObject speechPanel {
        get
        {
            return elements.speechPanel;
        }
    }

    public Text speakerNameText
    {
        get
        {
            return elements.speakerNameText;
        }
    }

    public Text speechText
    {
        get
        {
            return elements.speechText;
        }
    }
}
