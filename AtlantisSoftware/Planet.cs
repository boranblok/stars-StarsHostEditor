using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AtlantisSoftware;

public class Planet
{
	private byte[] xData;

	private int xPlanetID;

	private int xOwnerID;

	private bool xHomeworld;

	private int xStarbase;

	private bool xInstallations;

	private bool xArtefact;

	private bool xSurfaceMinerals;

	private int xIronium;

	private int xBoranium;

	private int xGermanium;

	private int xPopulation;

	private int xExcessPopulation;

	private int xIroniumConcentration;

	private int xBoraniumConcentration;

	private int xGermaniumConcentration;

	private int xTemperature;

	private int xGravity;

	private int xRadiation;

	private int xOriginalTemperature;

	private int xOriginalGravity;

	private int xOriginalRadiation;

	private bool xTerraformed;

	private int xX;

	private int xY;

	private int xNameID;

	private string xName;

	private int xEstimatedPopulation;

	private int xFactories;

	private int xMines;

	private int xDefenses;

	private int xStarbaseSlotID;

	private bool xScanner;

	private int xUnknown1;

	private int xUnknown2;

	private int xUnknown3;

	private int xStarbaseUnknown1;

	private int xMDTarget;

	private int xMDSpeed;

	private int xStarbaseDamage;

	private int tConcentration;

	private int tSurface;

	private int tInstallations;

	private int tStarbase;

