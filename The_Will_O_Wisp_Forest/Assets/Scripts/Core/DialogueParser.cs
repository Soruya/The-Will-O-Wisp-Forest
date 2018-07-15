using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DialogueParser : MonoBehaviour {
    public static DialogueParser instance;
    public string file;

    public struct DialogueLine
    {
        public string speakerName;
        public string speech;
        public string pose;
        public string position;
        public string[] options;

        public DialogueLine(string speakerName, string speech, string pose, string position)
        {
            this.speakerName = speakerName;
            this.speech = speech;
            this.pose = pose;
            this.position = position;
            this.options = new string[0];
        }

        public string GetSpeakerName()
        {
            return this.speakerName;
        }

        public string GetSpeech()
        {
            return this.speech;
        }

        public string GetPose()
        {
            return this.pose;
        }

        public string GetPosition()
        {
            return this.position;
        }
    }

    public List<DialogueLine> lines;

	// Use this for initialization
	void Start () {
        file = "Assets/Data/";
        string sceneName = SceneManager.GetActiveScene().name;
        file += sceneName + ".txt";

        lines = new List<DialogueLine>();

        LoadDialogue(file);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadDialogue(string filename)
    {
        string line;
        file = filename;
        StreamReader sr = new StreamReader(file);

        using(sr)
        {
            do
            {
                line = sr.ReadLine();
                if (line != null)
                {
                    string[] lineData = line.Split(':');
                    if (lineData[0] == "Player")
                    {
                        DialogueLine lineEntry = new DialogueLine(lineData[0], "", "", "");
                        lineEntry.options = new string[lineData.Length - 1];

                        for (int i = 1; i < lineData.Length; i++)
                        {
                            lineEntry.options[i - 1] = lineData[i];
                        }
                        lines.Add(lineEntry);
                    }
                    else
                    {
                        DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], lineData[2], lineData[3]);
                        lines.Add(lineEntry);
                    }
                }
            }
            while (line != null);
            sr.Close();
        }
    }
}
