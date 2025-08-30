using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AtlantisSoftware;

public class Race
{
	private byte[] xRaceData;

	public Collection Attributes;

	public byte[] RaceData
	{
		get
		{
			return xRaceData;
		}
		set
		{
			Attributes = new Collection();
			xRaceData = value;
			Attributes.Add(value[0], "RaceID");
			Attributes.Add(checked(value[8] + value[9] * 256) & 0x3FF, "Homeworld");
		}
	}

	public int RaceID => Conversions.ToInteger(Attributes["RaceID"]);

	public int Homeworld
	{
		get
		{
			return Conversions.ToInteger(Attributes["Homeworld"]);
		}
		set
		{
			if (value >= 0 && value <= 1023)
			{
				Attributes.Remove("Homeworld");
				Attributes.Add(value, "Homeworld");
				checked
				{
					int num = ((xRaceData[8] + xRaceData[9] * 256) & 0xFB00) + value;
					xRaceData[8] = (byte)unchecked(num % 256);
					xRaceData[9] = (byte)Math.Round(Conversion.Int((double)num / 256.0));
				}
			}
		}
	}

	[DebuggerNonUserCode]
	public Race()
	{
	}
}
