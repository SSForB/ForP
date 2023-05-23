using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MyScriptable/MonsterParam")]
public class MonsterParam : ScriptableObject
{

    public List<MonsterDB> monsterDBList = new List<MonsterDB>();

    [System.Serializable]
    public class MonsterDB
    {
        public string monsterName;
        public Sprite monsterImage;
        public float monsterSpeed;
        public float monsterHP;
        public int dropGold;
    }
}
