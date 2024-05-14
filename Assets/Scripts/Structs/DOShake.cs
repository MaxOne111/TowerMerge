using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct DOShake
{
    public float     duration;
    public Vector3   strength;
    public int       vibrato;
    public float     randomness;
    public bool      fadeOut;

}