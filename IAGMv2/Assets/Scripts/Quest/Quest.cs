using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quest : MonoBehaviour
{
    public enum QuestGRADE
    {
        F, E, D, C, B, A
    }

    [Serializable]
    public struct QuestInfo
    {
        [SerializeField] string QuestName;
        [SerializeField] QuestGRADE Questgrade; 
        [SerializeField] string Information;
        [SerializeField] Reward Rewards;

        public string questname
        {
            get => QuestName;
            set => QuestName = value;
        }


        public QuestGRADE questgrade
        {
            get => Questgrade;
            set => Questgrade = value;
        }

        public string information
        {
            get => Information;
            set => Information = value;
        }

        public int rewardgold
        {
            get => Rewards.gold;
            set => Rewards.gold = value;
        }
        public int rewardfame
        {
            get => Rewards.fame;
            set => Rewards.fame = value;
        }
    }
    [Serializable]
    public struct Reward
    {
        [SerializeField] int Gold;
        [SerializeField] int Fame;

        public int gold
        {
            get => Gold;
            set => Gold = value;
        }
        public int fame
        {
            get => Fame;
            set => Fame = value;
        }

        public Reward(int gold, int fame)
        {
            Gold = gold;
            Fame = fame;
        }
    }


}
