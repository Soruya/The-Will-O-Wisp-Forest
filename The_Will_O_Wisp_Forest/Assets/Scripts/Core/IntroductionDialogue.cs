using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionDialogue : MonoBehaviour {

    DialogueSystem dialogue;
    DialogueParser parser;
    int index = 0;
    string speakerName;

    // Use this for initialization
    void Start()
    {
        dialogue = DialogueSystem.instance;
        parser = DialogueParser.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (parser.lines != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
                {
                    if (index >= parser.lines.Count)
                    {
                        return;
                    }
                    speakerName = parser.lines[index].GetSpeakerName();
                    Say(parser.lines[index].GetSpeech());
                    index++;
                }
            }
        }
    }

    void Say(string s)
    {
        string speech = s;
        string speaker = speakerName;

        dialogue.Say(speech, speaker);
    }
}
