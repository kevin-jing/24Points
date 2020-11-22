using System;
using System.Linq;

int[] Input = { 10, 10, 4, 4 };
int Target = 24;

try
{
    calc(Input, 0, "");
}
catch (ResultException ex)
{
    if (ex.Result)
        Console.WriteLine("Succeeded");
    else
        Console.WriteLine("Failed");
}

void calc (int[] A, int previousResult, string previousFormula)
{
    if (A.Length == 1)
    {
        var a = A[0];

        int t;
        string f;

        t = previousResult + a;
        f = previousFormula + $" + {a}";
        Console.WriteLine($"{f} = {t}");
        if (t == Target)
            throw new ResultException(true);

        t = previousResult - a;
        f = previousFormula + $" - {a}";
        Console.WriteLine($"{f} = {t}");
        if (t == Target)
            throw new ResultException(true);

        t = previousResult * a;
        f = previousFormula + $" * {a}";
        Console.WriteLine($"{f} = {t}");
        if (t == Target)
            throw new ResultException(true);

        t = previousResult % a == 0 ?
            previousResult / a :
            Int32.MinValue;
        f = previousFormula + $" / {a}";
        Console.WriteLine($"{f} = {t}");
        if (t == Target)
            throw new ResultException(true);

        return;
    }

    // there are more than 1 number in A.
    for(int i = 0; i < A.Length; i++)
    {
        var L = A.ToList();
        L.RemoveAt(i);
        int a = A[i];

        string formular;

        formular = previousFormula != "" ?
            $"{previousFormula} + {a}" :
            $"{a}";

        calc(L.ToArray(), previousResult + a, formular);

        formular = previousFormula != "" ?
            $"{previousFormula} - {a}" :
            $"{a}";

        calc(L.ToArray(), previousResult - a, formular);

        formular = previousFormula != "" ?
            $"{previousFormula} * {a}" :
            $"{a}";

        calc(L.ToArray(), previousResult * a, formular);

        formular = previousFormula != "" ?
            $"{previousFormula} / {a}" :
            $"{a}";

        calc(L.ToArray(), previousResult / a, formular);
    }
}

class ResultException : Exception
{
    public ResultException (bool result)
    {
        Result = result;
    }
    public bool Result;
}
