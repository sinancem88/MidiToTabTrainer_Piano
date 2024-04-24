using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Common;

public class KeyScript : MonoBehaviour
{
    public Dictionary< SevenBitNumber, GameObject> keyMappings { get; set; }
    
    public GameObject TabObjects { get; private set; }


    /// <summary>
    /// Maps every each PianoKey Gameobject with the exact accurate sevenbit number of the midiNote
    /// </summary>
    public void Awake()
    {
        keyMappings = new Dictionary<SevenBitNumber, GameObject>();

        for (int i = 1; i <= 88; i++)
        {
            
            string keyName = "key" + i.ToString();
            TabObjects = GameObject.Find(keyName);

            if (TabObjects != null)
            { 
            SevenBitNumber midiNote = GetMidiNoteForPianoKey(i); 
            keyMappings.Add(midiNote, TabObjects );
            }
  
        }
    }

    /// <summary>
    /// midi saves Piano Keys starting with 21 to 107
    /// </summary>
    /// <param name="keyNumber"></param>
    /// <returns></returns>
    SevenBitNumber GetMidiNoteForPianoKey(int keyNumber)
    {
        
        int midiNote = keyNumber + 20; 
        return (SevenBitNumber)midiNote;
    }


}
