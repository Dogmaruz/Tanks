using UnityEngine;
using System;
# if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "GameSounds", menuName = "Sounds/GameSounds")]
public class GameSounds : ScriptableObject
{
    public AudioClip[] m_Sounds;

    public AudioClip this[Sound sound] => m_Sounds[(int)sound];


#if UNITY_EDITOR
    [CustomEditor(typeof(GameSounds))]
    public class SoundsInspector : Editor
    {
        private static readonly int soundCount = Enum.GetValues(typeof(Sound)).Length;
        private new GameSounds target => base.target as GameSounds;
    }
#endif
}
