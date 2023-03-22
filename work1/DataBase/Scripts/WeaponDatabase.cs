using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Param/Weapon")]

public class WeaponDatabase : ScriptableObject
{
    public List<Param> list = new List<Param>();

    public Sprite weaponSword;
    public Sprite weaponAxe;
    public Sprite weaponWand;

    [System.SerializableAttribute]
    public class Param
    {
        public string weaponID;
        public string weaponName;
        public int weaponKind; //1:剣　2:斧　3:杖　(暫定)
        public int weaponPower;
        public int weaponLuck;
        public int weaponSearch;
    }
}

