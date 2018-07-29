using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTest : MonoBehaviour {

    DialogueSystem dialogue;
    int index = 0;

	// Use this for initialization
	void Start ()
    {
        dialogue = DialogueSystem.instance;	
	}

    public string[] s = new string[]
    {
        "Night had fallen across the land...:",
        "The only light to guide you was the silvery glow of the moon overhead, but even that was unreliable.",
        "With the warmth of the sun gone, doubt was beginning to creep over you.",
        "Maybe this was a bad idea after all...:Elaine"
    };
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
            {
                if (index >= s.Length)
                {
                    return;
                }

                Say(s[index]);
                index++;
            }
        }
	}

    void Say(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        dialogue.Say(speech, speaker);
    }
}
