using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class GeneralClass : ScriptableObject
{
    public enum AttackDirection {
        none,
        top,
        bottom,
        left,
        right,
        center
    }
}
