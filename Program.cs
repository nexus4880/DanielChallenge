using System.Text;

namespace DanielChallenge {
	public class Program {
		private static List<Person> _people = new List<Person>();

		public static void Main() {
			while (true) {
				EInputRoute route = GetBaseInput();
				Boolean operation = route switch {
					EInputRoute.Purchase => GetPurchase(),
					EInputRoute.History => GetHistory(),
					EInputRoute.None => false,
					_ => true
				};

				if (!operation) {
					Console.WriteLine("Operation was cancelled");
				}

				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
				Console.Clear();
			}
		}

		private static Boolean GetPurchase() {
			String? name = Utils.ReadLine("Enter name: ", true);
			if (name == null) {
				return false;
			}

			String? storeName = Utils.ReadLine("Enter store name: ", true);
			if (storeName == null) {
				return false;
			}

			Int32 books = Utils.ReadValue("Enter books: ", true, 0);
			if (books == 0) {
				return false;
			}

			Decimal pricePerBook = Utils.ReadValue<Decimal>("Price per book: ", false);
			Decimal tax = Utils.ReadValue<Decimal>("Tax: ", true);
			Person? person = _people.FirstOrDefault(p => p.Name.ToLower() == name);
			if (person == null) {
				_people.Add(person = new Person(name));
			}

			Decimal cost = books * pricePerBook * (1m + tax * 0.01m);
			person.AddTransaction(new Transaction(storeName, cost, books));
			Console.WriteLine($"Total: ${cost:0.00}");

			return true;
		}

		private static Boolean GetHistory() {
			String? name = Utils.ReadLine("Enter name: ", true)?.ToLower();
			if (name == null) {
				return false;
			}

			Person? person = _people.FirstOrDefault(p => p.Name.ToLower() == name);
			if (person == null) {
				return Utils.ReadLine("Failed to find person, try again [Y/n]?: ", true)?.ToLower() is "y" or null &&
					   GetHistory();
			}

			StringBuilder builder = new StringBuilder();
			builder.AppendLine($"{person.Name} has made {person.Transactions.Count} purchases");
			foreach (Transaction transaction in person.Transactions) {
				builder.AppendLine(
					$"{transaction.Amount} at {transaction.Store} where he bought {transaction.BookCount} book(s)");
			}

			Console.WriteLine(builder);

			return true;
		}

		private static EInputRoute GetBaseInput() {
			Console.WriteLine(
				$"Welcome to {Environment.UserName}'s book store, what would you like to do today?{Environment.NewLine}Your options are: purchase | history");
			String? input = Console.ReadLine();
			return input switch {
				"purchase" => EInputRoute.Purchase,
				"buy" => EInputRoute.Purchase,
				"history" => EInputRoute.History,
				_ => EInputRoute.None
			};
		}
	}
}