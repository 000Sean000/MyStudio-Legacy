using System;
using System.Drawing;

class Program
{
	static void Main4()
	{
		Point point = new Point(10, 20);
		Size size = new Size(5, 5);

		Point translatedPoint = point + size;
		Console.WriteLine($"Translated Point: {translatedPoint}");

		Point translatedPoint2 = point - size;
		Console.WriteLine($"Translated Point 2: {translatedPoint2}");
	}
}
