using System.Collections.Generic;

namespace RolePlayer
	{
	class CharacterContainer
		{
			public string PlayerName { get; set; }
			public string CharacterAlignment { get; set; }
			public string CharacterName { get; set; }
			public CharacterClass CharacterClass { get; set; } //could be an enum. But to allow custom classes, its a string
			public string CharacterDeity { get; set; }
			public string CharacterHomeland { get; set; }
			public string CharacterRace { get; set; }
			public string CharacterGender { get; set; }
			public string CharacterHeight { get; set; }
			public string CharacterWeight { get; set; }
			public string CharacterHair { get; set; }
			public string CharacterEyes { get; set; }
			public string CharacterSize { get;set; }
			public int CharacterAge { get; set; }
			
			// stat area

			//base scores
			public int CoreStrength { get; set; }
			public int CoreDexterity { get; set; }
			public int CoreConstitution { get; set; }
			public int CoreIntelligence { get; set; }
			public int CoreWisdom { get; set; }
			public int CoreCharisma { get; set; }
			
			//other nums
			public int HitPoints { get; set; } // this is based off class and con, user input. Cheat if you want too.
			public int DamageRes { get; set; } // user input
			public int BaseAttackBonus { get; set; } //based off class and level
			
			// Equipment
			public List<Weapon> Weapon { get; set; }

			public CharacterContainer()
				{
					Weapon = new List<Weapon>();
				}
		
			
		
		}
	}
