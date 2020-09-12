﻿using UnityEngine;

namespace TwitchFX {
	
	class LightController : MonoBehaviour {
		
		public static LightController instance { get; private set; }
		
		public ConfigurableColorSO colorLeft;
		public ConfigurableColorSO colorRight;
		public ConfigurableColorSO highlightcolorLeft;
		public ConfigurableColorSO highlightcolorRight;
		
		private void Awake() {
			
			if (instance != null) {
				
				Logger.log.Warn("Instance of LightController already exists, destroying.");
				
				Destroy(this);
				
				return;
				
			}
			
			instance = this;
			
		}
		
		private void Start() {
			
			LightSwitchEventEffect[] lights = Resources.FindObjectsOfTypeAll<LightSwitchEventEffect>();
			
			LightSwitchEventEffect ligt = lights[0];
			
			colorRight = new ConfigurableColorSO(Helper.GetValue<ColorSO>(ligt, "_lightColor0"));
			colorLeft = new ConfigurableColorSO(Helper.GetValue<ColorSO>(ligt, "_lightColor1"));
			highlightcolorRight = new ConfigurableColorSO(Helper.GetValue<ColorSO>(ligt, "_highlightColor0"));
			highlightcolorLeft = new ConfigurableColorSO(Helper.GetValue<ColorSO>(ligt, "_highlightColor1"));
			
			foreach(LightSwitchEventEffect light in lights) {
				
				Helper.SetValue<ColorSO>(light, "_lightColor0", colorRight);
				Helper.SetValue<ColorSO>(light, "_lightColor1", colorLeft);
				Helper.SetValue<ColorSO>(light, "_highlightColor0", highlightcolorRight);
				Helper.SetValue<ColorSO>(light, "_highlightColor1", highlightcolorLeft);
				
			}
			
		}
		
		private void OnDestroy() {
			
			if (instance == this)
				instance = null;
			
		}
		
	}
	
}