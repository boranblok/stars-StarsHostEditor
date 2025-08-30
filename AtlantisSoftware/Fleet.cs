using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AtlantisSoftware;

[ClassInterface(ClassInterfaceType.None)]
[ComClass]
public class Fleet : Fleet._Fleet
{
	[ComVisible(true)]
	public interface _Fleet
	{
		[DispId(1)]
		byte[] FleetData
		{
			[DispId(1)]
			get;
			[DispId(1)]
			set;
		}

		[DispId(2)]
		int PositionObjectID
		{
			[DispId(2)]
			get;
			[DispId(2)]
			set;
		}

		[DispId(3)]
		int FleetID
		{
			[DispId(3)]
			get;
			[DispId(3)]
			set;
		}

		[DispId(4)]
		int OwnerID
		{
			[DispId(4)]
			get;
			[DispId(4)]
			set;
		}

		[DispId(5)]
		int X
		{
			[DispId(5)]
			get;
			[DispId(5)]
			set;
		}

		[DispId(6)]
		int Y
		{
			[DispId(6)]
			get;
			[DispId(6)]
			set;
		}

		[DispId(7)]
		int Ironium
		{
			[DispId(7)]
			get;
			[DispId(7)]
			set;
		}

		[DispId(8)]
		int Boranium
		{
			[DispId(8)]
			get;
			[DispId(8)]
			set;
		}

		[DispId(9)]
		int Germanium
		{
			[DispId(9)]
			get;
			[DispId(9)]
			set;
		}

		[DispId(10)]
		int Fuel
		{
			[DispId(10)]
			get;
			[DispId(10)]
			set;
		}

		[DispId(11)]
		int BattlePlan
		{
			[DispId(11)]
			get;
			[DispId(11)]
			set;
		}

		[DispId(12)]
		int Unknown
		{
			[DispId(12)]
			get;
		}

		[DispId(13)]
		Collection Ships();
	}

	private int xFleetID;

	private int xOwnerID;

	private int xX;

	private int xY;

	private int xIronium;

	private int xBoranium;

	private int xGermanium;

	private int xPopulation;

	private int xFuel;

	private int xBattlePlan;

	private int xUnknown;

	private Collection xShips;

	private int xPositionObjectID;

	private byte[] xFleetData;

	public Collection Waypoints;

