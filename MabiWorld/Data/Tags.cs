using System.Text.RegularExpressions;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents a StringID that can be checked against.
	/// </summary>
	/// <remarks>
	/// Supports AND "&", OR "|", braces "()", and asterisks as wildcard.
	/// 
	/// Since the match check uses regex, more complicated checks than the
	/// ones in the examples could technically be done.
	/// </remarks>
	/// <example>
	/// var tags = new Tags("/animal/wolf/direwolf/beast/whitedirewolf/");
	/// tags.Matches("/wolf/"); // true
	/// tags.Matches("/wolf/*/beast/"); // true
	/// tags.Matches("/wolf/|/beast/"); // true
	/// tags.Matches("/wolf/&/beast/"); // true
	/// tags.Matches("/*direwolf/"); // true
	/// tags.Matches("/blackdirewolf/"); // false
	/// 
	/// var tags = new Tags("/equip/foot/agelimit_armorboots/steel/magicsmith_repairable/human_only/");
	/// tags.Matches("/equip/&/armorboots/|(/equip/&/agelimit_armorboots/)"); // true
	/// </example>
	public class Tags
	{
		/// <summary>
		/// Gets or sets the instance's value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="value"></param>
		public Tags()
		{
			this.Value = "";
		}

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="value"></param>
		public Tags(string value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Returns string representation.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>
		/// Converts StringId to string.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static implicit operator string(Tags tags)
		{
			return tags.ToString();
		}

		/// <summary>
		/// Converts string to StringId and returns it in a new instance.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static implicit operator Tags(string tags)
		{
			return new Tags(tags);
		}

		/// <summary>
		/// Returns true if this instance contains the given tag.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public bool Matches(string tag)
		{
			return this.Check(tag, 0);
		}

		/// <summary>
		/// Returns true if the given string matches the tags of this instance.
		/// </summary>
		/// <param name="tags"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		private bool Check(string tags, int level)
		{
			if (string.IsNullOrEmpty(tags))
				return false;

			var result = false;
			var length = tags.Length;
			var start = 0;
			var prevOperator = '|';
			var open = 0;

			for (var i = 0; i < length; ++i)
			{
				// End
				if (i == length - 1)
				{
					var val = tags.Substring(start, i - start + 1);
					bool eval;
					if (val[0] == '(')
						eval = Check(val.Substring(1, val.Length - 2), level + 1);
					else
						eval = this.Evaluate(val);
					if (prevOperator == '|') result |= eval; else if (prevOperator == '&') result &= eval; else result ^= eval;
					break;
				}

				// Sub
				if (tags[i] == '(')
				{
					open++;
					continue;
				}
				else if (tags[i] == ')')
				{
					if (open != 0)
					{
						open--;
						continue;
					}
				}

				// Or/And/Xor
				if (open == 0 && (tags[i] == '|' || tags[i] == '&' /*|| tags[i] == '^'*/))
				{
					var val = tags.Substring(start, i - start);
					bool eval;
					if (val[0] == '(')
						eval = Check(val.Substring(1, val.Length - 2), level + 1);
					else
						eval = Evaluate(val);
					if (prevOperator == '|') result |= eval; else if (prevOperator == '&') result &= eval; else result ^= eval;
					prevOperator = tags[i];
					start = i + 1;
					continue;
				}
			}

			return result;
		}

		/// <summary>
		/// Returns true if the given tag can be found inside this instance.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		private bool Evaluate(string tag)
		{
			if (this.Value == null || tag == null)
				return false;

			tag = tag.Trim().Replace("*", ".*");
			// TODO: Cache?
			return Regex.IsMatch(this.Value, tag, RegexOptions.IgnoreCase);
		}
	}
}
