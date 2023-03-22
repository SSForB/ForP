using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SubQuest/SubQuest")]

public class SubQuestDatabase : ScriptableObject
{
    public List<Summary> list = new List<Summary>();

    [System.SerializableAttribute]
    public class Summary
    {
        public string SubQuestTitle;

        [TextArea(1, 6)]
        public string SubQuestOverview;
    }
}

