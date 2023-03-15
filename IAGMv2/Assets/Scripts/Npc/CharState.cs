using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharState : MonoBehaviour
{
    public int fame = 0;
    public enum NPCJOB
    {
        WARRIOR, WIZARD, THIEF, ACHER
    }
    public enum GRADE
    {
        F, E, D, C, B, A
    }

    [Serializable]
    public struct NPC
    {
        //0.등급 F~A
        [SerializeField] Sprite Profile;
        [SerializeField] string ADName; // ADNPC에서 이름이 두번 나옴 없앨건지 다른 두개의 이름을 사용할지 정해야함
        [SerializeField] NPCJOB NpcJob;
        [SerializeField] GRADE CharGrade;
        [SerializeField] int Health;
        [SerializeField] int Attack;
        [SerializeField] int Strong;
        [SerializeField] int Agility;
        [SerializeField] int Intellect;
        [SerializeField] int Dexterity;
        public Sprite profile
        {
            get => Profile;
            set => Profile = value;
        }
        public string name
        {
            get => ADName;
            set => ADName = value;
        }
        public NPCJOB npcJob
        {
            get => NpcJob;
            set => NpcJob = value;
        }
        public GRADE charGrade
        {
            get => CharGrade;
            set => CharGrade = value;
        }
        public int health
        {
            get => Health;
            set => Health = value;
        }
        public int attack
        {
            get => Attack;
            set => Attack = value;
        }
        public int strong
        {
            get => Strong;
            set => Strong = value;
        }
        public int agility
        {
            get => Agility;
            set => Agility = value;
        }
        public int intellect
        {
            get => Intellect;
            set => Intellect = value;
        }
        public int dexterity
        {
            get => Dexterity;
            set => Dexterity = value;
        }
    }

}
