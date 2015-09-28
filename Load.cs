using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace RolePlayer
	{
	class Load
		{
			private CharacterContainer currentContainer = new CharacterContainer();
		// DONE!

			public Load()
				{
					OpenFileDialog openFile = new OpenFileDialog();
					openFile.Filter = "Javascript serialized files (.json) | *.json";
					openFile.Title = "Open your Pathfinder character sheet";
					openFile.DefaultExt = ".json";
					openFile.ShowDialog();
					if (openFile.FileName == "")
						return;
					currentContainer = JsonConvert.DeserializeObject<CharacterContainer>(File.ReadAllText(openFile.FileName));
				}

			public CharacterContainer GetCharacterContainer()
				{
					
					return currentContainer;
				}
		}
	}
