using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public GameObject blockPrefab;

    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    //keep all the position-in-beats of notes in the song
    public float[] track;

    public Transform[] spawnPoints;
    /*public float timeBetweenWaves = 1f;

    private float timeToSpawn = 2f;*/

    // The start positionY of notes.
    public float startLineY;

    // The positionY of music notes.
    public float posY;

    // How many beats are contained on the screen. (Imagine this as "how many beats per bar" on music sheets.)
    public float BeatsShownOnScreen = 2f;

    //the index of the next note to be spawned
    int nextIndex = 0;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    // Current song position. (We don't want to show this in Editor, hence the "NonSerialized")
    [System.NonSerialized] public float songposition;

    // Next index for the array "track".
    private int indexOfNextNote;

    // Queue, keep references of the MusicNodes which currently on screen.
    private Queue<BlockNote> notesOnScreen;

    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
        notesOnScreen = new Queue<BlockNote>();
        indexOfNextNote = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Check if there are still notes in the track, and check if the next note is within the bounds we intend to show on screen.
        if (nextIndex < track.Length && track[nextIndex] < songPositionInBeats)
        {
            //Instantiate(blockPrefab);

            // Instantiate a new music note. (Search "Object Pooling" for more information if you wish to minimize the delay when instantiating game objects.)
            // We don't care about the position and rotation because we will set them later in MusicNote.Initialize(...).

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (randomIndex != i)
                {
                    //Instantiate(blockPrefab, spawnPoints[i].position, Quaternion.identity);
                    BlockNote blockNote = Instantiate(blockPrefab,spawnPoints[i].position, Quaternion.identity).GetComponent<BlockNote>();
                    

                    // The note is push into the queue for reference.
                    notesOnScreen.Enqueue(blockNote);
                    //initialize the fields of the music note
                    nextIndex++;
                }


            }
   
        }
        /*if (Time.time >= secPerBeat)
        {
            SpawnBlocks();
            secPerBeat = Time.time + BeatsShownOnScreen;
        }*/
    }
    /*void SpawnBlocks()
    {

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform[] transforms = new Transform[track.Length];
     

        for (int i = 0; i < track.Length; i++)
        {
            if (randomIndex != i)
            {
                Instantiate(blockPrefab, spawnPoints[i].position, Quaternion.identity);
            }
        }
    }*/
}
