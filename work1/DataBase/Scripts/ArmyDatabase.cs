using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Param/Army")]

public class ArmyDatabase : ScriptableObject
{
    public List<Param> list = new List<Param>();


    [System.SerializableAttribute]
    public class Param
    {
        public string armyID;
        public string armyName;
        public int armyJob;
        public int armySex; //0:男 1:女 2:不明 ※暫定
        public int armyAge;
        public Sprite armySprite;

        [TextArea(1, 6)]
        public string armyText;
    }
}

