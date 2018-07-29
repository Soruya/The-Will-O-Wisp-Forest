using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    DialogueSystem dialogue;
    DialogueParser parser;

    CharacterManager charaManager;

    int index = 0;
    string speakerName;
    string emotion;

    // Use this for initialization
    void Start()
    {
        dialogue = DialogueSystem.instance;
        parser = DialogueParser.instance;
        charaManager = CharacterManager.instance;
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
                    emotion = parser.lines[index].GetEmotion();
                    if (speakerName != " ")
                    {
                        Character speaker = charaManager.GetCharacter(speakerName);
                        speaker.SetEmotion(emotion);
                        speaker.Say(parser.lines[index].GetSpeech());
                    }
                    else
                    {
                        Say(parser.lines[index].GetSpeech());
                    }
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
