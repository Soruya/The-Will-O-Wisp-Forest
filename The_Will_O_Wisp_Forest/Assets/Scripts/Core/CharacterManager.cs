using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Responsible for adding and maintaining characters in scene
public class CharacterManager : MonoBehaviour {

    public static CharacterManager instance;

    // Reference to Character Panel
    // All characters must be attached to the character panel
    public RectTransform characterPanel;

    // List of characters currently in scene
    public List<Character> characters = new List<Character>();

    // Easy lookup for characters
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();

    void Awake()
    {
        instance = this;    
    }

    public Character GetCharacter(string characterName, bool createCharacterIfDoesNotExist = true, bool enableCreateCharacterOnStart = true)
    {
        int index = -1;
        // Search dictionary for character if already in scene
        if (characterDictionary.TryGetValue(characterName, out index))
        {
            return characters[index];
        }
        else if (createCharacterIfDoesNotExist)
        {
            return CreateCharacter(characterName, enableCreateCharacterOnStart);
        }
        return null;
    }

    public Character CreateCharacter(string characterName, bool enableOnStart = true)
    {
        Character newCharacter = new Character(characterName, enableOnStart);

        characterDictionary.Add(characterName, characters.Count);
        characters.Add(newCharacter);

        return newCharacter;
    }
}
