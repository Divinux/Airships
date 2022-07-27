using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Music : MonoBehaviour {
	public List<Songs> songList = new List<Songs>();
	AudioSource asrc;
  
	public int vCurrent = 0;
  
	public CanvasGroup fadeCanvasGroup;
	public Text textName;
	public float fadeSpeed = 1f;
	[System.Serializable]
	public class Songs 
	{
		public string name = "";
		public AudioClip song;
	}
	
	void Awake () {
		//keep this alive between scene changes
	 DontDestroyOnLoad(gameObject);
	 asrc = GetComponent<AudioSource>();
	 //load volume
	 if(PlayerPrefs.HasKey("MusicVolume"))
	 {
		 asrc.volume = PlayerPrefs.GetFloat("MusicVolume");
	 }
	 //start without delay 
	 StartAudio();
	}
	
	
	void StartAudio()
	{
		//stop music
		asrc.Stop();
		//set music clip
		asrc.clip = songList[vCurrent].song;
		asrc.Play();
		
		textName.text = songList[vCurrent].name;
		//fade song name in and out
		StopAllCoroutines();
		if(asrc.volume > 0f){
		StartCoroutine(FadeIn());
		}
		//set next song number
		 if(vCurrent<songList.Count-1)
		 {
			 vCurrent++;
		 }
		 else
		 {
			 vCurrent = 0;
		 }
		 //wait for song to finish and start next one
		Invoke("StartAudio",asrc.clip.length+0.1f);  
	}
	
	public IEnumerator FadeIn()
     {
         while (fadeCanvasGroup.alpha < 1f)
         {
             fadeCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
 
             yield return null;
         }
		 //Debug.Log("waiting");
		 yield return new WaitForSeconds(3f);
		 //Debug.Log("wfading out");
		 StartCoroutine(FadeOut());
		 
     }
	 public IEnumerator FadeOut()
     {
         while (fadeCanvasGroup.alpha > 0f)
         {
             fadeCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
 
             yield return null;
         }
     }
}
