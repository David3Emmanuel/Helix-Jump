using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Platform
{
    [Range(1, 11)] public int partCount = 11;
    [Range(0, 11)] public int deathPartCount = 11;
}

[CreateAssetMenu(fileName = "New Stage")]
public class Stage : ScriptableObject
{
    public Color backgroundColor = Color.white;
    public Color platformColor = Color.white;
    public Color playerColor = Color.white;
    public List<Platform> platforms = new List<Platform>();
}
