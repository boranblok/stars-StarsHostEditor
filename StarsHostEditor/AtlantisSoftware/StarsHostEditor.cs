using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AtlantisSoftware;

[ClassInterface(ClassInterfaceType.None)]
[ComClass]
public class StarsHostEditor : StarsHostEditor._StarsHostEditor
{
	[ComVisible(true)]
	public interface _StarsHostEditor
	{
		[DispId(6)]
		object PlanetCount
		{
			[DispId(6)]
			get;
		}

		[DispId(1)]
		long Load(string Filename);

		[DispId(2)]
		void SwapPlanets(int Planet1ID, int Planet2ID);

		[DispId(3)]
		long Save(string Filename);

		[DispId(4)]
		Collection Planets();

		[DispId(5)]
		Collection Races();
	}

	private byte[] hstFile;

	private byte[] xyFile;

	private int xPlayerCount;

	private int xPlanetCount;

	private string xGameName;

	private bool hstLoaded;

	private Collection xPlanets;

	private Collection xRaces;

	private Collection mFiles;

	public int[] PlanetIDs;

	public Collection Fleets;

	[SpecialName]
	private Fleet _0024STATIC_0024ReadBlock_002420511D5108101D5108108_0024LastFleet;

	public object PlanetCount => xPlanetCount;

	public StarsHostEditor()
	{
		hstLoaded = false;
		xPlanets = new Collection();
		xRaces = new Collection();
		mFiles = new Collection();
		PlanetIDs = new int[1025];
		Fleets = new Collection();
	}

	public long Load(string Filename)
	{
		xPlanets = new Collection();
		Decryptor decryptor = new Decryptor();
		checked
		{
			if (Operators.CompareString(Filename.Substring(Filename.Length - 4).ToUpper(), ".HST", TextCompare: false) == 0)
			{
				Filename = Filename.Substring(0, Filename.Length - 4);
			}
			xyFile = decryptor.OpenFile(Filename + ".xy");
			xPlayerCount = xyFile[28];
			xPlanetCount = xyFile[30] + xyFile[31] * 256;
			xGameName = "";
			int num = 52;
			while (xyFile[num] != 0)
			{
				xGameName += Conversions.ToString(Strings.Chr(xyFile[num]));
				num++;
				if (num > 83)
				{
					break;
				}
			}
			int num2 = 1000;
			int num3 = Conversions.ToInteger(Operators.SubtractObject(PlanetCount, 1));
			for (int i = 0; i <= num3; i++)
			{
				Planet planet = new Planet(i);
				planet.X = xyFile[84 + i * 4 + 0] + (xyFile[84 + i * 4 + 1] & 3) * 256 + num2;
				num2 = planet.X;
				planet.y = (int)Math.Round(Conversion.Int((double)unchecked((int)xyFile[checked(84 + i * 4 + 1)]) / 4.0) + (double)((xyFile[84 + i * 4 + 2] & 0x3F) * 64));
				planet.NameID = (int)Math.Round(Conversion.Int((double)unchecked((int)xyFile[checked(84 + i * 4 + 2)]) / 64.0) + (double)(xyFile[84 + i * 4 + 3] * 4));
				Planets().Add(planet, "ID" + Conversions.ToString(i));
				PlanetIDs[i] = i;
			}
			hstFile = decryptor.OpenFile(Filename + ".hst");
			hstLoaded = true;
			int hstPosition = 0;
			int type = default(int);
			int Size = default(int);
			do
			{
				byte[] Data = new byte[1025];
				ReadBlock(hstFile, ref hstPosition, ref Data, ref type, ref Size);
			}
			while (hstPosition != hstFile.Length);
			int num4 = xPlayerCount;
			for (int j = 1; j <= num4; j++)
			{
				byte[] item = decryptor.OpenFile(Filename + ".m" + Conversions.ToString(j));
				mFiles.Add(item);
			}
			long result = default(long);
			return result;
		}
	}

