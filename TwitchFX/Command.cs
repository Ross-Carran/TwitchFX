﻿using System;
using System.Collections.Generic;
using TwitchFX.Configuration;
using UnityEngine;

namespace TwitchFX {
	
	public abstract class Command {
		
		private static readonly Dictionary<string, Command> commands = new Dictionary<string, Command>();
		
		public static Command GetCommand(string name) {
			
			return commands.ContainsKey(name.ToLower()) ? commands[name.ToLower()] : null;
			
		}
		
		private readonly PermissionsLevel requiredPermissions;
		
		private string name;
		private string usage = "";
		
		protected Command() {
			
			string command = this.GetType().Name.Substring(7, this.GetType().Name.Length - 7).ToLower();
			
			if (!PluginConfig.instance.commands.ContainsKey(command))
				PluginConfig.instance.commands[command] = command;
			
			name = PluginConfig.instance.commands[command];
			
			commands[name.ToLower()] = this;
			
			if (!PluginConfig.instance.commandsRequiredPermissions.ContainsKey(command))
				PluginConfig.instance.commandsRequiredPermissions[command] = "everyone";
			
			switch (PluginConfig.instance.commandsRequiredPermissions[command].ToLower()) {
			case "broadcaster":
				requiredPermissions = PermissionsLevel.Broadcaster;
				break;
			case "moderator":
				requiredPermissions = PermissionsLevel.Moderator;
				break;
			case "vip":
				requiredPermissions = PermissionsLevel.VIP;
				break;
			case "subscriber":
				requiredPermissions = PermissionsLevel.Subscriber;
				break;
			case "everyone":
				requiredPermissions = PermissionsLevel.Everyone;
				break;
			default:
				PluginConfig.instance.commandsRequiredPermissions[command] = "everyone";
				requiredPermissions = PermissionsLevel.Everyone;
				break;
			}
			
		}
		
		public bool CanExecute(PermissionsLevel permissions) {
			
			return permissions >= requiredPermissions;
			
		}
		
		protected void SetUsage(string usage) {
			
			this.usage = usage;
			
		}
		
		protected InvalidCommandArgumentsException CreateInvalidArgs() {
			
			string[] lines = usage.Split('\n');
			
			for (int i = 0; i < lines.Length; i++)
				lines[i] = "!" + name + " " + lines[i];
			
			throw new InvalidCommandArgumentsException(lines);
			
		}
		
		protected string[] ParseArgs(string argsStr, int expectedMin = -1, int expectedMax = -1) {
			
			string[] args = argsStr.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
			
			if (expectedMin != -1 && (args.Length < expectedMin || args.Length > (expectedMax != -1 ? expectedMax : expectedMin)))
				throw CreateInvalidArgs();
			
			return args;
			
		}
		
		protected float TryParseFloat(string[] args, int index) {
			
			if (index >= args.Length)
				return 0f;
			
			if (!float.TryParse(args[index], out float f))
				throw CreateInvalidArgs();
			
			return f;
			
		}
		
		protected Color ParseColor(string colorStr) {
			
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
				
				return new Color(r, g, b);
				
			}
			
			switch (colorStr) {
			case "black":
				return Color.black;
			case "blue":
				return Color.blue;
			case "cyan":
				return Color.cyan;
			case "gray":
			case "grey":
				return Color.gray;
			case "green":
				return Color.green;
			case "magenta":
				return Color.magenta;
			case "red":
				return Color.red;
			case "white":
				return Color.white;
			case "yellow":
				return Color.yellow;
			}
			
			throw CreateInvalidArgs();
			
		}
		
		private int ParseHexChar(char hex) {
			
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
		
		public abstract void Execute(string argsStr);
		
	}
	
}