
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace RolePlayer
	{
	class Save
		{ 
			// DONE!
			private CharacterContainer toBeSaved;
			private string SavePath;
			public Save(CharacterContainer currentChar)
				{
					toBeSaved = currentChar;
					PromtDirectory();
				}



			private void PromtDirectory()
				{
					SaveFileDialog saveFile = new SaveFileDialog();
					saveFile.CreatePrompt = true;
					saveFile.AddExtension = true; //add the extension no matter what
					saveFile.DefaultExt = "*.json";
					saveFile.Filter = "Javascript serialized files (.json) | *.json";
					saveFile.ShowDialog();
					if (saveFile.FileName == "")
						return;
					File.WriteAllText(saveFile.FileName, JsonConvert.SerializeObject(toBeSaved, Formatting.Indented));
					
				}
		}
	}
