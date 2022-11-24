namespace DanielChallenge {
	public class Person {
		private List<Transaction> _transactionsHistory = new List<Transaction>();

		public Person(String name) {
			this.Name = name;
		}

		public String Name { get; }

		public IReadOnlyList<Transaction> Transactions {
			get {
				return this._transactionsHistory;
			}
		}

		public void AddTransaction(Transaction transaction) {
			this._transactionsHistory.Add(transaction);
		}
	}
}