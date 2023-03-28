{
    string bank_code = "", account_number = "", iban = "";

    if (args.Length != 3 && args.Length != 2)
    {
        Console.WriteLine("Too few arguments");
        return;
    }

    if (args[0] != "build" && args[0] != "analyze")
    {
        Console.WriteLine("Invalid command, must be 'build' or 'analyze'");
        return;
    }

    if (args[0] == "build")
    {
        if (args[1].Length == 4) { bank_code = args[1]; }
        if (args[2].Length == 6) { account_number = args[2]; }

        if (args[2].Length != 6)
        {
            Console.WriteLine("Account number has wrong length, must contain 6 digit!");
            return;
        }
        if (args[1].Length != 4)
        {
            Console.WriteLine("Bank code has wrong length, must contain 4 digits");
            return;
        }

        var result = BuildIban(ref bank_code, ref account_number);
        Console.WriteLine(result);

    }

    if (args[0] == "analyze")
    {
        if (args[1].Length != 15) { Console.WriteLine("IBAN has wrong length, must contain 15 digits"); return; }
        if (args[1].Length == 15) { iban = args[1]; }
        if (iban.Substring(15, 0) != "7") { Console.WriteLine("Wrong national check digit, we currently only support 7"); return; }

        if (iban.Substring(0,2) != "NO") { Console.WriteLine("Wrong country code, we currently only support 'NO"); return; }
        var result = AnalyzeIban(ref iban, ref bank_code, ref account_number);
        Console.WriteLine(result);

        for (int i = 0; i < bank_code.Length; i++)
        {
            if (!char.IsDigit(bank_code[i]))
            { Console.WriteLine("Bank code must not contain letters"); return; }
        }

        for (int i = 0; i < account_number.Length; i++)
        {
            if (!char.IsDigit(account_number[i]))
            { Console.WriteLine("Account number must not contain letters"); return; }
        }
    }
}
string BuildIban(ref string bank_code, ref string account_number)
{
    return $"NO00{bank_code}{account_number}7";
}

string AnalyzeIban(ref string iban, ref string bank_code, ref string account_number)
{
    bank_code = iban.Substring(4, 4);
    account_number = iban.Substring(8, 6);
    return $"Bankcode: {bank_code}, Account number:{account_number}";
}

