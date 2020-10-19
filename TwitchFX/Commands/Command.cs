﻿using System;
using System.Collections.Generic;
using TwitchFX.Configuration;
using UnityEngine;

namespace TwitchFX.Commands {
	
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
			
			if (!Helper.TryParseColor(colorStr, out Color color))
				throw CreateInvalidArgs();
			
			return color;
			
		}
		
		public abstract void Execute(string argsStr);
		
	}
	
}