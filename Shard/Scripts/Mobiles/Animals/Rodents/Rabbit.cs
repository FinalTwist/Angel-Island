/*
 *	This program is the CONFIDENTIAL and PROPRIETARY property 
 *	of Tomasello Software LLC. Any unauthorized use, reproduction or
 *	transfer of this computer program is strictly prohibited.
 *
 *      Copyright (c) 2004 Tomasello Software LLC.
 *	This is an unpublished work, and is subject to limited distribution and
 *	restricted disclosure only. ALL RIGHTS RESERVED.
 *
 *			RESTRICTED RIGHTS LEGEND
 *	Use, duplication, or disclosure by the Government is subject to
 *	restrictions set forth in subparagraph (c)(1)(ii) of the Rights in
 * 	Technical Data and Computer Software clause at DFARS 252.227-7013.
 *
 *	Angel Island UO Shard	Version 1.0
 *			Release A
 *			March 25, 2004
 */

/* ./Scripts/Mobiles/Animals/Rodents/Rabbit.cs
 *	ChangeLog :
 *  8/16/06, Rhiannon
 *		Changed speed settings to match SpeedInfo table.
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 2 lines removed.
*/

using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("a hare corpse")]
	public class Rabbit : BaseCreature
	{
		[Constructable]
		public Rabbit()
			: base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.25, 0.5)
		{
			Name = "a rabbit";
			Body = 205;

			if (0.5 >= Utility.RandomDouble())
				Hue = Utility.RandomAnimalHue();

			SetStr(6, 10);
			SetDex(26, 38);
			SetInt(6, 14);

			SetHits(4, 6);
			SetMana(0);

			SetDamage(1);

			SetSkill(SkillName.MagicResist, 5.0);
			SetSkill(SkillName.Tactics, 5.0);
			SetSkill(SkillName.Wrestling, 5.0);

			Fame = 150;
			Karma = 0;

			VirtualArmor = 6;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -18.9;
		}

		public override int Meat { get { return 1; } }
		public override int Hides { get { return 1; } }
		public override FoodType FavoriteFood { get { return FoodType.FruitsAndVegies; } }

		public Rabbit(Serial serial)
			: base(serial)
		{
		}

		public override int GetAttackSound()
		{
			return 0xC9;
		}

		public override int GetHurtSound()
		{
			return 0xCA;
		}

		public override int GetDeathSound()
		{
			return 0xCB;
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}