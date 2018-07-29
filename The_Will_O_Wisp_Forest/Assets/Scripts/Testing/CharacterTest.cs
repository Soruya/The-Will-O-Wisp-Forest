using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTest : MonoBehaviour {

    public Character Elaine;

	// Use this for initialization
	void Start () {
        Elaine = CharacterManager.instance.GetCharacter("Elaine");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
