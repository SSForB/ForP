using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Param/Armor")]

public class ArmorDatabase : ScriptableObject
{
    public List<Param> list = new List<Param>();

    public Sprite armorHead;
    public Sprite armorBody;
    public Sprite armorLeg;

    [System.SerializableAttribute]
    public class Param
    {
        public string armorID;
        public string armorName;
        public int armorKind; 
        public int armorPower;
        public int armorLuck;
        public int armorSearch;
    }
}

