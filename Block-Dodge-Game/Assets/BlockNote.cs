using UnityEngine;
using System.Collections;

public class BlockNote : MonoBehaviour {
	// Keep a reference of the conductor.
	public SongManager conductor;

	// We keep the start and end positionY to perform interpolation.
	/*public float startY;
	public float endY;	*/
	//public float beat;
	
	public void Initialize(SongManager conductor)
	{
		this.conductor = conductor;
	}
	void Start ()
	{
		GetComponent<Rigidbody2D>().gravityScale += Time.timeSinceLevelLoad / 20f;
	}

	// Update is called once per frame
	void Update () {
		// We update the position of the note according to the position of the song.
		// (Think of this as "resetting" instead of "updating" the position of the note each frame according to the position of the song.)
		// See this image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic3.png (Note that the direction is reversed.)
		//transform.position = new Vector2(startY + (endY - startY) * (1f - (beat - conductor.songposition / conductor.secPerBeat) / conductor.BeatsShownOnScreen), transform.position.x);
		if (transform.position.y < -2f)
		{
			Destroy(gameObject);
		}
	}

}
