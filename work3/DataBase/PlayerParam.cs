using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MyScriptable/PlayerParam")]

public class PlayerParam : ScriptableObject
{
    public List<PlayerDB> PlayerDBList = new List<PlayerDB>();

    [System.Serializable]
    public class PlayerDB
    {
        public string playerName;
        public Sprite playerImage;
        public float playerRange;
        public float playerExtentRad; //”ÍˆÍUŒ‚”¼Œa
        public float playerExtentKind; //”ÍˆÍUŒ‚í—Ş
        public float playerAttackSpeed;
        public float playerAttackDamage;
    }
}
