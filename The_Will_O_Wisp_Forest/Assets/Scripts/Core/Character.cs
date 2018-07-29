using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character {

    public string characterName;
    DialogueSystem dialogue;

    // Container for all images related to the character in the scene
    [HideInInspector] public RectTransform root;

    public bool enabled
    {
        get
        {
            return root.gameObject.activeInHierarchy;
        }

        set
        {
            root.gameObject.SetActive(value);
        }
    }

    public Character(string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        // Locate character prefab
        GameObject prefab = Resources.Load("Characters/Prefabs/Character[" + _name + "]") as GameObject;
        // Spawn instance of prefab directonly on the character panel
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform>();
        characterName = _name;

        // Get the renderer(s)
        renderers.renderer = ob.GetComponentInChildren<Image>();
        renderers.renderer = ob.transform.Find("EmotionLayer").GetComponent<Image>();

        dialogue = DialogueSystem.instance;

        enabled = enableOnStart;
    }


    public Sprite GetSprite(string emotion)
    {
        return Resources.Load<Sprite>("Characters/" + characterName + "/" + characterName + "_" + emotion);
    }

    public void SetEmotion(string emotion)
    {
        renderers.renderer.sprite = GetSprite(emotion);
    }

    public void SetEmotion(Sprite sprite)
    {
        renderers.renderer.sprite = sprite;
    }

    public void Say(string speech, bool add = false)
    {
        if (!enabled)
        {
            enabled = true;
        }

        if (!add)
        {
            dialogue.Say(speech, characterName);
        }
        else
        {
            dialogue.SayAdd(speech, characterName);
        }
    }

    [System.Serializable]
    public class Renderers
    {
        // Used only for single layer image
        public Image renderer;
    }

    public Renderers renderers = new Renderers();
}