	long _StarsHostEditor.Load(string Filename)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Load
		return this.Load(Filename);
	}

	public void SwapPlanets(int Planet1ID, int Planet2ID)
	{
		checked
		{
			Planet planet = (Planet)Planets()[Planet1ID + 1];
			Planet planet2 = (Planet)Planets()[Planet2ID + 1];
			Planet planet3 = (Planet)Planets()[Planet1ID + 1];
			byte[] planetData = planet3.PlanetData;
			NewLateBinding.LateSetComplex(Planets()[Planet1ID + 1], null, "PlanetData", new object[1] { RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Planets()[Planet2ID + 1], null, "PlanetData", new object[0], null, null, null)) }, null, null, OptimisticSet: false, RValueBase: true);
			NewLateBinding.LateSetComplex(Planets()[Planet2ID + 1], null, "PlanetData", new object[1] { planetData }, null, null, OptimisticSet: false, RValueBase: true);
			NewLateBinding.LateSetComplex(Planets()[Planet1ID + 1], null, "PlanetID", new object[1] { Planet1ID }, null, null, OptimisticSet: false, RValueBase: true);
			NewLateBinding.LateSetComplex(Planets()[Planet2ID + 1], null, "PlanetID", new object[1] { Planet2ID }, null, null, OptimisticSet: false, RValueBase: true);
			PlanetIDs[Planet1ID] = Planet2ID;
			PlanetIDs[Planet2ID] = Planet1ID;
			foreach (Race item in Races())
			{
				if (item.Homeworld == Planet1ID)
				{
					item.Homeworld = Planet2ID;
				}
				else if (item.Homeworld == Planet2ID)
				{
					item.Homeworld = Planet1ID;
				}
			}
			foreach (Fleet fleet in Fleets)
			{
				if ((fleet.X == planet.X) & (fleet.Y == planet.y))
				{
					fleet.X = planet2.X;
					fleet.Y = planet2.y;
					fleet.PositionObjectID = planet2.PlanetID;
					foreach (Waypoint waypoint3 in fleet.Waypoints)
					{
						if ((waypoint3.X == planet.X) & (waypoint3.Y == planet.y))
						{
							waypoint3.X = planet2.X;
							waypoint3.Y = planet2.y;
							waypoint3.PositionObjectID = planet2.PlanetID;
						}
					}
				}
				else
				{
					if (!((fleet.X == planet2.X) & (fleet.Y == planet2.y)))
					{
						continue;
					}
					fleet.X = planet.X;
					fleet.Y = planet.y;
					fleet.PositionObjectID = planet.PlanetID;
					foreach (Waypoint waypoint4 in fleet.Waypoints)
					{
						if ((waypoint4.X == planet2.X) & (waypoint4.Y == planet2.y))
						{
							waypoint4.X = planet.X;
							waypoint4.Y = planet.y;
							waypoint4.PositionObjectID = planet.PlanetID;
						}
					}
				}
			}
		}
	}

	void _StarsHostEditor.SwapPlanets(int Planet1ID, int Planet2ID)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SwapPlanets
		this.SwapPlanets(Planet1ID, Planet2ID);
	}

	public long Save(string Filename)
	{
		checked
		{
			if (Operators.CompareString(Filename.Substring(Filename.Length - 4).ToUpper(), ".HST", TextCompare: false) == 0)
			{
				Filename = Filename.Substring(0, Filename.Length - 4);
			}
			int num = 0;
			do
			{
				int num2 = hstFile[num + 0] + (hstFile[num + 1] & 3) * 256;
				double num4;
				unchecked
				{
					double num3 = Conversion.Int((double)(int)hstFile[checked(num + 1)] / 4.0);
					num4 = num3;
				}
				if (num4 != 0.0)
				{
					if (num4 == 6.0)
					{
						byte[] data = hstFile;
						int Start = num + 2;
						object right = Operators.AndObject(functions.GetBytes(data, ref Start, 2), 15);
						Race race = (Race)xRaces[Operators.ConcatenateObject("ID", right)];
						functions.DeleteObject(ref hstFile, num);
						functions.InsertObject(ref hstFile, 6, race.RaceData, num);
						num2 = race.RaceData.Length;
					}
					else if (num4 != 8.0)
					{
						if (num4 == 13.0)
						{
							byte[] data2 = hstFile;
							int Start = num + 2;
							object right2 = Operators.AndObject(functions.GetBytes(data2, ref Start, 2), 1023);
							foreach (Planet item in Planets())
							{
								if (Operators.ConditionalCompareObjectEqual(item.PlanetID, right2, TextCompare: false))
								{
									functions.DeleteObject(ref hstFile, num);
									functions.InsertObject(ref hstFile, 13, item.PlanetData, num);
									num2 = item.PlanetData.Length;
								}
							}
						}
						else if (num4 == 16.0)
						{
							byte[] data3 = hstFile;
							int Start = num + 2;
							object objectValue = RuntimeHelpers.GetObjectValue(functions.GetBytes(data3, ref Start, 2));
							foreach (Fleet fleet3 in Fleets)
							{
								if (!Operators.ConditionalCompareObjectEqual(fleet3.FleetID + fleet3.OwnerID * 512, objectValue, TextCompare: false))
								{
									continue;
								}
								functions.DeleteObject(ref hstFile, num);
								for (int num5 = (int)Math.Round(Conversion.Int((double)unchecked((int)hstFile[checked(num + 1)]) / 4.0)); num5 == 20; num5 = (int)Math.Round(Conversion.Int((double)unchecked((int)hstFile[checked(num + 1)]) / 4.0)))
								{
									functions.DeleteObject(ref hstFile, num);
								}
								functions.InsertObject(ref hstFile, 16, fleet3.FleetData, num);
								num2 = fleet3.FleetData.Length;
								foreach (Waypoint waypoint3 in fleet3.Waypoints)
								{
									functions.InsertObject(ref hstFile, 20, waypoint3.WaypointData, num + num2 + 2);
									num2 = num2 + waypoint3.WaypointData.Length + 2;
								}
								break;
							}
						}
						else if (num4 != 20.0 && num4 != 26.0 && num4 != 30.0 && num4 != 43.0)
						{
						}
					}
				}
				num = num + 2 + num2;
			}
			while (num < hstFile.Length);
			Decryptor decryptor = new Decryptor();
			int count = mFiles.Count;
			for (int i = 1; i <= count; i++)
			{
				byte[] Data = (byte[])mFiles[i];
				int num6 = 0;
				do
				{
					int num7 = Data[num6 + 0] + (Data[num6 + 1] & 3) * 256;
					double num9;
					unchecked
					{
						double num8 = Conversion.Int((double)(int)Data[checked(num6 + 1)] / 4.0);
						num9 = num8;
					}
					if (num9 != 0.0 && num9 != 6.0 && num9 != 8.0)
					{
						if (num9 == 12.0)
						{
							byte[] array = new byte[37]
							{
								176, 13, 177, 217, 120, 9, 220, 26, 65, 80,
								214, 73, 173, 232, 212, 22, 213, 32, 49, 144,
								219, 125, 242, 214, 1, 6, 222, 219, 212, 40,
								7, 215, 13, 197, 22, 42, 159
							};
						}
						else if (num9 == 13.0)
						{
							int num10 = (Data[num6 + 2] + Data[num6 + 3] * 256) & 0x3FF;
							int num11 = -1;
							int num12 = 0;
							do
							{
								if (PlanetIDs[num12] == num10)
								{
									num11 = num12;
									break;
								}
								num12++;
							}
							while (num12 <= 1023);
							Planet planet2 = (Planet)Planets()["ID" + Conversions.ToString(num11)];
							functions.DeleteObject(ref Data, num6);
							functions.InsertObject(ref Data, 13, planet2.PlanetData, num6);
							num7 = planet2.PlanetData.Length;
						}
						else if (num9 == 16.0)
						{
							int num13 = Data[num6 + 2] + Data[num6 + 3] * 256;
							foreach (Fleet fleet4 in Fleets)
							{
								if (fleet4.FleetID + fleet4.OwnerID * 512 != num13)
								{
									continue;
								}
								functions.DeleteObject(ref Data, num6);
								for (int num14 = (int)Math.Round(Conversion.Int((double)unchecked((int)Data[checked(num6 + 1)]) / 4.0)); num14 == 20; num14 = (int)Math.Round(Conversion.Int((double)unchecked((int)Data[checked(num6 + 1)]) / 4.0)))
								{
									functions.DeleteObject(ref Data, num6);
								}
								functions.InsertObject(ref Data, 16, fleet4.FleetData, num6);
								num7 = fleet4.FleetData.Length;
								foreach (Waypoint waypoint4 in fleet4.Waypoints)
								{
									functions.InsertObject(ref Data, 20, waypoint4.WaypointData, num6 + num7 + 2);
									num7 = num7 + waypoint4.WaypointData.Length + 2;
								}
								break;
							}
						}
						else if (num9 != 20.0 && num9 != 26.0 && num9 != 30.0 && num9 != 43.0)
						{
						}
					}
					num6 = num6 + 2 + num7;
				}
				while (num6 < Data.Length);
				decryptor.SaveFile(Data, Filename + ".m" + Conversions.ToString(i));
			}
			decryptor.SaveFile(hstFile, Filename + ".hst");
			long result = default(long);
			return result;
		}
	}

	long _StarsHostEditor.Save(string Filename)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Save
		return this.Save(Filename);
	}

	public Collection Planets()
	{
		return xPlanets;
	}

	Collection _StarsHostEditor.Planets()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Planets
		return this.Planets();
	}

	public Collection Races()
	{
		return xRaces;
	}

	Collection _StarsHostEditor.Races()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Races
		return this.Races();
	}

	private void ReadBlock(byte[] file, ref int hstPosition, ref byte[] Data, ref int type, ref int Size)
	{
		checked
		{
			Size = file[hstPosition + 0] + (file[hstPosition + 1] & 3) * 256;
			type = (int)Math.Round(Conversion.Int((double)unchecked((int)file[checked(hstPosition + 1)]) / 4.0));
			Data = new byte[Size - 1 + 1];
			if (Size != 0)
			{
				Array.Copy(file, hstPosition + 2, Data, 0, Size);
			}
			switch (type)
			{
			case 6:
			{
				byte b = Data[0];
				Race race = new Race();
				race.RaceData = Data;
				xRaces.Add(race, "ID" + Conversions.ToString(b));
				break;
			}
			case 13:
			{
				int num2 = Data[0] + (Data[1] & 3) * 256;
				Planet planet = (Planet)Planets()["ID" + Conversions.ToString(num2)];
				planet.PlanetData = Data;
				break;
			}
			case 16:
			{
				Fleet item2 = new Fleet(Data);
				int num = Data[0] + Data[1] * 256;
				Fleets.Add(item2, "ID" + Conversions.ToString(num));
				_0024STATIC_0024ReadBlock_002420511D5108101D5108108_0024LastFleet = item2;
				break;
			}
			case 20:
			{
				Waypoint item = new Waypoint(Data);
				_0024STATIC_0024ReadBlock_002420511D5108101D5108108_0024LastFleet.Waypoints.Add(item);
				break;
			}
			}
			hstPosition = hstPosition + 2 + Size;
		}
	}
}
