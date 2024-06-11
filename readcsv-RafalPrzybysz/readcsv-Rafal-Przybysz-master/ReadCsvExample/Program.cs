using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// 8. Procesory: nazwa kodowa, częstotliwość, ilość pamięci podręcznej na kolejnych poziomach 
/// (od 1 do 3cyfr; opcjonalnie: znak ’#’, od 1 do 3 cyfr; opcjonalnie: znak ’#’, od 1 do 3 cyfr
/// </summary>

namespace ReadCsv
{
	public class Article
	{
		public int Id { get; set; }

		#region Name
		private string _name;

		public string Name
		{
			get => _name;
			set
			{
				if (!_nameValidationRegex.IsMatch(value))
					throw new ArgumentException($"Invalid name value ({value})!");
				_name = value;
			}
		}

		private static readonly Regex _nameValidationRegex = new Regex("^[a-z]+$");
		#endregion

		#region Hertz
		private string _hertz;

		public string Hertz
		{
			get => _hertz;
			set
			{
				if (!_hertzValidationRegex.IsMatch(value))
					throw new ArgumentException($"Invalid hertz value ({value})!");
				_hertz = value;
			}
		}

		private static readonly Regex _hertzValidationRegex = new Regex("^\\d+$");
		#endregion

		#region Serial no
		private string _serialNo;

		public string SerialNo
		{
			get => _serialNo;
			set
			{
				if (!_serialNoValidationRegex.IsMatch(value))
					throw new ArgumentException($"Invalid serial no value ({value})!");
				_serialNo = value;
			}
		}

		private static readonly Regex _serialNoValidationRegex = new Regex("^\\d{1,3}(#)?\\d{1,3}(#)?\\d{1,3}$");
		#endregion


	}



	internal class Program
	{
		static void Main(string[] args)
		{
			var articles = new List<Article>();
			using (var textReader = new StreamReader("test.csv"))
			{
				while (!textReader.EndOfStream)
				{
					try
					{
						var line = textReader.ReadLine();
						var elements = line.Split(";");
						var article = new Article()
						{
							Id = int.Parse(elements[0].Trim()),
							Name = elements[1].Trim(),
							Hertz = elements[2].Trim(),
							SerialNo = elements[3].Trim()
						};
						articles.Add(article);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
			}

			Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-10}", "ID", "Nazwa kodowa", "częstotliwość", "Ilość pamięci podręcznej");
			foreach (var article in articles)
				Console.WriteLine($"{article.Id,-5} {article.Name,-10} {article.Hertz,-10} {article.SerialNo,-10}");
		}
	}
}
