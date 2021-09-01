using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameData : ScriptableObject
{
#if UNITY_EDITOR
        [MenuItem("Game/GameData")]
        static void createInstance()
        {
            ScriptableObjectUtility.CreateAsset<GameData>();
        }
#endif

    [Header("General")]
    public TextAsset CSVGeneral;

    [Header("Loading")]
    public Vector2 TimeFadeInOutLoading = new Vector2(0.5f, 0.5f);
    public float TimeWaitingLoading = 0.5f;
}
