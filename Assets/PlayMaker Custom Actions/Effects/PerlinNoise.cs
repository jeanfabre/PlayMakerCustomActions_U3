/// <summary>
/// 
/// Jean Fabre April 2011
/// http://www.fabrejean.net
/// 
/// More infos on the playmaker api for actions: https://hutonggames.fogbugz.com/default.asp?W351
/// 
/// This action uses PerlinNoise generator with a random seed on the x axis and an animated value on the y axis.
/// 
/// </summary>
using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Effects)]
	[Tooltip("PerlinNoise action")]
	public class PerlinNoise : FsmStateAction {

		[RequiredField]
		[Tooltip("PerlinNoise animation speed")]
		public FsmFloat speed;
		
		[RequiredField]
		[Tooltip("the actual PerlinNoise result ranging from 0 to 1")]
		[UIHint(UIHint.Variable)]
		public FsmFloat perlinNoise;
		
		[Tooltip("If set to false, will not animate over time")]
		public bool everyFrame;
		
		/// <summary>
		/// randomness for the perlinnoise.
		/// </summary> 
		private float _seed;
		
		/// <summary>
		/// Reset all values
		/// </summary>
		public override void Reset()
		{		
			
			_seed = Random.Range(0f, 65535f);
			speed = new FsmFloat();
			speed.Value = 1f;
			perlinNoise= null;
			everyFrame = true;	
			
		}// Reset
	
		/// <summary>
		/// Compute the perlinNoise and finish the action if only supposed to run once.
		/// <see cref="everyFrame"/>
		/// </summary>		
		public override void OnEnter()
		{
			
			ComputePerlinNoise();
			
			if (!everyFrame)
				Finish();
			
		}// OnEnter
	
		
		/// <summary>
		/// If the action is suppose to run every frame, we compute the noise here
		/// <see cref="everyFrame"/>
		/// </summary>
		public override void OnUpdate()
		{
			
			ComputePerlinNoise();	
			
		}// OnUpdate

		
		/// <summary>
		/// Compute and store the current perlin noise.
		/// </summary>
		private void ComputePerlinNoise(){
			
			perlinNoise.Value = Mathf.PerlinNoise(_seed, speed.Value*Time.time);
			
		}// ComputePerlinNoise
	
	}// PerlinNoise
}// namespace HutongGames.PlayMaker.Actions