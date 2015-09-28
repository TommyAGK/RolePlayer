namespace RolePlayer
	{
	class Weapon
		{
			// DONE!
			public string Name { get; set; }
			public string Damage { get; set; }
			public string Reach { get; set; }
			public string AttackBonus { get; set; }
			public string Ammunition { get; set; }
			public string Critical { get; set; }
			private string type;
			public string Type
				{
					get { return type; }
					set { type = ResolveString(value); }
				}

			private readonly string[] wTypes;
			public Weapon()
				{
					wTypes = new[] {"Piercing", "Blunt", "Slashing", "Exlposive", "Non-Lethal"};
				}

			private string ResolveString(string input)
				{
					foreach (var type in wTypes)
						{
							if (type.Contains(input) | type.Equals(input))
								{
									return type;
								}
						}

					return wTypes[0];
				}
		}
	}
