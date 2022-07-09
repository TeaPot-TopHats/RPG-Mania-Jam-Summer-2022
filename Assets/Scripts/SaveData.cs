using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData Current
    {
        get
        {
            if( _current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }// end of get
    }// end of SaveData

    public PlayerProfileTest profile;
    public int Guns;
    public int Catnip;
    
}// end of class
