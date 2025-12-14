using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterRush/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterDefinition> characters;
}
