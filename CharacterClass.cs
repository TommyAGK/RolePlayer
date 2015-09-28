using System.Collections.Generic;

namespace RolePlayer
	{
	class CharacterClass
		{
			public string ClassName { get; set; }
			public string ClassLevel { get; set; }
			public Dictionary<string, List<string>> ClassSkills;

			public CharacterClass()
				{
					ClassSkills = new Dictionary<string, List<string>>();
					BaseAttackBonus = new Dictionary<int, int>();
					FortSave = new Dictionary<int, int>();
					RefSave = new Dictionary<int, int>();
					WillSave = new Dictionary<int, int>();
					Special = new Dictionary<int, Dictionary<string, string>>();
					Ex = new Dictionary<string, string>();
					Su = new Dictionary<string, string>();

				}
			//base dictionaries
			public Dictionary<int,int> BaseAttackBonus { get; set; } // level, score
			public Dictionary<int,int> FortSave  { get; set; }
			public Dictionary<int,int> RefSave { get; set; }
			public Dictionary<int,int> WillSave { get; set; }
			public Dictionary<int,Dictionary<string, string>> Special { get; set; }
			
			//class specific information collections
			public Dictionary<string, string> Ex { get; set; }
			public Dictionary<string, string> Su { get; set; } 


		}
	}