	public byte[] FleetData
	{
		get
		{
			return xFleetData;
		}
		set
		{
			xFleetData = value;
			int Start = 0;
			xFleetData = value;
			object objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 2));
			xFleetID = Conversions.ToInteger(Operators.AndObject(objectValue, 511));
			xOwnerID = Conversions.ToInteger(Operators.AndObject(Conversion.Int(Operators.DivideObject(objectValue, 512)), 15));
			objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 1));
			objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 1));
			objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 1));
			objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 1));
			int length = (Operators.ConditionalCompareObjectEqual(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue), 3, 1), 1, TextCompare: false) ? 1 : 2);
			object objectValue2 = RuntimeHelpers.GetObjectValue(functions.GetBit(Conversions.ToInteger(objectValue), 4));
			objectValue2 = 1;
			int num = Conversions.ToInteger(functions.GetBit(Conversions.ToInteger(objectValue), 6));
			num = 1;
			xPositionObjectID = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
			xX = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
			xY = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
			int value2 = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
			int num2 = 0;
			checked
			{
				do
				{
					if (Operators.ConditionalCompareObjectEqual(functions.GetBit(value2, num2), 1, TextCompare: false))
					{
						Ships ships = new Ships();
						ships.DesignID = num2;
						ships.ShipCount = Conversions.ToInteger(functions.GetBytes(value, ref Start, length));
						Ships().Add(ships, "ID" + Conversions.ToString(xOwnerID) + "-" + Conversions.ToString(num2));
					}
					num2++;
				}
				while (num2 <= 15);
				object objectValue3 = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 2));
				object objectValue4 = RuntimeHelpers.GetObjectValue(Conversion.Int(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue3), 0, 2), 1))));
				object objectValue5 = RuntimeHelpers.GetObjectValue(Conversion.Int(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue3), 2, 2), 1))));
				object objectValue6 = RuntimeHelpers.GetObjectValue(Conversion.Int(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue3), 4, 2), 1))));
				object objectValue7 = RuntimeHelpers.GetObjectValue(Conversion.Int(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue3), 6, 2), 1))));
				object objectValue8 = RuntimeHelpers.GetObjectValue(Conversion.Int(Operators.ExponentObject(2, Operators.SubtractObject(functions.GetBits(RuntimeHelpers.GetObjectValue(objectValue3), 8, 2), 1))));
				xIronium = Conversions.ToInteger(functions.GetBytes(value, ref Start, Conversions.ToInteger(objectValue4)));
				xBoranium = Conversions.ToInteger(functions.GetBytes(value, ref Start, Conversions.ToInteger(objectValue5)));
				xGermanium = Conversions.ToInteger(functions.GetBytes(value, ref Start, Conversions.ToInteger(objectValue6)));
				xPopulation = Conversions.ToInteger(functions.GetBytes(value, ref Start, Conversions.ToInteger(objectValue7)));
				xFuel = Conversions.ToInteger(functions.GetBytes(value, ref Start, Conversions.ToInteger(objectValue8)));
				if (Conversions.ToBoolean(objectValue2))
				{
					object objectValue9 = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 2));
					int num3 = 0;
					do
					{
						if (Operators.ConditionalCompareObjectEqual(functions.GetBit(Conversions.ToInteger(objectValue9), num3), 1, TextCompare: false))
						{
							foreach (Ships item in Ships())
							{
								if (item.DesignID == num3)
								{
									item.Damage = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
								}
							}
						}
						num3++;
					}
					while (num3 <= 15);
				}
				if (num != 0)
				{
					xBattlePlan = Conversions.ToInteger(functions.GetBytes(value, ref Start, 1));
				}
				xUnknown = Conversions.ToInteger(functions.GetBytes(value, ref Start, 1));
			}
		}
	}

	public int PositionObjectID
	{
		get
		{
			return xPositionObjectID;
		}
		set
		{
			xPositionObjectID = value;
			checked
			{
				xFleetData[6] = (byte)unchecked(value % 256);
				xFleetData[7] = (byte)Math.Round(Conversion.Int((double)value / 256.0));
			}
		}
	}

	public int FleetID
	{
		get
		{
			return xFleetID;
		}
		set
		{
			xFleetID = value;
		}
	}

	public int OwnerID
	{
		get
		{
			return xOwnerID;
		}
		set
		{
			xOwnerID = value;
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
			checked
			{
				xFleetData[8] = (byte)unchecked(value % 256);
				xFleetData[9] = (byte)Math.Round(Conversion.Int((double)value / 256.0));
			}
		}
	}

	public int Y
	{
		get
		{
			return xY;
		}
		set
		{
			xY = value;
			checked
			{
				xFleetData[10] = (byte)unchecked(value % 256);
				xFleetData[11] = (byte)Math.Round(Conversion.Int((double)value / 256.0));
			}
		}
	}

	public int Ironium
	{
		get
		{
			return xIronium;
		}
		set
		{
			xIronium = value;
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
			xBoranium = value;
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
			xGermanium = value;
		}
	}

	public int Fuel
	{
		get
		{
			return xFuel;
		}
		set
		{
			xFuel = value;
		}
	}

	public int BattlePlan
	{
		get
		{
			return xBattlePlan;
		}
		set
		{
			xBattlePlan = value;
		}
	}

	public int Unknown
	{
		get
		{
			return xUnknown;
		}
		internal set
		{
			xUnknown = value;
		}
	}

	public Fleet(byte[] Data)
	{
		xShips = new Collection();
		Waypoints = new Collection();
		FleetData = Data;
	}

	public Collection Ships()
	{
		return xShips;
	}

	Collection _Fleet.Ships()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Ships
		return this.Ships();
	}
}
