namespace MGCommunity.Services.App_Start
{
	using System;

	public static class DateTimeExtensions
	{
		public static string ToString(this DateTime? value)
		{
			return value.ToString(null as IFormatProvider);
		}

		public static string ToString(this DateTime? value, IFormatProvider formatProvider)
		{
			if (value.HasValue)
				return value.Value.ToString(formatProvider);
			else
				return string.Empty;
		}

		public static string ToString(this DateTime? value, string formatSpecifier)
		{
			return value.ToString(formatSpecifier, null);
		}

		public static string ToString(this DateTime? value, string formatSpecifier, IFormatProvider formatProvider)
		{
			if (value.HasValue)
				return value.Value.ToString(formatSpecifier, formatProvider);
			else
				return string.Empty;
		}

		public static string ToUniversalFormat(this DateTime date)
		{
			return GetUniversalFormatValue(date, null);
		}

		public static string ToUniversalFormat(this DateTime date, IFormatProvider formatProvider)
		{
			return GetUniversalFormatValue(date, formatProvider);
		}

		private static string GetUniversalFormatValue(DateTime date, IFormatProvider formatProvider)
		{
			string formatSpecifier = string.Empty;

			if (date.Year == DateTime.Now.Year)
				formatSpecifier = "{0:dddd, MMM d} at {0:h:mm tt}";
			else
				formatSpecifier = "{0:dddd, MMM d, yyyy} at {0:h:mm tt}";

			return string.Format(formatProvider, formatSpecifier, date);
		}

		public static string ToRelativeDateString(this DateTime date)
		{
			return GetRelativeDateValue(date, DateTime.Now, null);
		}

		public static string ToRelativeDateString(this DateTime date, IFormatProvider formatProvider)
		{
			return GetRelativeDateValue(date, DateTime.Now, formatProvider);
		}

		public static string ToRelativeDateStringUtc(this DateTime date)
		{
			return GetRelativeDateValue(date, DateTime.UtcNow, null);
		}

		public static string ToRelativeDateStringUtc(this DateTime date, IFormatProvider formatProvider)
		{
			return GetRelativeDateValue(date, DateTime.UtcNow, formatProvider);
		}

		private static string GetRelativeDateValue(DateTime date, DateTime comparedTo, IFormatProvider formatProvider)
		{
			TimeSpan diff = comparedTo.Subtract(date);
			if (diff.TotalDays >= 365)
				return string.Concat("на ", date.ToString("MMMM d, yyyy", formatProvider)).TranslateDay();
			if (diff.TotalDays >= 7)
				return string.Concat("на ", date.ToString("MMMM d", formatProvider).TranslateDay());
			else if (diff.TotalDays > 1)
				return string.Concat("преди " + diff.TotalDays.ToInt32().ToStringAsText(), " дена");
			else if (diff.TotalDays == 1)
				return "вчера";
			else if (diff.TotalHours >= 2)
				return string.Concat("преди " + diff.TotalHours.ToInt32().ToStringAsText(), " часа");
			else if (diff.TotalMinutes >= 60)
				return "преди повече от час";
			else if (diff.TotalMinutes >= 5)
				return string.Concat("преди " + diff.TotalMinutes.ToInt32().ToStringAsText(), " минути");
			if (diff.TotalMinutes >= 1)
				return "преди няколко минути";
			else
				return "преди по-малко от минута";
		}

		public static string TranslateDay(this string date)
		{
			string[] dateArgs = date.Split(new[] { ' ' });
			switch (dateArgs[1])
			{
				case "January":
					dateArgs[1] = "Януари"; break;
				case "February":
					dateArgs[1] = "Февруари"; break;
				case "March":
					dateArgs[1] = "Март"; break;
				case "April":
					dateArgs[1] = "Април"; break;
				case "May":
					dateArgs[1] = "Май"; break;
				case "June":
					dateArgs[1] = "Юни"; break;
				case "July":
					dateArgs[1] = "Юли"; break;
				case "August":
					dateArgs[1] = "Август"; break;
				case "September":
					dateArgs[1] = "Септември"; break;
				case "October":
					dateArgs[1] = "Октомври"; break;
				case "November":
					dateArgs[1] = "Ноември"; break;
				case "December":
					dateArgs[1] = "Декември"; break;
				default: break;
			}
			return String.Join(" ", dateArgs);
		}
	}

	public static class DoubleExtensions
	{
		public static int ToInt32(this double value)
		{
			return Convert.ToInt32(value);
		}
	}

	public static class Int32Extensions
	{
		public static bool Between(this int value, int lowerBound, int upperBound)
		{
			return value >= lowerBound && value <= upperBound;
		}

		public static bool BetweenExclusive(this int value, int lowerBound, int upperBound)
		{
			return value > lowerBound && value < upperBound;
		}

		public static string ToAbbreviatedFormat(this int value)
		{
			return value.ToAbbreviatedFormat(null);
		}

		public static string ToAbbreviatedFormat(this int value, IFormatProvider formatProvider)
		{
			if (value.BetweenExclusive(-1000, 1000))
				return value.ToString(formatProvider);
			else
				return string.Format(formatProvider, "{0:N0}k", value / 1000);
		}

		public static string ToStringAsText(this int value)
		{
			return value.ToStringAsText(null);
		}

		public static string ToStringAsText(this int value, IFormatProvider formatProvider)
		{
			return ToStringAsText(value, false, formatProvider);
		}

		public static string ToStringAsText(this int value, bool capitalizeText)
		{
			return ToStringAsText(value, capitalizeText, null);
		}

		public static string ToStringAsText(this int value, bool capitalizeText, IFormatProvider formatProvider)
		{
			switch (value)
			{
				case 1:
					if (capitalizeText)
						return "Един";
					else
						return "един";
				case 2:
					if (capitalizeText)
						return "Два";
					else
						return "два";
				case 3:
					if (capitalizeText)
						return "Три";
					else
						return "три";
				case 4:
					if (capitalizeText)
						return "Четири";
					else
						return "четири";
				case 5:
					if (capitalizeText)
						return "Пет";
					else
						return "пет";
				case 6:
					if (capitalizeText)
						return "Шест";
					else
						return "шест";
				case 7:
					if (capitalizeText)
						return "Седем";
					else
						return "седем";
				case 8:
					if (capitalizeText)
						return "Осем";
					else
						return "осем";
				case 9:
					if (capitalizeText)
						return "Девет";
					else
						return "девет";
				case 10:
					if (capitalizeText)
						return "Десет";
					else
						return "десет";

				default:
					return value.ToString("N0", formatProvider);
			}
		}
	}
}