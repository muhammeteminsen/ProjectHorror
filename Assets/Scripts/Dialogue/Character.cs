using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterType characterType;

    private void Awake()
    {
        characterType = GetComponent<CharacterType>();
    }

    public CharacterType GetCharacterType()
    {
        return characterType;
    }
}
