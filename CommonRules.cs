
using System.Collections.Generic;

namespace RolePlayer
	{
	class CommonRules
		{
		public enum CharacterSize
				{
					Fine = 8,
					Diminutive = 4,
					Tiny = 2,
					Small = 1,
					Medium = 0,
					Large = -1,
					Huge = -2,
					Gargantuan = -4,
					Colossal = -8
 				}
		Dictionary<string, int[]> CarryCapacity = new Dictionary<string, int[]>();
		
		public Dictionary<string, List<string>> SkillDictionary = new Dictionary<string, List<string>>();


		public CommonRules()
				{
					CarryCapacity.Add("Heavy",new []{1,-6}); //max dex, check penalty
					CarryCapacity.Add("Medium",new []{3,-3});
					SetLists();
				}


			private void SetLists()
				{
					//dex
					List<string> DexSkills = new List<string>();
					DexSkills.Add("Acrobatics");
					DexSkills.Add("Disable Device*");
					DexSkills.Add("Escape Artist");
					DexSkills.Add("Fly");
					DexSkills.Add("Ride");
					DexSkills.Add("Sleight of Hand*");
					DexSkills.Add("Stealth");
					SkillDictionary.Add("Dex",DexSkills);

					List<string> IntSkills = new List<string>();
					IntSkills.Add("Appraise");
					IntSkills.Add("Craft");
					IntSkills.Add("Knowledge");
					IntSkills.Add("Lingustics*");
					IntSkills.Add("Spellcraft*");
					SkillDictionary.Add("Int",IntSkills);

					List<string> ChaSkills = new List<string>();
					ChaSkills.Add("Bluff");
					ChaSkills.Add("Diplomacy");
					ChaSkills.Add("Disguise");
					ChaSkills.Add("Handle Animal*");
					ChaSkills.Add("Intimidate");
					ChaSkills.Add("Perform");
					ChaSkills.Add("Use Magic Device*");
					SkillDictionary.Add("Cha",ChaSkills);

					List<string> StrSkills = new List<string>();
					StrSkills.Add("Climb");
					StrSkills.Add("Swim");
					SkillDictionary.Add("Str",StrSkills);

					List<string> WisSkills = new List<string>();
					WisSkills.Add("Heal");
					WisSkills.Add("Perception");
					WisSkills.Add("Profession*");
					WisSkills.Add("Sense Motive");
					WisSkills.Add("Survivial");
					SkillDictionary.Add("Wis",WisSkills);
				}
		
		}
	}
