namespace DanielChallenge {
	public static class Utils {
		public static String? ReadLine(String prompt, Boolean allowNull) {
			String? read;
			Boolean nullOrEmpty;
			do {
				Console.Write(prompt);
				read = Console.ReadLine();
				nullOrEmpty = String.IsNullOrWhiteSpace(read);
			} while (!allowNull && nullOrEmpty);

			return nullOrEmpty ? null : read;
		}

		public static T? ReadValue<T>(String prompt, Boolean allowNull, T? defaultValue = default)
			where T : IParsable<T> {
			String? value = ReadLine(prompt, allowNull);
			return value is null ? defaultValue : T.Parse(value, null);
		}
	}
}