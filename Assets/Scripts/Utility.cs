using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class Utility
{
	public static Color HueShift(this Color color, float amount)
	{
		var hsv = color.ToHSV();
		hsv.r = (hsv.r + amount) % 1f;

		return hsv.ToRGB();
	}

	public static Color ToHSV(this Color RgbColor)
	{
		float hue = 0f;
		float saturation = 0f;
		float value = 0f;
		float d = 0f;
		float h = 0f;

		float minRgb = Mathf.Min(RgbColor.r, Mathf.Min(RgbColor.g, RgbColor.b));
		float maxRgb = Mathf.Max(RgbColor.r, Mathf.Max(RgbColor.g, RgbColor.b));

		if (minRgb == maxRgb)
			return new Color(0f, 0f, minRgb, RgbColor.a);

		if (RgbColor.r == minRgb)
			d = RgbColor.g - RgbColor.b;
		else if (RgbColor.b == minRgb)
			d = RgbColor.r - RgbColor.g;
		else
			d = RgbColor.b - RgbColor.r;

		if (RgbColor.r == minRgb)
			h = 3f;
		else if (RgbColor.b == minRgb)
			h = 1f;
		else
			h = 5f;

		hue = (60f * (h - d / (maxRgb - minRgb))) / 360f;
		saturation = (maxRgb - minRgb) / maxRgb;
		value = maxRgb;

		return new Color(hue, saturation, value, RgbColor.a);
	}

	public static Color ToRGB(this Color HsvColor)
	{
		float red = 0f;
		float green = 0f;
		float blue = 0f;
		float maxHSV = 255f * HsvColor.b;
		float minHSV = maxHSV * (1f - HsvColor.g);
		float h = HsvColor.r * 360f;
		float z = (maxHSV - minHSV) * (1f - Mathf.Abs((h / 60f) % 2f - 1f));

		if (0f <= h && h < 60f)
		{
			red = maxHSV;
			green = z + minHSV;
			blue = minHSV;
		}
		else if (60f <= h && h < 120f)
		{
			red = z + minHSV;
			green = maxHSV;
			blue = minHSV;
		}
		else if (120f <= h && h < 180f)
		{
			red = minHSV;
			green = maxHSV;
			blue = z + minHSV;
		}
		else if (180f <= h && h < 240f)
		{
			red = minHSV;
			green = z + minHSV;
			;
			blue = maxHSV;
		}
		else if (240f <= h && h < 300f)
		{
			red = z + minHSV;
			green = minHSV;
			blue = maxHSV;
		}
		else if (300f <= h && h < 360f)
		{
			red = maxHSV;
			green = minHSV;
			blue = z + minHSV;
		}

		return new Color(red / 255f, green / 255f, blue / 255f, HsvColor.a);
	}
}
