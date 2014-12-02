// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;
using System;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Automatically types into a string.")]
	public class StringTypewriter : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The string with the entire message to type out.")]
		public FsmString baseString;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The target result string (can be the same as the base string).")]
		public FsmString resultString;

		[Tooltip("The time between letters appearing.")]
		public FsmFloat pause;

		[Tooltip("When punctuation is encountered then pause is multiplied by this.\n(period, exclamation, question, comma, semicolon, colon and ellipsis).\nIt also handles repeating characters and pauses only one time at their end.")]
		public FsmFloat punctuationMultiplier;

		[Tooltip("True is realtime: continues typing while game is paused. False will subject time variable to the game's timeScale.")]
		public FsmBool realtime;
		
		[Tooltip("Send this event when finished typing.")]
		public FsmEvent finishEvent;



		[UIHint(UIHint.Description)]
		public string d1 = "     Optional Sounds Section:";

		[Tooltip("Check this to play sounds while typing.")]
		public bool useSounds;

		[Tooltip("Check this to not play a sound when it is a spacebar character.")]
		public bool noSoundOnSpaces;

		[ObjectType(typeof(AudioClip))]
		[Tooltip("The sound to play for each letter typed.")]
		public FsmObject typingSound;

		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault audioSourceObj;

		char[] punctuaction = {'.', '!', '?', ',', ';', ':'};
		float p = 0.0f;
		int index = 0;
		int length;
		float startTime;
		float timer = 0.0f;
		string message;
		char lastChar;
		char nextChar;
		private AudioSource audioSource;
		private AudioClip sound;

		public override void Reset()
		{
			// --- Basic --- 
			baseString = null;
			resultString = null;
			pause = 0.05f;
			punctuationMultiplier = 10.0f;
			realtime = false;
			finishEvent = null;

			// --- Sounds --- 
			useSounds = false;
			noSoundOnSpaces = true;
			typingSound = null;
			audioSourceObj = null;
		}

		public override void OnEnter()
		{
			// sort out the sound stuff
			if (useSounds){
				var go = Fsm.GetOwnerDefaultTarget(audioSourceObj);
				if (go != null){
					audioSource = go.GetComponent<AudioSource>();
					if (audioSource == null){
						Debug.LogError ("String Typewriter Action reports: The <color=#ffa500ff>AudioSource component</color> was not found! Does the target object have an Audio Source component?");
						useSounds = false;
					}

					sound = typingSound.Value as AudioClip;
					if (sound == null){
						Debug.LogError ("String Typewriter Action reports: The <color=#ffa500ff>AudioClip</color> was not found!");
						useSounds = false;
					}
				}

				else {
					Debug.LogError ("String Typewriter Action reports: The <color=#ffa500ff>target Game Object</color> for the audio source was not found!");
					useSounds = false;
				}
			}

			message = baseString.Value; // clone the base string.
			length = message.Length; // get the length of the message.
			resultString.Value = ""; // clear the target string.
			startTime = Time.realtimeSinceStartup; // get the actual time since the game started.
		}

		// in update we handle the pausing between letters.
		public override void OnUpdate()
		{
			p = pause.Value; // clone the pause variable in OnUpdate in case it is changed by the user at runtime.

			nextChar = message[index];
			int _iLast = Array.IndexOf (punctuaction, lastChar); // get last index
			int _iNext = Array.IndexOf (punctuaction, nextChar); // get next index
			bool _lastIsMark = _iLast != -1; // if index is not -1, there is a mark.
			bool _nextIsMark = _iNext != -1; // if index is not -1, there is a mark.

			if (_lastIsMark)
			{
				// if the next char is a p.mark, we should not pause.
				if (!_nextIsMark)
				{
					pause = (p * punctuationMultiplier.Value);
				}
			}

			if (realtime.Value)
			{	
				// check the current time minus the previous Typing event time.
				// if that's more than the pause gap, then its time for another character.
				if (Time.realtimeSinceStartup - startTime >= pause.Value)
				{
					DoTyping();
				}
			}
		
			if (!realtime.Value)
			{
				// add delta time until its more than the pause gap.
				timer += Time.deltaTime;
				if (timer >= pause.Value)
				{
					DoTyping();
				}
			}

			pause.Value = p; // done with pausing, so revert the pause in case it was changed for punctuation.
		}

		// in DoTyping we handle firing sounds and creating the next char in the string.
		public void DoTyping()
		{
			// play the sound if enabled
			if (useSounds)
			{
				if (noSoundOnSpaces && message[index] != ' ')
				{
					audioSource.PlayOneShot (sound);
				}

				else
				{
					audioSource.PlayOneShot (sound);
				}
			}

			resultString.Value += message[index]; 	// add one character to the string
			lastChar = message[index]; 				// store the index that we just typed
			index++;								// iterate the index

			if (index >= length)
			{
				DoFinish();
			}

			timer = 0.0f;							// reset timer
			startTime = Time.realtimeSinceStartup;	// update realtime
		}

		public void DoFinish()
		{
			Finish();
			if (finishEvent != null)
			{
				Fsm.Event(finishEvent);
			}
		}

		public override void OnExit()
		{
			// if the state exits before finishing the string
			// then it needs to be auto-completed.
			resultString.Value = message;
		}
	}
}










