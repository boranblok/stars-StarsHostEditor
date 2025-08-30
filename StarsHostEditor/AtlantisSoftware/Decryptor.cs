using System;
using System.IO;
using Microsoft.VisualBasic;

namespace AtlantisSoftware;

internal class Decryptor
{
	private uint[] raw;

	private uint RndA;

	private uint RndB;

	public Decryptor()
	{
		raw = new uint[64]
		{
			3u, 5u, 7u, 11u, 13u, 17u, 19u, 23u, 29u, 31u,
			37u, 41u, 43u, 47u, 53u, 59u, 61u, 67u, 71u, 73u,
			79u, 83u, 89u, 97u, 101u, 103u, 107u, 109u, 113u, 127u,
			131u, 137u, 139u, 149u, 151u, 157u, 163u, 167u, 173u, 179u,
			181u, 191u, 193u, 197u, 199u, 211u, 223u, 227u, 229u, 233u,
			239u, 241u, 251u, 257u, 263u, 279u, 271u, 277u, 281u, 283u,
			293u, 307u, 311u, 313u
		};
	}

	public byte[] OpenFile(string Filename)
	{
		FileStream fileStream = new FileStream(Filename, FileMode.Open);
		MemoryStream memoryStream = new MemoryStream();
		BinaryReader file = new BinaryReader(fileStream);
		BinaryWriter outfile = new BinaryWriter(memoryStream);
		checked
		{
			int type = default(int);
			int Size = default(int);
			do
			{
				byte[] Data = new byte[1025];
				ReadBlock(file, ref Data, ref type, ref Size);
				DisplayBlock(ref Data, type, Size, outfile);
				if (type == 7)
				{
					int num = Data[10] + Data[11] * 256;
					int num2 = num - 1;
					for (int i = 0; i <= num2; i++)
					{
						ReadPlanet(file, ref Data);
						DisplayPlanet(ref Data, outfile);
					}
				}
			}
			while (fileStream.Position != fileStream.Length);
			fileStream.Close();
			byte[] array = new byte[(int)(memoryStream.Length - 1) + 1];
			memoryStream.Seek(0L, SeekOrigin.Begin);
			memoryStream.Read(array, 0, (int)memoryStream.Length);
			memoryStream.Close();
			return array;
		}
	}

	public void SaveFile(byte[] Bytes, string Filename)
	{
		MemoryStream memoryStream = new MemoryStream();
		FileStream fileStream = new FileStream(Filename, FileMode.Create);
		memoryStream.Write(Bytes, 0, Bytes.Length);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		BinaryReader file = new BinaryReader(memoryStream);
		BinaryWriter outfile = new BinaryWriter(fileStream);
		checked
		{
			int type = default(int);
			int Size = default(int);
			do
			{
				byte[] Data = new byte[1025];
				ReadBlock(file, ref Data, ref type, ref Size);
				DisplayBlock(ref Data, type, Size, outfile);
				if (type == 7)
				{
					int num = Data[10] + Data[11] * 256;
					int num2 = num - 1;
					for (int i = 0; i <= num2; i++)
					{
						ReadPlanet(file, ref Data);
						DisplayPlanet(ref Data, outfile);
					}
				}
			}
			while (memoryStream.Position != memoryStream.Length);
			fileStream.Close();
		}
	}

	public uint NextRandom()
	{
		checked
		{
			int num = (int)Math.Round((double)(unchecked((long)RndA % 53668L) * 40014) - Conversion.Int((double)RndA / 53668.0) * 12211.0);
			if (num < 0)
			{
				num += 2147483563;
			}
			RndA = (uint)num;
			int num2 = (int)Math.Round((double)(unchecked((long)RndB % 52774L) * 40692) - Conversion.Int((double)RndB / 52774.0) * 3791.0);
			if (num2 < 0)
			{
				num2 += 2147483399;
			}
			RndB = (uint)num2;
			if (num > num2)
			{
				return (uint)(num - num2);
			}
			return (uint)Math.Round(4294967296.0 + (double)(num - num2));
		}
	}

	public void Decrypt(ref byte[] Data, int Size)
	{
		checked
		{
			int num = Size - 1;
			for (int i = 0; i <= num; i += 4)
			{
				uint num2 = Data[i];
				num2 = (uint)(num2 + Data[i + 1] * 256);
				num2 = (uint)(num2 + Data[i + 2] * 65536);
				num2 = (uint)(num2 + unchecked((long)Data[checked(i + 3)]) * 16777216L);
				num2 ^= NextRandom();
				Data[i] = (byte)unchecked((long)num2 % 256L);
				Data[i + 1] = (byte)Math.Round(Conversion.Int((double)num2 / 256.0) % 256.0);
				Data[i + 2] = (byte)Math.Round(Conversion.Int((double)num2 / 65536.0) % 256.0);
				Data[i + 3] = (byte)Math.Round(Conversion.Int((double)num2 / 16777216.0) % 256.0);
			}
		}
	}

	public void InitDecrypt(int a, int b, int c, int d, int e)
	{
		int num = d & 0x1F;
		checked
		{
			int num2 = (int)((long)Math.Round(Conversion.Int((double)d / 32.0)) & 0x1F);
			if ((d & 0x400) != 0)
			{
				num += 32;
			}
			else
			{
				num2 += 32;
			}
			RndA = raw[num];
			RndB = raw[num2];
			for (num = ((e & 3) + 1) * ((c & 3) + 1) * ((b & 3) + 1) + a; num >= 1; num += -1)
			{
				NextRandom();
			}
		}
	}

	public void ProcessBlock(ref byte[] Data, byte type, int Size)
	{
		checked
		{
			if (type == 8)
			{
				int num = Data[14] + Data[15] * 256;
				int num2 = Data[12] + Data[13] * 256;
				int c = Data[10] + Data[11] * 256;
				int num3 = num2;
				int e = Data[4] + Data[5] * 256 + Data[6] * 65536 + Data[7] * 65536 * 256;
				num = (int)((long)Math.Round(Conversion.Int((double)num / 4096.0)) & 1);
				num2 &= 0x1F;
				num3 = (int)Math.Round(Conversion.Int((double)num3 / 32.0));
				InitDecrypt(num, num2, c, num3, e);
			}
			else
			{
				Decrypt(ref Data, Size);
			}
		}
	}

	public void ReadBlock(BinaryReader file, ref byte[] Data, ref int type, ref int Size)
	{
		file.Read(Data, 0, 2);
		checked
		{
			Size = Data[0] + (Data[1] & 3) * 256;
			type = (int)Math.Round(Conversion.Int((double)unchecked((int)Data[1]) / 4.0));
			if (Size != 0)
			{
				file.Read(Data, 0, Size);
			}
			ProcessBlock(ref Data, (byte)type, Size);
		}
	}

	public void DisplayBlock(ref byte[] Data, int type, int size, BinaryWriter outfile)
	{
		checked
		{
			outfile.Write((byte)unchecked(checked(type * 1024 + size) % 256));
			outfile.Write((byte)Math.Round(Conversion.Int((double)(type * 1024 + size) / 256.0)));
			int num = size - 1;
			for (int i = 0; i <= num; i++)
			{
				outfile.Write(Data[i]);
			}
		}
	}

	public void ReadPlanet(BinaryReader File, ref byte[] Data)
	{
		File.Read(Data, 0, 4);
	}

	public void DisplayPlanet(ref byte[] Data, BinaryWriter outfile)
	{
		int num = 0;
		do
		{
			outfile.Write(Data[num]);
			num = checked(num + 1);
		}
		while (num <= 3);
	}
}
