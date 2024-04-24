using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;





public class MidiTrackLoader : EditorWindow
{
    private const string SAVE_SEPERATOR = "**seperator**";

    private string folderPath = "C:/Users/sinan/Music/MidiTracks";

    private string[] midiFilePaths;
    
    private  string allMidiPaths;

    public List<string> MidiFiles { get; set; } = new List<string>();
    public string[] TrackList { get; set; }

 /// <summary>
 /// Adds Midifile from Filepath to List
 /// </summary>

    public void MidiFileAdd()
    {

        var midiFilePaths = Directory.GetFiles(folderPath);
        foreach (string filePath in midiFilePaths)
        {
  
            MidiFiles.Add(filePath);
        }

    }

    //[MenuItem("Example/Overwrite Texture")]
    //static void Apply()
    //{
    //    Texture2D texture = Selection.activeObject as Texture2D;
    //    if (texture == null)
    //    {
    //        EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
    //        return;
    //    }

    //    string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
    //    if (path.Length != 0)
    //    {
    //        var fileContent = File.ReadAllBytes(path);
    //        texture.LoadImage(fileContent);
    //    }
    //}

    
    //void MidiFileShow()
    //{ 

    //    //Read Midi Tracks from FolderPath

    //    foreach (string filePath in midiFilePaths)
    //    {
            
    //        allMidiPaths = string.Join(SAVE_SEPERATOR, filePath);
            
    //    }

    //    TrackList = allMidiPaths.Split(new[] { SAVE_SEPERATOR }, System.StringSplitOptions.None);        

    //}
   
}
