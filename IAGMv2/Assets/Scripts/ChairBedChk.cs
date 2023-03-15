using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairBedChk : MonoBehaviour
{
    [SerializeField] public List<Vector3> _gotable = new List<Vector3>();

    public List<Vector3> gotable
    {
        get { return _gotable; }
        set { _gotable = value; }
    }

    [SerializeField] public List<Vector3> _gobed = new List<Vector3>();

    public List<Vector3> gobed
    {
        get { return _gobed; }
        set { _gobed = value; }
    }

    public enum ChairSlot
    {
        Check, None
    }
    public List<ChairSlot> _chairSlot = new List<ChairSlot>(); //ChairSlot List·Î ¹Ù²Þ

    //public ChairSloat[] _chairsloat = new ChairSloat[] {  ChairSloat.NONE, ChairSloat.NONE };
    public List<ChairSlot> chairSlot
    {
        get { return _chairSlot; }
        set { _chairSlot = value; }
    }

    public List<BedSlot> _bedSlot = new List<BedSlot>(); //BedSlot List·Î ¹Ù²Þ
    public enum BedSlot
    {
        Check, None
    }
    //public BedSloat[] _bedsloat = new BedSloat[] { BedSloat.None };

    public List<BedSlot> bedSlot
    {
        get { return _bedSlot; }
        set { _bedSlot = value; }
    }
}
