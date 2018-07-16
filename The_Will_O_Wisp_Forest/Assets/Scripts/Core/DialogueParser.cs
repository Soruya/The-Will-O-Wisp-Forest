using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DialogueParser : MonoBehaviour {
    public static DialogueParser instance;
    public string file;

    private void Awake()
    {
        instance = this;
    }

    public struct DialogueLine
    {
        public string speakerName;
        public string speech;
        /*public string pose;
        public string position;
        public string[] options;*/

        public DialogueLine(string speakerName, string speech)
        {
            this.speakerName = speakerName;
            this.speech = speech;
        }

        public string GetSpeakerName()
        {
            return this.speakerName;
        }

        public string GetSpeech()
        {
            return this.speech;
        }
    }

    public List<DialogueLine> lines;

	// Use this for initialization
	void Start () {
        instance = this;
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
                    DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1]);
                    lines.Add(lineEntry);
                }
            }
            while (line != null);
            sr.Close();
        }
    }
}
