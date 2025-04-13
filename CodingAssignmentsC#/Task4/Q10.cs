using System;


namespace CodingAssignmentsC_.Task4
{
    class Q10
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ValidateCustomer("John Doe", "name"));         // Valid
            Console.WriteLine(ValidateCustomer("No@#Address", "address"));  // Invalid
            Console.WriteLine(ValidateCustomer("123-456-7890", "phone"));   // Valid
        }

        public static string ValidateCustomer(string data, string detail)
        {
            switch (detail.ToLower())
            {
                case "name":
                    // Name should contain only letters and spaces, each word capitalized
                    string[] words = data.Split(' ');
                    foreach (string word in words)
                    {
                        if (word.Length == 0 || !char.IsUpper(word[0])) return "Invalid name: Each word should start with uppercase";
                        for (int i = 1; i < word.Length; i++)
                        {
                            if (!char.IsLetter(word[i]) || !char.IsLower(word[i])) return "Invalid name: Only letters allowed and proper capitalization needed";
                        }
                    }
                    return "Valid name";

                case "address":
                    // Address should not contin special characters like @ # $ % 
                    foreach (char ch in data)
                    {
                        if (!(char.IsLetterOrDigit(ch) || ch == ' ' || ch == ',' || ch == '.'))
                        {
                            return "Invalid address: Special characters not allowed";
                        }
                    }
                    return "Valid address";

                case "phone":
                    // Format ###-###-####
                    if (data.Length == 12 &&
                        char.IsDigit(data[0]) && char.IsDigit(data[1]) && char.IsDigit(data[2]) &&
                        data[3] == '-' &&
                        char.IsDigit(data[4]) && char.IsDigit(data[5]) && char.IsDigit(data[6]) &&
                        data[7] == '-' &&
                        char.IsDigit(data[8]) && char.IsDigit(data[9]) && char.IsDigit(data[10]) && char.IsDigit(data[11]))
                    {
                        return "Valid phone number";
                    }
                    else
                    {
                        return "Invalid phone: Format should be ###-###-####";
                    }

                default:
                    return "Unknown detail type";
            }
        }
    }
}
