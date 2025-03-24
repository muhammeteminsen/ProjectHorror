using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Create Dialogue")]

public class Dialogue_SO : ScriptableObject
{
    public CharacterType characterType;
    public List<Dialogue> state;
}
[Serializable]
public class Dialogue
{
    [TextArea] public List<string> question;
    [TextArea] public List<string> answer;   
}
