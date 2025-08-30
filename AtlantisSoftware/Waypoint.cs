using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AtlantisSoftware;

public class Waypoint
{
	private int xFleetID;

	private int xOwnerID;

	private int xX;

	private int xY;

	private int xWarpSpeed;

	private int xPositionObjectID;

	private byte[] xWaypointData;

	public byte[] WaypointData
	{
		get
		{
			return xWaypointData;
		}
		set
		{
			int Start = 0;
			xWaypointData = value;
			xX = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
			xY = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
			xPositionObjectID = Conversions.ToInteger(functions.GetBytes(value, ref Start, 2));
			object objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 1));
			xWarpSpeed = Conversions.ToInteger(Conversion.Int(Operators.DivideObject(objectValue, 16)));
			objectValue = Operators.AndObject(objectValue, 15);
			object objectValue2 = RuntimeHelpers.GetObjectValue(functions.GetBytes(value, ref Start, 1));
		}
	}

	public int FleetID
	{
		get
		{
			return xFleetID;
		}
		internal set
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
		internal set
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
				xWaypointData[0] = (byte)unchecked(xX % 256);
				xWaypointData[1] = (byte)Math.Round(Conversion.Int((double)xX / 256.0));
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
				xWaypointData[2] = (byte)unchecked(xY % 256);
				xWaypointData[3] = (byte)Math.Round(Conversion.Int((double)xY / 256.0));
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
				xWaypointData[4] = (byte)unchecked(xPositionObjectID % 256);
				xWaypointData[5] = (byte)Math.Round(Conversion.Int((double)xPositionObjectID / 256.0));
			}
		}
	}

	public Waypoint(byte[] Data)
	{
		WaypointData = Data;
	}
}
