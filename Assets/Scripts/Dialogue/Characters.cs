using UnityEngine;

public enum CharacterType
{
    Postman,
    Engineer,
    Doctor,
}
public class Characters : MonoBehaviour
{
    public CharacterType characterType;
    public CharacterType GetCharacterType()
    {
        return characterType;
    }
    
}
