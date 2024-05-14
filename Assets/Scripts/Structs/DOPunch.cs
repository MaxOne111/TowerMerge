using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct DOPunch
{
    public Vector3   punch;
    public float     duration;
    public int       vibrato;
    public float     elasticity;

}