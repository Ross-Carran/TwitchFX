﻿using System;
using System.Reflection;
using UnityEngine;

namespace TwitchFX {
	
	public class Helper {
		
		private static readonly Color COLOR_RAINBOW = new Color(1337f, 420f, 69f);
		
		private static readonly Color COLOR_ORANGE = new Color(1f, 0.45098039215f, 0f);
		private static readonly Color COLOR_LIME = new Color(0.70196078431f, 1f, 0f);
		private static readonly Color COLOR_SPRING_GREEN = new Color(0f, 1f, 0.58823529411f);
		private static readonly Color COLOR_LIGHT_BLUE = new Color(0f, 0.54901960784f, 1f);
		private static readonly Color COLOR_PURPLE = new Color(0.58823529411f, 0f, 1f);
		private static readonly Color COLOR_PINK = new Color(1f, 0f, 0.50196078431f);
		
		public static T GetValue<T>(object obj, string name) {
			
			FieldInfo field = obj.GetType().GetField(
				name,
				BindingFlags.NonPublic |
				BindingFlags.Public |
				BindingFlags.Instance
			);
			
			return (T) field.GetValue(obj);
			
		}
		
		public static T GetValue<C, T>(object obj, string name) {
			
			FieldInfo field = typeof(C).GetField(
				name,
				BindingFlags.NonPublic |
				BindingFlags.Public |
				BindingFlags.Instance
			);
			
			return (T) field.GetValue(obj);
			
		}
		
		public static void SetValue<T>(object obj, string name, T value) {
			
			FieldInfo field = obj.GetType().GetField(
				name,
				BindingFlags.NonPublic |
				BindingFlags.Public |
				BindingFlags.Instance
			);
			
			field.SetValue(obj, value);
			
		}
		
		public static void SetValue<C, T>(object obj, string name, T value) {
			
			FieldInfo field = typeof(C).GetField(
				name,
				BindingFlags.NonPublic |
				BindingFlags.Public |
				BindingFlags.Instance
			);
			
			field.SetValue(obj, value);
			
		}
		
		public static bool IsRainbow(Color color) {
			
			return color.Equals(COLOR_RAINBOW);
			
		}
		
		public static bool TryParseColor(string colorStr, out Color color) {
			
			if (
				colorStr.Equals("rainbow", StringComparison.OrdinalIgnoreCase) ||
				colorStr.Equals("gay", StringComparison.OrdinalIgnoreCase)
			) {
				
				//gay lol
				
				color = COLOR_RAINBOW;
				
				return true;
				
			}
			
			if (colorStr.StartsWith("#")) { //hex color code
				
				switch (colorStr.Length) {
				case 1:
					colorStr += "000";
					break;
				case 2:
					colorStr += "00";
					break;
				case 3:
					colorStr += "0";
					break;
				case 5:
					colorStr += "00";
					break;
				case 6:
					colorStr += "0";
					break;
				}
				
				float r, g, b;
				
				if (colorStr.Length == 4) {
					
					r = ParseHexChar(colorStr[1]) / 16f;
					g = ParseHexChar(colorStr[2]) / 16f;
					b = ParseHexChar(colorStr[3]) / 16f;
					
				} else {
					
					r = (ParseHexChar(colorStr[1]) * 16 + ParseHexChar(colorStr[2])) / 255f;
					g = (ParseHexChar(colorStr[3]) * 16 + ParseHexChar(colorStr[4])) / 255f;
					b = (ParseHexChar(colorStr[5]) * 16 + ParseHexChar(colorStr[6])) / 255f;
					
				}
				
				color = new Color(r, g, b);
				
				return true;
				
			}
			
			switch (colorStr.ToLower()) {
			case "black":
				color = Color.black;
				return true;
			case "darkblue":
			case "dark_blue":
				color = Color.blue;
				return true;
			case "cyan":
				color = Color.cyan;
				return true;
			case "gray":
			case "grey":
				color = Color.gray;
				return true;
			case "green":
				color = Color.green;
				return true;
			case "magenta":
				color = Color.magenta;
				return true;
			case "red":
				color = Color.red;
				return true;
			case "white":
				color = Color.white;
				return true;
			case "yellow":
				color = Color.yellow;
				return true;
			case "orange":
				color = COLOR_ORANGE;
				return true;
			case "lime":
				color = COLOR_LIME;
				return true;
			case "springgreen":
			case "spring_green":
			case "cyangreen":
			case "cyan_green":
			case "greencyan":
			case "green_cyan":
				color = COLOR_SPRING_GREEN;
				return true;
			case "blue":
			case "lightblue":
			case "light_blue":
				color = COLOR_LIGHT_BLUE;
				return true;
			case "purple":
				color = COLOR_PURPLE;
				return true;
			case "pink":
				color = COLOR_PINK;
				return true;
			}
			
			color = Color.black;
			
			return false;
			
		}
		
		private static int ParseHexChar(char hex) {
			
			switch (hex) {
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case 'a':
			case 'A':
				return 10;
			case 'b':
			case 'B':
				return 11;
			case 'c':
			case 'C':
				return 12;
			case 'd':
			case 'D':
				return 13;
			case 'e':
			case 'E':
				return 14;
			case 'f':
			case 'F':
				return 15;
			default:
				return 0;
			}
			
		}
		
	}
	
}