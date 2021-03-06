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

/* Scripts/Mobiles/Monsters/Humanoid/Melee/Ranger.cs
 * ChangeLog
 *  7/02/06, Kit
 *		InitBody/InitOutfit additions, changed rangefight to 6
 *  9/20/05, Adam
 *		Make bard immune.
 *  9/19/05, Adam
 *		a. Change Karma loss to that for a 'good' aligned creature
 *		b. remove powder of transloacation
 *  9/16/05, Adam
 *		spawned from BrigandArcher.cs
 */

using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Engines.IOBSystem;

namespace Server.Mobiles
{
	public class Ranger : BaseCreature
	{
		[Constructable]
		public Ranger()
			: base(AIType.AI_Archer, FightMode.Aggressor | FightMode.Criminal, 10, 6, 0.2, 0.4)
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the ranger";
			Hue = Utility.RandomSkinHue();
			IOBAlignment = IOBAlignment.Good;
			ControlSlots = 3;
			BardImmune = true;

			SetStr(146, 200);
			SetDex(130, 170);
			SetInt(51, 65);

			SetDamage(23, 27);

			SetSkill(SkillName.Archery, 90.0, 100.5);
			SetSkill(SkillName.Macing, 60.0, 82.5);
			SetSkill(SkillName.Poisoning, 60.0, 82.5);
			SetSkill(SkillName.MagicResist, 57.5, 80.0);
			SetSkill(SkillName.Anatomy, 80.0, 92.5);
			SetSkill(SkillName.Tactics, 80.0, 92.5);

			Fame = 1500;
			Karma = 1500;

			InitBody();
			InitOutfit();
			PackItem(new Bandage(Utility.RandomMinMax(1, 15)));
			PackItem(new FancyShirt(Utility.RandomNeutralHue()));
			PackItem(new LongPants(Utility.RandomNeutralHue()));

		}

		public override bool AlwaysMurderer { get { return false; } }
		public override bool ShowFameTitle { get { return false; } }
		public override bool CanRummageCorpses { get { return false; } }
		public override bool ClickTitle { get { return true; } }

		public override bool CanBandage { get { return true; } }
		public override TimeSpan BandageDelay { get { return TimeSpan.FromSeconds(Utility.RandomMinMax(10, 11)); } }

		public Ranger(Serial serial)
			: base(serial)
		{
		}

		public override void InitBody()
		{
			if (this.Female = Utility.RandomBool())
			{
				Body = 0x191;
				Name = NameList.RandomName("female");
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName("male");
			}
		}
		public override void InitOutfit()
		{
			WipeLayers();
			Item hair = new Item(Utility.RandomList(0x203B, 0x203C, 0x203D, 0x2048));
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem(hair);

			double chance = 0.98;
			AddItem(new RangerArms(), Utility.RandomDouble() < chance ? LootType.Newbied : LootType.Regular);
			AddItem(new RangerChest(), Utility.RandomDouble() < chance ? LootType.Newbied : LootType.Regular);
			AddItem(new RangerGloves(), Utility.RandomDouble() < chance ? LootType.Newbied : LootType.Regular);
			AddItem(new RangerGorget(), Utility.RandomDouble() < chance ? LootType.Newbied : LootType.Regular);
			AddItem(new RangerLegs(), Utility.RandomDouble() < chance ? LootType.Newbied : LootType.Regular);
			AddItem(new Boots(0x5E4), LootType.Newbied); // never drop, you need the IOB version
			AddItem(new Bow());
		}


		public override void GenerateLoot()
		{
			if (Core.UOAI || Core.UOAR)
			{
				PackGold(170, 220);

				PackItem(new Arrow(Utility.RandomMinMax(20, 30)));

				// Froste: 12% random IOB drop
				if (0.12 > Utility.RandomDouble())
				{
					Item iob = Loot.RandomIOB();
					PackItem(iob);
				}

				if (IOBRegions.GetIOBStronghold(this) == IOBAlignment)
				{
					// 30% boost to gold
					PackGold(base.GetGold() / 3);
				}
			}
			else
			{
				if (Core.UOSP || Core.UOMO)
				{	// ai special
					if (Spawning)
					{
						PackGold(0);
					}
					else
					{
					}
				}
				else
				{
					// ai special
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
