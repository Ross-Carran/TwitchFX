using TwitchFX.Colors;
using UnityEngine;

namespace TwitchFX.Commands {
	
	public class CommandSetSaberColor : Command {
		
		public CommandSetSaberColor() {
			
			usage = "<left color> <right color> OR\n" +
			"<left color> <right color> <duration in seconds>";
			
			SetArgsCount(2, 3);
			
		}
		
		protected override void Execute(string[] args) {
			
			Color leftColor = ParseColor(args[0]);
			Color rightColor = ParseColor(args[1]);

            if (leftColor == rightColor)
            {
                throw new InvalidCommandExecutionException("Command not executed: You must enter two different colors as arguments");
            }
            else
            {
                float? duration = TryParseFloat(args, 2);

                ColorController.instance.SetSaberColors(leftColor, rightColor, duration);
            }
        }
		
	}
	
}