	public byte[] PlanetData
	{
		get
		{
			return xData;
		}
		set
		{
			int Start = 0;
			xData = value;
			object objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 2));
			xPlanetID = Conversions.ToInteger(Operators.AndObject(objectValue, 1023));
			xOwnerID = Conversions.ToInteger(Conversion.Int(Operators.DivideObject(objectValue, 2048)));
			if (xOwnerID == 31)
			{
				xOwnerID = -1;
			}
			objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 2));
			xHomeworld = Conversions.ToBoolean(functions.GetBit(Conversions.ToInteger(objectValue), 7));
			xStarbase = Conversions.ToInteger(functions.GetBit(Conversions.ToInteger(objectValue), 9));
			xTerraformed = Conversions.ToBoolean(functions.GetBit(Conversions.ToInteger(objectValue), 10));
			xInstallations = Conversions.ToBoolean(functions.GetBit(Conversions.ToInteger(objectValue), 11));
			xArtefact = Conversions.ToBoolean(functions.GetBit(Conversions.ToInteger(objectValue), 12));
			xSurfaceMinerals = Conversions.ToBoolean(functions.GetBit(Conversions.ToInteger(objectValue), 13));
			objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 1));
			object objectValue2 = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, Conversions.ToInteger(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 0, 2))));
			objectValue2 = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, Conversions.ToInteger(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 2, 2))));
			objectValue2 = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, Conversions.ToInteger(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 4, 2))));
			tConcentration = Start;
			xIroniumConcentration = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
			xBoraniumConcentration = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
			xGermaniumConcentration = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
			xGravity = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
			xTemperature = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
			xRadiation = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
			checked
			{
				if (!xTerraformed)
				{
					xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 + 3 + 1]);
					Array.Copy(xData, Start, xData, Start + 3, xData.Length - Start - 3);
					xData[Start] = (byte)xGravity;
					xData[Start + 1] = (byte)xTemperature;
					xData[Start + 2] = (byte)xRadiation;
					xData[3] = (byte)(xData[3] | 4);
					xTerraformed = true;
				}
				xOriginalGravity = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
				xOriginalTemperature = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
				xOriginalRadiation = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
				if (xOwnerID != -1)
				{
					xEstimatedPopulation = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 2));
				}
				tSurface = Start;
				if (!xSurfaceMinerals)
				{
					xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 + 1 + 1]);
					Array.Copy(xData, Start, xData, Start + 1, xData.Length - Start - 1);
					xData[3] = (byte)(xData[3] | 0x20);
					xSurfaceMinerals = true;
					xData[Start] = 0;
				}
				objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 1));
				int num = Conversions.ToInteger(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 0, 2), 1)));
				int num2 = Conversions.ToInteger(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 2, 2), 1)));
				int num3 = Conversions.ToInteger(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 4, 2), 1)));
				int num4 = Conversions.ToInteger(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 6, 2), 1)));
				xIronium = Conversions.ToInteger(functions.GetBytes(xData, ref Start, num));
				xBoranium = Conversions.ToInteger(functions.GetBytes(xData, ref Start, num2));
				xGermanium = Conversions.ToInteger(functions.GetBytes(xData, ref Start, num3));
				xPopulation = Conversions.ToInteger(functions.GetBytes(xData, ref Start, num4));
				if (num4 > 0)
				{
					xExcessPopulation = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
					num4++;
				}
				int num5 = num + num2 + num3 + num4;
				int num6 = 17;
				xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 + num6 - num5 + 1]);
				Array.Copy(xData, Start, xData, Start + num6 - num5, xData.Length - num6 + num5 - Start);
				xData[tSurface] = byte.MaxValue;
				Start = Start + num6 - num5;
				Ironium = xIronium;
				Boranium = xBoranium;
				Germanium = xGermanium;
				Population = xPopulation;
				if (!xInstallations)
				{
					xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 + 7 + 1]);
					Array.Copy(xData, Start, xData, Start + 7, xData.Length - Start - 7);
					xData[3] = (byte)(xData[3] | 8);
					xData[Start] = 0;
					xData[Start + 1] = 0;
					xData[Start + 2] = 0;
					xData[Start + 3] = 0;
					xData[Start + 4] = 0;
					xData[Start + 5] = 1;
					xData[Start + 6] = 0;
				}
				tInstallations = Start;
				objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 3));
				xMines = Conversions.ToInteger(Operators.AndObject(objectValue, 4095));
				xFactories = Conversions.ToInteger(Conversion.Int(Operators.DivideObject(objectValue, 4096)));
				objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 2));
				xDefenses = Conversions.ToInteger(Operators.AndObject(objectValue, 4095));
				xUnknown1 = Conversions.ToInteger(Conversion.Int(Operators.DivideObject(objectValue, 4096)));
				xUnknown2 = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
				xScanner = Conversions.ToBoolean(Operators.NotObject(functions.GetBits(xUnknown2, 0, 1)));
				xUnknown3 = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
				tStarbase = Start;
				if (xStarbase != 0)
				{
					objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 1));
					xStarbaseSlotID = Conversions.ToInteger(Operators.AndObject(objectValue, 15));
					xStarbaseDamage = Conversions.ToInteger(Conversion.Int(Operators.DivideObject(objectValue, 16)));
					xStarbaseUnknown1 = Conversions.ToInteger(functions.GetBytes(xData, ref Start, 1));
					xStarbaseDamage += (xStarbaseUnknown1 & 0x1F) * 16;
					objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(xData, ref Start, 2));
					xMDTarget = Conversions.ToInteger(Operators.AndObject(objectValue, 1023));
					xMDSpeed = Conversions.ToInteger(Operators.AddObject(Conversion.Int(Operators.DivideObject(objectValue, 1024)), 4));
				}
				else
				{
					xStarbaseSlotID = -1;
				}
			}
		}
	}

	public int Factories
	{
		get
		{
			return xFactories;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 4095)
			{
				value = 4095;
			}
			xFactories = value;
			checked
			{
				xData[tInstallations + 1] = (byte)((xData[tInstallations + 1] & 0xF) + Conversion.Int(value & 0xF) * 16);
				xData[tInstallations + 2] = (byte)Math.Round(Conversion.Int((double)value / 16.0));
			}
		}
	}

	public int Mines
	{
		get
		{
			return xMines;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 4095)
			{
				value = 4095;
			}
			xMines = value;
			checked
			{
				xData[tInstallations] = (byte)(value & 0xFF);
				xData[tInstallations + 1] = (byte)Math.Round((double)(xData[tInstallations + 1] & 0xF0) + Conversion.Int((double)value / 256.0));
			}
		}
	}

	public int Defenses
	{
		get
		{
			return xDefenses;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 4095)
			{
				value = 4095;
			}
			xDefenses = value;
			checked
			{
				xData[tInstallations + 3] = (byte)(value & 0xFF);
				xData[tInstallations + 4] = (byte)Math.Round((double)(xData[tInstallations + 4] & 0xF0) + Conversion.Int((double)value / 256.0));
			}
		}
	}

	public int Unknown1
	{
		get
		{
			return xUnknown1;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 15)
			{
				value = 15;
			}
			xUnknown1 = value;
			checked
			{
				xData[tInstallations + 4] = (byte)((xData[tInstallations + 4] & 0xF) + value * 16);
			}
		}
	}

	public int Unknown2
	{
		get
		{
			return xUnknown2;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 255)
			{
				value = 255;
			}
			xUnknown2 = value;
			checked
			{
				xData[tInstallations + 5] = (byte)value;
			}
		}
	}

	public int Unknown3
	{
		get
		{
			return xUnknown3;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 255)
			{
				value = 255;
			}
			xUnknown3 = value;
			checked
			{
				xData[tInstallations + 6] = (byte)value;
			}
		}
	}

	public int StarbaseUnknown1
	{
		get
		{
			return xStarbaseUnknown1;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 255)
			{
				value = 255;
			}
			xStarbaseUnknown1 = value;
			checked
			{
				xData[tStarbase + 1] = (byte)value;
			}
		}
	}

	public int MDTarget
	{
		get
		{
			return xMDTarget;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 1023)
			{
				value = 0;
			}
			xMDTarget = value;
			checked
			{
				xData[tStarbase + 2] = (byte)(value & 0xFF);
				xData[tStarbase + 3] = (byte)Math.Round((double)(xData[tStarbase + 3] & 0xFC) + Conversion.Int((double)value / 256.0));
			}
		}
	}

	public int MDSpeed
	{
		get
		{
			return xMDSpeed;
		}
		set
		{
			if (value < 4)
			{
				value = 4;
			}
			if (value > 16)
			{
				value = 16;
			}
			xMDSpeed = value;
			checked
			{
				xData[tStarbase + 3] = (byte)((xData[tStarbase + 3] & 3) + (value - 4) * 4);
			}
		}
	}

	public int PlanetID
	{
		get
		{
			return xPlanetID;
		}
		set
		{
			if (value >= 0 && value <= 1023)
			{
				xPlanetID = value;
				byte[] data = xData;
				int Start = 0;
				object left = Operators.OrObject(Operators.AndObject(functions.GetBytes(data, ref Start, 2), 64256), value);
				xData[0] = Conversions.ToByte(Operators.ModObject(left, 256));
				xData[1] = Conversions.ToByte(Conversion.Int(Operators.DivideObject(left, 256)));
			}
		}
	}

	public int X
	{
		get
		{
			return xX;
		}
		set
		{
			xX = value;
		}
	}

	public int y
	{
		get
		{
			return xY;
		}
		set
		{
			xY = value;
		}
	}

	public int NameID
	{
		get
		{
			return xNameID;
		}
		set
		{
			xNameID = value;
			xName = PlanetNamesClass.PlanetNames[xNameID];
		}
	}

	public string Name => xName;

	public int OwnerID
	{
		get
		{
			return xOwnerID;
		}
		set
		{
			checked
			{
				if (unchecked(value >= 0 && value <= 15))
				{
					if (xOwnerID == -1)
					{
						xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 + 2 + 1]);
						Array.Copy(xData, tConcentration + 9, xData, tConcentration + 11, xData.Length - tConcentration - 11);
						xData[tConcentration + 9] = 0;
						xData[tConcentration + 10] = 0;
						tSurface += 2;
						tInstallations += 2;
						tStarbase += 2;
					}
					xData[1] = (byte)(xData[1] & (7 + value * 8));
					xOwnerID = value;
				}
				else
				{
					if (xOwnerID != -1)
					{
						Array.Copy(xData, tConcentration + 11, xData, tConcentration + 9, xData.Length - tConcentration - 11);
						xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 - 2 + 1]);
						tSurface -= 2;
						tInstallations -= 2;
						tStarbase -= 2;
					}
					xOwnerID = -1;
					xData[1] = (byte)(xData[1] & 0xFF);
				}
			}
		}
	}

	public bool Homeworld
	{
		get
		{
			return xHomeworld;
		}
		set
		{
			xHomeworld = value;
			checked
			{
				if (value)
				{
					xData[2] = (byte)(xData[2] | 0x80);
				}
				else
				{
					xData[2] = (byte)(xData[2] & 0x7F);
				}
			}
		}
	}

	public bool Scanner
	{
		get
		{
			return xScanner;
		}
		set
		{
			xScanner = value;
			checked
			{
				if (value)
				{
					xData[tInstallations + 5] = (byte)(xData[tInstallations + 5] & 0xFE);
				}
				else
				{
					xData[tInstallations + 5] = (byte)(xData[tInstallations + 5] | 1);
				}
			}
		}
	}

	public bool Artefact
	{
		get
		{
			return xArtefact;
		}
		set
		{
			xArtefact = value;
			checked
			{
				if (value)
				{
					xData[2] = (byte)(xData[3] | 0x10);
				}
				else
				{
					xData[2] = (byte)(xData[3] & 0xEF);
				}
			}
		}
	}

	public bool Terraformed => xTerraformed;

	public int Ironium
	{
		get
		{
			return xIronium;
		}
		set
		{
			if (value < 0)
			{
				xIronium = 0;
			}
			else if (value > int.MaxValue)
			{
				value = int.MaxValue;
			}
			xIronium = value;
			object objectValue = RuntimeHelpers.GetObjectValue(functions.GetBits(xData[tSurface], 0, 2));
			checked
			{
				int num = tSurface + 1;
				int num2 = tSurface + 1 + 3;
				for (int i = num; i <= num2; i++)
				{
					xData[i] = (byte)(value & 0xFF);
					value = (int)Math.Round(Conversion.Int((double)value / 256.0));
				}
			}
		}
	}

	public int Boranium
	{
		get
		{
			return xBoranium;
		}
		set
		{
			if (value < 0)
			{
				xBoranium = 0;
			}
			else if (value > int.MaxValue)
			{
				xBoranium = int.MaxValue;
			}
			else
			{
				xBoranium = value;
			}
			checked
			{
				int num = tSurface + 5;
				int num2 = tSurface + 5 + 3;
				for (int i = num; i <= num2; i++)
				{
					xData[i] = (byte)(value & 0xFF);
					value = (int)Math.Round(Conversion.Int((double)value / 256.0));
				}
			}
		}
	}

	public int Germanium
	{
		get
		{
			return xGermanium;
		}
		set
		{
			if (value < 0)
			{
				xGermanium = 0;
			}
			else if (value > int.MaxValue)
			{
				xGermanium = int.MaxValue;
			}
			else
			{
				xGermanium = value;
			}
			checked
			{
				int num = tSurface + 9;
				int num2 = tSurface + 9 + 3;
				for (int i = num; i <= num2; i++)
				{
					xData[i] = (byte)(value & 0xFF);
					value = (int)Math.Round(Conversion.Int((double)value / 256.0));
				}
			}
		}
	}

	public int Population
	{
		get
		{
			return xPopulation;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			else if (value > int.MaxValue)
			{
				value = int.MaxValue;
			}
			xPopulation = value;
			checked
			{
				int num = tSurface + 13;
				int num2 = tSurface + 13 + 3;
				for (int i = num; i <= num2; i++)
				{
					xData[i] = (byte)(value & 0xFF);
					value = (int)Math.Round(Conversion.Int((double)value / 256.0));
				}
			}
		}
	}

	public int IroniumConcentration
	{
		get
		{
			return xIroniumConcentration;
		}
		set
		{
			if (value < 0)
			{
				xIroniumConcentration = 0;
				return;
			}
			if (value >= 256)
			{
				xIroniumConcentration = 255;
			}
			else
			{
				xIroniumConcentration = value;
			}
			xData[tConcentration] = checked((byte)xIroniumConcentration);
		}
	}

	public int BoraniumConcentration
	{
		get
		{
			return xBoraniumConcentration;
		}
		set
		{
			if (value < 0)
			{
				xBoraniumConcentration = 0;
			}
			else if (value >= 256)
			{
				xBoraniumConcentration = 255;
			}
			else
			{
				xBoraniumConcentration = value;
			}
			checked
			{
				xData[tConcentration + 1] = (byte)xBoraniumConcentration;
			}
		}
	}

	public int GermaniumConcentration
	{
		get
		{
			return xGermaniumConcentration;
		}
		set
		{
			if (value < 0)
			{
				xGermaniumConcentration = 0;
			}
			else if (value >= 256)
			{
				xGermaniumConcentration = 255;
			}
			else
			{
				xGermaniumConcentration = value;
			}
			checked
			{
				xData[tConcentration + 2] = (byte)xGermaniumConcentration;
			}
		}
	}

	public int Temperature
	{
		get
		{
			return xTemperature;
		}
		set
		{
			if (value < 0)
			{
				xTemperature = 0;
			}
			else if (value >= 100)
			{
				xTemperature = 100;
			}
			else
			{
				xTemperature = value;
			}
			checked
			{
				xData[tConcentration + 4] = (byte)xTemperature;
			}
		}
	}

	public int Gravity
	{
		get
		{
			return xGravity;
		}
		set
		{
			if (value < 0)
			{
				xGravity = 0;
			}
			else if (value >= 100)
			{
				xGravity = 100;
			}
			else
			{
				xGravity = value;
			}
			checked
			{
				xData[tConcentration + 3] = (byte)xGravity;
			}
		}
	}

	public int Radiation
	{
		get
		{
			return xRadiation;
		}
		set
		{
			if (value < 0)
			{
				xRadiation = 0;
			}
			else if (value >= 100)
			{
				xRadiation = 100;
			}
			else
			{
				xRadiation = value;
			}
			checked
			{
				xData[tConcentration + 5] = (byte)xRadiation;
			}
		}
	}

	public int OriginalTemperature
	{
		get
		{
			return xOriginalTemperature;
		}
		set
		{
			if (value < 0)
			{
				xOriginalTemperature = 0;
			}
			else if (value >= 100)
			{
				xOriginalTemperature = 100;
			}
			else
			{
				xOriginalTemperature = value;
			}
			checked
			{
				xData[tConcentration + 7] = (byte)xOriginalTemperature;
			}
		}
	}

	public int OriginalGravity
	{
		get
		{
			return xOriginalGravity;
		}
		set
		{
			if (value < 0)
			{
				xOriginalGravity = 0;
			}
			else if (value >= 100)
			{
				xOriginalGravity = 100;
			}
			else
			{
				xOriginalGravity = value;
			}
			checked
			{
				xData[tConcentration + 6] = (byte)xOriginalGravity;
			}
		}
	}

	public int OriginalRadiation
	{
		get
		{
			return xOriginalRadiation;
		}
		set
		{
			if (value < 0)
			{
				xOriginalRadiation = 0;
			}
			else if (value >= 100)
			{
				xOriginalRadiation = 100;
			}
			else
			{
				xOriginalRadiation = value;
			}
			checked
			{
				xData[tConcentration + 8] = (byte)xOriginalRadiation;
			}
		}
	}

	public int EstimatedPopulation => xEstimatedPopulation;

	public int StarbaseSlotID
	{
		get
		{
			return xStarbaseSlotID;
		}
		set
		{
			if (value < -1)
			{
				value = -1;
			}
			if (value > 9)
			{
				value = 9;
			}
			checked
			{
				if (value == -1)
				{
					if (xStarbase != 0)
					{
						Array.Copy(xData, tStarbase + 4, xData, tStarbase, xData.Length - tStarbase - 4);
						xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 - 4 + 1]);
						xData[3] = (byte)(xData[3] & 0xFD);
						xStarbase = 0;
					}
				}
				else
				{
					if (~xStarbase != 0)
					{
						xData = (byte[])Utils.CopyArray(xData, new byte[xData.Length - 1 + 4 + 1]);
						Array.Copy(xData, tStarbase, xData, tStarbase + 4, xData.Length - tStarbase - 4);
						xData[3] = (byte)(xData[3] | 2);
						xStarbase = -1;
						xData[tStarbase + 1] = 0;
						xData[tStarbase + 2] = 0;
						xData[tStarbase + 3] = 0;
					}
					xData[tStarbase] = (byte)(xData[tStarbase] & (240 + value));
				}
				xStarbaseSlotID = value;
			}
		}
	}

	public object StarbaseDamage
	{
		get
		{
			return xStarbaseDamage;
		}
		set
		{
			if (Operators.ConditionalCompareObjectLess(value, 0, TextCompare: false))
			{
				value = 0;
			}
			if (Operators.ConditionalCompareObjectGreater(value, 511, TextCompare: false))
			{
				value = 511;
			}
			xStarbaseDamage = Conversions.ToInteger(value);
			xData[tStarbase] = Conversions.ToByte(Operators.AddObject(xData[tStarbase] & 0xF, Operators.AndObject(value, 15)));
			checked
			{
				xData[tStarbase + 1] = Conversions.ToByte(Operators.AddObject(xData[tStarbase + 1] & 0xE0, Conversion.Int(Operators.DivideObject(value, 16))));
			}
		}
	}

	public Planet(byte[] Data)
	{
		PlanetData = Data;
	}

	public Planet(int PlanetID)
	{
		xPlanetID = PlanetID;
	}
}
