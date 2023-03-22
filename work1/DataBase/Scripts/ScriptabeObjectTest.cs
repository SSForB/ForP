using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Param/test")]

public class ScriptabeObjectTest : ScriptableObject
{
    public List<Param> list = new List<Param>();

    [System.SerializableAttribute]
    public class Param
    {
        public string weaponName;
        public string id;
        public int weaponPower;
    }
}

