using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue_SO : ScriptableObject
{
    public List<Dialogue> dialogues;
    public CharacterType characterType;
}
[Serializable]
public class Dialogue
{
    [TextArea] public List<string> questions;
    [TextArea] public string answer;
    
}