using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Int32;

namespace RolePlayer
	{
		/// <summary>
		/// Interaction logic for MainWindow.xaml
		/// </summary>
		public partial class MainWindow : Window
			{
				private CharacterContainer currentChar;
				private List<TextBox> SkillBoxes = new List<TextBox>();
				private List<TextBox> WeaponBox1 = new List<TextBox>();
				private List<TextBox> WeaponBox2 = new List<TextBox>();
				private List<TextBox> WeaponBox3 = new List<TextBox>();
				private List<TextBox> WeaponBox4 = new List<TextBox>();
				private List<TextBox> WeaponBox5 = new List<TextBox>();
				private List<List<TextBox>> BoxBoxes = new List<List<TextBox>>(); 
				//private CommonRules rules = new CommonRules();
				//TODO: autocomplete input by user : Pow should display "Power attack" etc.
				//TODO: Finish logic for storing all user data, items etc.
				public MainWindow()
					{
						InitializeComponent();
						SetBoxLists();
						currentChar = new CharacterContainer();
					}

				private void SaveFile(object sender, RoutedEventArgs e)
					{
						StoreDataProcedure();
						SetClassSkills();
						new Save(currentChar); // constructed blank atm.
					}

				private void SetClassSkills()
					{
						List<string> cSkills = new List<string>(); //13 - 10 
						foreach (var box in FindVisualChildren<CheckBox>(this))
							{
								if (box.IsChecked.Value)
									{
										cSkills.Add(box.Content.ToString());
									}
							}
						currentChar.CharacterClass.ClassSkills.Add(currentChar.CharacterClass.ClassName, cSkills);
					}

				private void SetClassSkills(bool loaded)
					{
						if (currentChar.CharacterClass.ClassSkills.Count == 0)
							{
								return;
							}
						List<string> cSkills = currentChar.CharacterClass.ClassSkills[currentChar.CharacterClass.ClassName];
						foreach (var box in FindVisualChildren<CheckBox>(this))
							{
								foreach (var skill in cSkills)
									{
										// special case, fly, knowledges, craft!!!!
										if (skill.Equals("Fly"))
											{
												if (box.Content.Equals(skill))
													{
														box.IsChecked = true;
													}
											}
										else if (skill.Contains("Know"))
											{
												if (box.Content.Equals(skill))
													{
														box.IsChecked = true;
													}
											}
										else if (skill.Equals("Craft"))
											{
												if (box.Content.Equals(skill))
													{
														box.IsChecked = true;
													}
											}
										else if (box.Name.Contains(skill.Substring(0, 4)))
											{
												box.IsChecked = true;
											}
									}
							}
					}

				private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObject) where T : DependencyObject
					{
						if (depObject != null)
							{
								for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObject); i++)
									{
										DependencyObject child = VisualTreeHelper.GetChild(depObject, i);
										if (child != null && child is T)
											{
												yield return (T) child;
											}
										foreach (T childOfChild in FindVisualChildren<T>(child))
											{
												yield return childOfChild;
											}
									}
							}
					}

				private void StoreDataProcedure()
					{
						int age = 0;
						TryParse(charAge.Text, NumberStyles.Integer, null, out age);
						//visual aspects
						currentChar.CharacterAge = age;
						currentChar.CharacterSize = charSize.Text;
						currentChar.CharacterEyes = charEyes.Text;
						currentChar.CharacterHair = charHair.Text;
						currentChar.CharacterWeight = charWeight.Text;
						currentChar.CharacterHeight = charHeight.Text;
						currentChar.CharacterGender = charGender.Text;
						currentChar.CharacterRace = charRace.Text;
						currentChar.CharacterHomeland = charHomeLand.Text;
						currentChar.CharacterDeity = charDeity.Text;
						currentChar.CharacterClass = new CharacterClass() {ClassLevel = charLevel.Text, ClassName = charClass.Text};
						currentChar.CharacterName = charCharName.Text;
						currentChar.CharacterAlignment = charAlignment.Text;
						currentChar.PlayerName = charPlayerName.Text;
						//stats
						if (coreStr.Text != "")
							currentChar.CoreStrength = Parse(coreStr.Text);
						if (coreDex.Text != "")
							currentChar.CoreDexterity = Parse(coreDex.Text);
						if (coreCon.Text != "")
							currentChar.CoreConstitution = Parse(coreCon.Text);
						if (coreInt.Text != "")
							currentChar.CoreIntelligence = Parse(coreInt.Text);
						if (coreWis.Text != "")
							currentChar.CoreWisdom = Parse(coreWis.Text);
						if (coreCha.Text != "")
							currentChar.CoreCharisma = Parse(coreCha.Text);
						if (coreHP.Text != "")
							currentChar.HitPoints = Parse(coreHP.Text);
						if (coreDR.Text != "")
							currentChar.DamageRes = Parse(coreDR.Text);
						if (coreBab.Text != "")
							currentChar.BaseAttackBonus = Parse(coreBab.Text);
						if (sFortBase.Text != "" && currentChar.CharacterClass.ClassLevel != "")
							currentChar.CharacterClass.FortSave.Add(Parse(currentChar.CharacterClass.ClassLevel), Parse(sFortBase.Text));
						if (sRefBase.Text != "" && currentChar.CharacterClass.ClassLevel != "")
							currentChar.CharacterClass.RefSave.Add(Parse(currentChar.CharacterClass.ClassLevel), Parse(sRefBase.Text));
						if (sWillBase.Text != "" && currentChar.CharacterClass.ClassLevel != "")
							currentChar.CharacterClass.WillSave.Add(Parse(currentChar.CharacterClass.ClassLevel), Parse(sWillBase.Text));
						if (coreBab.Text != "" && currentChar.CharacterClass.ClassLevel != "")
							currentChar.CharacterClass.BaseAttackBonus.Add(Parse(currentChar.CharacterClass.ClassLevel),Parse(coreBab.Text));

						WeaponStatsToContainer();
							//TODO: add storage for each of the important bits
					}

				private void OpenFile(object sender, RoutedEventArgs e)
					{
						var loadThing = new Load();
						currentChar = loadThing.GetCharacterContainer();
						if (currentChar != null)
							{
								PopulateCharacterDataProcedure();
								SetClassSkills(true);
							}
					}

				private void PopulateCharacterDataProcedure()
					{

			#region Stats
						// main statblock
						coreStr.Text = currentChar.CoreStrength.ToString();
						coreDex.Text = currentChar.CoreDexterity.ToString();
						coreCon.Text = currentChar.CoreConstitution.ToString();
						coreInt.Text = currentChar.CoreIntelligence.ToString();
						coreWis.Text = currentChar.CoreWisdom.ToString();
						coreCha.Text = currentChar.CoreCharisma.ToString();
						// mathed things
						coreModStr.Text = DetermineModNum(coreStr.Text);
						coreModDex.Text = DetermineModNum(coreDex.Text);
						coreModCon.Text = DetermineModNum(coreCon.Text);
						coreModInt.Text = DetermineModNum(coreInt.Text);
						coreModWis.Text = DetermineModNum(coreWis.Text);
						coreModCha.Text = DetermineModNum(coreCha.Text);
						//levelstats
						sFortBase.Text = currentChar.CharacterClass.FortSave[Parse(currentChar.CharacterClass.ClassLevel)].ToString();
						sRefBase.Text = currentChar.CharacterClass.RefSave[Parse(currentChar.CharacterClass.ClassLevel)].ToString();
						sWillBase.Text = currentChar.CharacterClass.WillSave[Parse(currentChar.CharacterClass.ClassLevel)].ToString();
						coreBab.Text = currentChar.CharacterClass.BaseAttackBonus[Parse(currentChar.CharacterClass.ClassLevel)].ToString();
						
						if (currentChar.CharacterSize != string.Empty)
							{
								CommonRules.CharacterSize tmp = (CommonRules.CharacterSize) Enum.Parse(
									typeof (CommonRules.CharacterSize),
									ResolveSize((currentChar.CharacterSize.Length <= 1
										? currentChar.CharacterSize
										: currentChar.CharacterSize.Substring(0, 1).ToUpper())));
								acSize.Text = ((int) tmp).ToString();

							}
			#endregion
			#region PlayerData

						charPlayerName.Text = currentChar.PlayerName;
						charCharName.Text = currentChar.CharacterName;
						charAlignment.Text = currentChar.CharacterAlignment;
						charClass.Text = currentChar.CharacterClass.ClassName;
						charLevel.Text = currentChar.CharacterClass.ClassLevel;
						charDeity.Text = currentChar.CharacterDeity;
						charHomeLand.Text = currentChar.CharacterHomeland;
						charRace.Text = currentChar.CharacterRace;
						charSize.Text = currentChar.CharacterSize;
						charGender.Text = currentChar.CharacterGender;
						charAge.Text = currentChar.CharacterAge.ToString();
						charWeight.Text = currentChar.CharacterWeight;
						charHair.Text = currentChar.CharacterHair;
						charEyes.Text = currentChar.CharacterEyes;
			#endregion
			#region WeaponStats

						if (currentChar.Weapon.Count != 0)
							{
								int iterator = 0;
								string[] tmp = new string[7];

								for (var j = 0; j < currentChar.Weapon.Count; j++)
									{
										tmp[0] = currentChar.Weapon[j].Name;
										tmp[1] = currentChar.Weapon[j].AttackBonus;
										tmp[2] = currentChar.Weapon[j].Critical;
										tmp[3] = currentChar.Weapon[j].Damage;
										tmp[4] = currentChar.Weapon[j].Ammunition;
										tmp[5] = currentChar.Weapon[j].Reach;
										tmp[6] = currentChar.Weapon[j].Type;

										for (int i = 0; i < tmp.Length; i++)
											{
													BoxBoxes[iterator][i].Text = tmp[i];
											}
										iterator++;
									}


							}
			#endregion
			#region something else
						//grab stuff from thing to put in box! uuuh
			#endregion

			}

				private string ResolveSize(string inputSize)
					{
						string realSize;
						switch (inputSize)
							{
								case "F":
									realSize = "Fine";
									break;
								case "D":
									realSize = "Diminutive";
									break;
								case "T":
									realSize = "Tiny";
									break;
								case "S":
									realSize = "Small";
									break;
								case "M":
									realSize = "Medium";
									break;
								case "L":
									realSize = "Large";
									break;
								case "H":
									realSize = "Huge";
									break;
								case "G":
									realSize = "Gargantuan";
									break;
								case "C":
									realSize = "Colossal";
									break;

								default:
									realSize = "Medium";
									break;
							}
						return realSize;
					}

				private string DetermineModNum(string value)
					{
						int score; //convert to int.
						TryParse(value, NumberStyles.Integer, null, out score);
						if (score > 10)
							{
								if (score % 2 == 0) //can be divided by 2
									{
										switch (score)
											{
												case 12:
													return "1";
												case 14:
													return "2";
												case 16:
													return "3";
												case 18:
													return "4";
												case 20:
													return "5";
												case 22:
													return "6";
												case 24:
													return "7";
												case 26:
													return "8";
												case 28:
													return "9";
												case 30:
													return "10";

											}
									}
								else
									{
										switch (score)
											{
												case 11:
													return "0";
												case 13:
													return "1";
												case 15:
													return "2";
												case 17:
													return "3";
												case 19:
													return "4";
											}
									}
							}
						else
							{
								if (score % 2 == 0) //can be divided by 2
									{
										switch (score)
											{
												case 10:
													return "0";
												case 8:
													return "-1";
											}
									}
								else
									{
										switch (score)
											{
												case 9:
													return "-1";
												case 7:
													return "-2";
											}
									}
							}
						return null;
					}

				private void UpdateScore(object sender, TextChangedEventArgs e)
			{
				//Strength
				if (! (sender is TextBox))
					{
						return;
					}
				TextBox source = (TextBox)e.Source;
				switch (source.Name)
					{
						case "coreStr":
							coreModStr.Text = DetermineModNum(coreStr.Text);
							break;
						case "coreDex":
							coreModDex.Text = DetermineModNum(coreDex.Text);
							break;
						case "coreCon":
							coreModCon.Text = DetermineModNum(coreCon.Text);
							break;
						case "coreInt":
							coreModInt.Text = DetermineModNum(coreInt.Text);
							break;
						case "coreWis":
							coreModWis.Text = DetermineModNum(coreWis.Text);
							break;
						case "coreCha":
							coreModCha.Text = DetermineModNum(coreCha.Text);
							break;


					}
			}

				private void UpdateSheet(object sender, TextChangedEventArgs e)
			{
				if (! (sender is TextBox))
					{
						return;
					}
				TextBox source = (TextBox)e.Source;
				switch (source.Name)
					{
						case "coreModStr":
							cmbStr.Text = coreModStr.Text;
							cmdStr.Text = coreModStr.Text;
							PopulateSkillBonuses(source);
							break;
						case "coreModDex":
							SetAC(null, null);
							sRefAb.Text = coreModDex.Text;
							cmdDex.Text = coreModDex.Text;
							coreInitDexMod.Text = coreModDex.Text;
							PopulateSkillBonuses(source);
							break;
						case "coreModCon":
							sFortAb.Text = coreModCon.Text;
							PopulateSkillBonuses(source);
							break;
						case "coreModInt":
							//coreModInt.Text = DetermineModNum(coreInt.Text);
							PopulateSkillBonuses(source);
							break;
						case "coreModWis":
							sWillAb.Text = coreModWis.Text;
							PopulateSkillBonuses(source);
							break;
						case "coreModCha":
							PopulateSkillBonuses(source);
							//coreModCha.Text = DetermineModNum(coreCha.Text);
							break;
					}
			
			}

				private void SetAC(Object sender, TextChangedEventArgs e)
					{
							int dexBonus = 0;
							int shield = 0;
							int armor = 0;
							int nat = 0;
							int size = 0;
							int def = 0;
							int misc = 0;
							TryParse(coreModDex.Text, NumberStyles.Any, null, out dexBonus);
							TryParse(acShield.Text, NumberStyles.Any, null, out shield);
							TryParse(acBonus.Text, NumberStyles.Any, null, out armor);
							TryParse(acNatArmor.Text, NumberStyles.Any, null, out nat);
							TryParse(acSize.Text, NumberStyles.Any, null, out size);
							TryParse(acDefMod.Text, NumberStyles.Any, null, out def);
							TryParse(acMisc.Text, NumberStyles.Any, null, out misc);
							acTouch.Text = (10 + dexBonus).ToString(); //touch AC dex+10base
							acTotal.Text = (10 + dexBonus + shield + armor + nat + size +def + misc).ToString();
							acDex.Text = coreModDex.Text;
					}


				private void PopulateSkillBonuses(TextBox sender)
					{
						var rules = new CommonRules();
						if (sender.Name.Contains("Str"))
							{
								//list of all skills with str ab mod
								var list = rules.SkillDictionary["Str"];
								LoopStat(list, "Str", coreModStr.Text);
							}
						if (sender.Name.Contains("Dex"))
							{
								//dex list
								var list = rules.SkillDictionary["Dex"];
								LoopStat(list, "Dex", coreModDex.Text);
							}
						if (sender.Name.Contains("Int"))
							{
								var list = rules.SkillDictionary["Int"];
								LoopStat(list, "Int", coreModInt.Text);
							}
						if (sender.Name.Contains("Wis"))
							{
								var list = rules.SkillDictionary["Wis"];
								LoopStat(list, "Wis", coreModWis.Text);
							}
						if (sender.Name.Contains("Cha"))
							{
								var list = rules.SkillDictionary["Cha"];
								LoopStat(list, "Cha", coreModCha.Text);
							}
					}

				private void LoopStat(List<string> list, string type, string bonus)
					{
						// figure out type of stat, then loop it!

						// special cases to be accounted for
						// Craft 3x, Performance 2x, Profession 2x, Perception, Fly due to very short name
						// substring 0,3 returns Per on both Perception and Performance
						SetBoxLists();
						foreach (var skill in list)
							{
								
								foreach (var skillBox in SkillBoxes)
									{
										if (type.Equals("Int"))
											{
												if (skill.Equals("Fly"))
													{
														if (skillBox.Name.Contains(skill))
															{
																skillBox.Text = bonus;
																break;
															}
													}
												if (skill.Contains("Kno") | skill.Substring(0, (skill.Length > 4? 4: 3)).Contains("Craf"))
													{
														if (/*skillBox.Name.Contains("Kno") |*/ skillBox.Name.Contains("Mod" + skill.Substring(0, (skill.Length > 4? 4: 3))))
															{
																if (skill.Equals("Craft"))
																	{
																		skModCraft1.Text = bonus;
																		skModCraft2.Text = bonus;
																		skModCraft3.Text = bonus;
																		break; //Hack: This is dirty
																	}
																skillBox.Text = bonus;
															}
													}
											
												if (skillBox.Name.Contains((skill).Substring(0,4)))
													{
														skillBox.Text = bonus;
														break;
													}
											} else if (type.Equals("Wis"))
												{
													if (skill.ToLower().Contains("prof"))
														{
															skModProf1.Text = bonus;
															skModProf2.Text = bonus;
															break;
														}
													if (skillBox.Name.Contains("Mod" + skill.Substring(0, (skill.Length > 4 ? 4 : 3))))
														{
															skillBox.Text = bonus;
															break;
														}
												} else if (type.Equals("Cha"))
													{
														if (skill.Contains("Perf"))
															{
																skModPerf1.Text = bonus;
																skModPerf2.Text = bonus;
																break;
															}
														if (skillBox.Name.Contains("Mod" + skill.Substring(0, (skill.Length > 4 ? 4 : 3))) | skill.Contains("Magic"))
														{
															if (skill.Contains("Magic"))
																{
																	skModUseMd.Text = bonus;
																	break;
																}
															skillBox.Text = bonus;
															break;
														}
													}
										else if (skillBox.Name.Contains(skill.Substring(0, 3)) && !skill.Equals("Disguise"))
											{
												skillBox.Text = bonus;
												break;
											}
									}
							}
			

					}

				private void SetBoxLists()
					{
						foreach (var box in FindVisualChildren<TextBox>(this))
							{
								if (box.Name.Contains("skMod"))
								{
									SkillBoxes.Add(box);
					
								}
								if (box.Name.Substring(0, 1).Contains("w"))
									{
									if (box.Name.EndsWith("1"))
										{
											if (! WeaponBox1.Contains(box))
												{
													WeaponBox1.Add(box);
												}
											}
									if (box.Name.EndsWith("2"))
											{
											if (! WeaponBox2.Contains(box))
												WeaponBox2.Add(box);
											}
									if (box.Name.EndsWith("3"))
											{
											if (! WeaponBox3.Contains(box))
												WeaponBox3.Add(box);
											}
									if (box.Name.EndsWith("4"))
											{
											if (! WeaponBox4.Contains(box))
												WeaponBox4.Add(box);
											}
									if (box.Name.EndsWith("5"))
											{
											if (! WeaponBox5.Contains(box))
												WeaponBox5.Add(box);
											}
									}
							}
						BoxBoxes.Add(WeaponBox1);
						BoxBoxes.Add(WeaponBox2);
						BoxBoxes.Add(WeaponBox3);
						BoxBoxes.Add(WeaponBox4);
						BoxBoxes.Add(WeaponBox5);
						
						
						
								
					}

				private void UpdateBab(object sender, TextChangedEventArgs e)
			{
			if (! (sender is TextBox))
					{
						return;
					}
				TextBox source = (TextBox)e.Source;

				cmbBab.Text = coreBab.Text;
				cmdBab.Text = coreBab.Text;
				


			}

				private void WeaponStatsToContainer()
					{
						Weapon weapon = new Weapon();
						currentChar.Weapon = new List<Weapon>();
						if (wName1.Text != "")
							{
								weapon.Name = wName1.Text;
								weapon.AttackBonus = wBonus1.Text;
								weapon.Ammunition = wAmmo1.Text;
								weapon.Reach = wRange1.Text;
								weapon.Damage = wDamage1.Text;
								weapon.Critical = wCrit1.Text;
								weapon.Type = wType1.Text; //superclever :D
								currentChar.Weapon.Add(weapon);
							}
						if (wName2.Text != "")
							{
								weapon = new Weapon();
								weapon.Name = wName2.Text;
								weapon.AttackBonus = wBonus2.Text;
								weapon.Ammunition = wAmmo2.Text;
								weapon.Reach = wRange2.Text;
								weapon.Damage = wDamage2.Text;
								weapon.Critical = wCrit2.Text;
								weapon.Type = wType2.Text; //superclever :D
								currentChar.Weapon.Add(weapon);
							}
						if (wName3.Text != "")
							{
								weapon = new Weapon();
								weapon.Name = wName3.Text;
								weapon.AttackBonus = wBonus3.Text;
								weapon.Ammunition = wAmmo3.Text;
								weapon.Reach = wRange3.Text;
								weapon.Damage = wDamage3.Text;
								weapon.Critical = wCrit3.Text;
								weapon.Type = wType3.Text; //superclever :D
								currentChar.Weapon.Add(weapon);
							}
						if (wName4.Text != "")
							{	
								weapon = new Weapon();
								weapon.Name = wName4.Text;
								weapon.AttackBonus = wBonus4.Text;
								weapon.Ammunition = wAmmo4.Text;
								weapon.Reach = wRange4.Text;
								weapon.Damage = wDamage4.Text;
								weapon.Critical = wCrit4.Text;
								weapon.Type = wType4.Text; //superclever :D
								currentChar.Weapon.Add(weapon);
							}
						if (wName5.Text != "")
							{	
								weapon = new Weapon();
								weapon.Name = wName5.Text;
								weapon.AttackBonus = wBonus5.Text;
								weapon.Ammunition = wAmmo5.Text;
								weapon.Reach = wRange5.Text;
								weapon.Damage = wDamage5.Text;
								weapon.Critical = wCrit5.Text;
								weapon.Type = wType5.Text; //superclever :D
								currentChar.Weapon.Add(weapon);
							}
						
					}
		}
	}
