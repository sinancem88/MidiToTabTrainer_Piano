using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class SphareNoteBehaviour : MonoBehaviour
{
    //SoundOutput

   
    MidiFilePlayer filePlayer;

    //GameflowManager _gameflow = null;

    public Queue<SevenBitNumber> noteQueue = new Queue<SevenBitNumber>();

    public Queue<SevenBitNumber> noteDisposeQueue = new Queue<SevenBitNumber>();

    [field: SerializeField]
    public KeyScript keyScript { get; set; }


    private SevenBitNumber noteNumber;

    private SevenBitNumber disposeNoteNumber;

    private SevenBitNumber addNoteBuffer;

    private SevenBitNumber disposeNoteBuffer;




    void Start()
    {
        //if (_gameflow == null) _gameflow = new();

        keyScript = GetComponent<KeyScript>();

        filePlayer = new MidiFilePlayer(this);

        filePlayer.InitializeOutputDevice();

        filePlayer.InitializeFilePlayback();



    }
    public void MidiFilePlaybackRepeatButton()
    {

        filePlayer.RepeatMIDIPlayback();

    }

    public void MidiFilePlaybackStartButton()
    {
        
        filePlayer.StartMIDIPlayback();

    }

    public void MidiFilePlaybackStopButton()
    {
        filePlayer.StopMIDIPlayback();
    }

    void Update()
    {
        while (noteQueue.Count > 0)
        {
            addNoteBuffer = noteQueue.Dequeue();
            Debug.Log("update note loop");
            noteNumber = addNoteBuffer;
            ShowTab(noteNumber);

        }

        while (noteDisposeQueue.Count > 0)
        {
            disposeNoteBuffer = noteDisposeQueue.Dequeue();
            Debug.Log("update disposenote loop");
            disposeNoteNumber = disposeNoteBuffer;
            StartCoroutine(DisposeTab(disposeNoteNumber));
        }

    }



    public void AddNoteToQueue(Note note)
    {

        Debug.Log("note added to queue " + note.NoteNumber);
        noteQueue.Enqueue(note.NoteNumber);



        //_gameflow.UpdateConditional(opertaion:() =>
        //                                      {
        //                                          addNoteBuffer = noteQueue.Dequeue();
        //                                          //Debug.Log("update note loop");
        //                                          noteNumber = addNoteBuffer;
        //                                          ShowTab(noteNumber);
        //                                      },
        //                                      condion:() => noteQueue.Count > 0);

     
    }
    

    public void AddDisposeNoteToQueue(Note note)
    {
        //Debug.Log("note added to dequeue " + note.NoteNumber);
        noteDisposeQueue.Enqueue(note.NoteNumber);


        //_gameflow.UpdateConditional(opertaion: () =>
        //                                     {
        //                                         disposeNoteBuffer = noteDisposeQueue.Dequeue();
        //                                         Debug.Log("update disposenote loop");
        //                                         disposeNoteNumber = disposeNoteBuffer;
        //                                         DisposeTab(disposeNoteNumber);
        //                                     },
        //                                     condion: () => noteDisposeQueue.Count > 0);






      
    }

    private void ShowTab(SevenBitNumber note)
    {

        Debug.Log("inside showtab method " + note);

        /*    if (noteNumber == note)*/
        keyScript.keyMappings[note].gameObject.GetComponent<KeyHighlight>().HighlightKey();

        //else keyScript.keyMappings[note].gameObject.GetComponent<KeyHighlight>().UnhighlightKey();

    }

    private IEnumerator DisposeTab(SevenBitNumber note)
    {
        Debug.Log("inside DisposeTab method " + note);

        /*    if (noteNumber == note)*/
        keyScript.keyMappings[note].gameObject.GetComponent<KeyHighlight>().UnhighlightKey();

        yield return new WaitForSeconds(0.0f);


    }



    private void OnApplicationQuit()
    {
        filePlayer.QuitApplication();
    }
}
