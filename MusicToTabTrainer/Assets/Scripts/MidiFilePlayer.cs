using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using Melanchall.DryWetMidi.Multimedia;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;
using Melanchall.DryWetMidi.MusicTheory;
using System.Text;
using UnityEditor.Experimental.GraphView;

public class MidiFilePlayer 

{
   
    SphareNoteBehaviour noteBehaviour;

    private Playback _playback;

    private OutputDevice _outputDevice;

    private MidiTrackLoader trackloader;

    public MidiFile MidiFile { get; set; } 

    /// <summary>
    /// Name of the output device intendet to used later on in the 
    /// </summary>
    private const string OutputDeviceName = "Microsoft GS Wavetable Synth";

   
    /// <summary>
    /// Ctor to instantiate:
    ///                     1. the SphareNoteBehaviour script for use of Notes AddNoteToQueue and AddDisposeNoteToUnqueue, 
    ///                     2. the trackloader script to access midi files from Folder and add them to List
    ///                     3. the MidiFile.Reader to Read the loaded midifiles from in 2. added filepath
    /// 
    /// </summary>
    /// <param name="sphareNoteBehaviour"></param>
    public MidiFilePlayer(SphareNoteBehaviour sphareNoteBehaviour)
    {
        noteBehaviour = sphareNoteBehaviour;

        trackloader = new MidiTrackLoader();
   
        trackloader.MidiFileAdd();

        MidiFile = MidiFile.Read(trackloader.MidiFiles.FirstOrDefault());
    }


    /// <summary>
    /// Starts midi Playback after Start Button clicked
    /// </summary>
    public void StartMIDIPlayback()
    {
        _playback.Start();
    }

    /// <summary>
    /// Stops midi Playback if Stop Button clicked
    /// </summary>

    public void StopMIDIPlayback()
    {
        _playback.Stop();
    }

    /// <summary>
    /// Repeat midi Playback if Repeat Button clicked
    /// </summary>
    public void RepeatMIDIPlayback()
    {
        _playback.NotesPlaybackStarted -= OnNotesPlaybackStarted;
        _playback.NotesPlaybackFinished -= OnNotesPlaybackFinished;

        _playback.Dispose();
     

        Debug.Log("playback dispose info: " + _playback);

        InitializeFilePlayback();

        _playback.Start();

    }


    /// <summary>
    /// 1. Step Output device gets Initialised
    /// </summary>
    public void InitializeOutputDevice()
    {
        Debug.Log($"Initializing output device [{OutputDeviceName}]...");

        _outputDevice = OutputDevice.GetByName(OutputDeviceName);

        Debug.Log($"Output device [{OutputDeviceName}] initialized.");
    }

    /// <summary>
    /// 2. Step Playback gets Initialized
    /// </summary>
    public void InitializeFilePlayback()
    {
     
       Debug.Log("Initializing playback...");

       _playback = MidiFile.GetPlayback(_outputDevice);
       _playback.Loop = true;
       _playback.NotesPlaybackStarted += OnNotesPlaybackStarted;
       _playback.NotesPlaybackFinished += OnNotesPlaybackFinished;   

       Debug.Log("Playback initialized.");
     }



    /// <summary>
    /// 3.Step Event fired if note PlayPlacback is finished
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void OnNotesPlaybackStarted(object sender,  NotesEventArgs e)
    {

        foreach (var note in e.Notes)
        {
            
            Debug.Log(note.NoteNumber);
            noteBehaviour.AddNoteToQueue(note);
        }

        LogNotes("Notes started:", e);
    }

    /// <summary>
    /// 4.Step Event fired if note PlayPlacback is finished
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnNotesPlaybackFinished(object sender, NotesEventArgs e)
    {
        Debug.Log($"Test: finished: {e.Notes.Count()}");
        foreach (var note in e.Notes)
        {

            Debug.Log(note.NoteNumber);
            noteBehaviour.AddDisposeNoteToQueue(note);
        }



        LogNotes("Notes finished:", e);
    }

    /// <summary>
    /// 5. Step Fires Event for Played and Finished Notes
    /// </summary>
    /// <param name="title"></param>
    /// <param name="e"></param>
    private void LogNotes(string title, NotesEventArgs e)
    {
        var message = new StringBuilder()
            .AppendLine(title)
            .AppendLine(string.Join(Environment.NewLine, e.Notes.Select(n => $"  {n}")))
            .ToString();
        Debug.Log(message.Trim());
    }

    /// <summary>
    /// 6. Step Disposes Playback and Output Device if there is no FileToRead or if Application Quit
    /// </summary>
    public void QuitApplication()
     {
       Debug.Log("Releasing playback and device...");

        if (_playback != null)
         {
           _playback.Dispose();
         }

        if (_outputDevice != null)
           _outputDevice.Dispose();

        Debug.Log("Playback and device released.");
      }

    }
