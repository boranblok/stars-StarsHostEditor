using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace AtlantisSoftware;

[ComClass]
[ClassInterface(ClassInterfaceType.None)]
public class Ships : Ships._Ships
{
	[ComVisible(true)]
	public interface _Ships
	{
		[DispId(1)]
		int DesignID
		{
			[DispId(1)]
			get;
			[DispId(1)]
			set;
		}

		[DispId(2)]
		int ShipCount
		{
			[DispId(2)]
			get;
			[DispId(2)]
			set;
		}

		[DispId(3)]
		int Damage
		{
			[DispId(3)]
			get;
			[DispId(3)]
			set;
		}
	}

	private int xSlot;

	private int xShipCount;

	private int xDamage;

	public int DesignID
	{
		get
		{
			return xSlot;
		}
		set
		{
			xSlot = value;
		}
	}

	public int ShipCount
	{
		get
		{
			return xShipCount;
		}
		set
		{
			xShipCount = value;
		}
	}

	public int Damage
	{
		get
		{
			return xDamage;
		}
		set
		{
			xDamage = value;
		}
	}

	[DebuggerNonUserCode]
	public Ships()
	{
	}
}
