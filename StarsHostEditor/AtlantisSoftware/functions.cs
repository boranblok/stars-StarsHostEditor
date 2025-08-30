using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AtlantisSoftware;

[StandardModule]
internal sealed class functions
{
	internal static object GetBytes(byte[] Data, ref int Start, int Length)
	{
		int num = 0;
		checked
		{
			int num2 = Start + Length - 1;
			int num3 = Start;
			for (int i = num2; i >= num3; i += -1)
			{
				if (i >= Data.Length)
				{
                    num = num * 256;
                }
				else
				{
					num = num * 256 + Data[i];
				}
			}
			Start += Length;
			return num;
		}
	}

	internal static object GetBit(int Value, int Bit)
	{
		double a = Conversion.Int((double)Value / Math.Pow(2.0, Bit));
		return checked((long)Math.Round(a)) & 1;
	}

	internal static object GetBits(object Value, object FirstBit, object Count)
	{
		object objectValue = RuntimeHelpers.GetObjectValue(Conversion.Int(Operators.DivideObject(Value, Operators.ExponentObject(2, FirstBit))));
		return Operators.AndObject(objectValue, Operators.SubtractObject(Operators.ExponentObject(2, Count), 1));
	}

	internal static object DeleteObject(ref byte[] Data, int Position)
	{
		int num = checked(Data[Position + 0] + (Data[Position + 1] & 3) * 256);
		double num2 = Conversion.Int((double)(int)Data[checked(Position + 1)] / 4.0);
		checked
		{
			Array.Copy(Data, Position + num + 2, Data, Position, Data.Length - Position - num - 2);
			Data = (byte[])Utils.CopyArray(Data, new byte[Data.Length - 1 - num - 2 + 1]);
			return 0;
		}
	}

	internal static object InsertObject(ref byte[] Data, int Type, byte[] NewObject, int Position)
	{
		int num = NewObject.Length;
		checked
		{
			Data = (byte[])Utils.CopyArray(Data, new byte[Data.Length - 1 + num + 2 + 1]);
			Array.Copy(Data, Position, Data, Position + num + 2, Data.Length - Position - num - 2);
			Data[Position] = (byte)(num & 0xFF);
			Data[Position + 1] = (byte)(((long)Math.Round(Conversion.Int((double)num / 256.0)) & 3) + Type * 4);
			Array.Copy(NewObject, 0, Data, Position + 2, num);
			return 0;
		}
	}
}
