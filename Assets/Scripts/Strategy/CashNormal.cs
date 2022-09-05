using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CashNormal : CashSuper
{
    public override double GetResult(double money)
    {
        return money;
    }
}

public class CashRebate : CashSuper
{
    private double moneyRebate = 1;

    public CashRebate(string moneyRebate) 
    {
       this.moneyRebate =double.Parse(moneyRebate);
    }

    public override double GetResult(double money)
    {
        return money * moneyRebate;
    }
}

public class CashReturn : CashSuper
{
    private double moneyCondition = 1;
    private double moneyReturn = 1;

    public CashReturn(string moneyCondition,string moneyReturn) 
    {
        this.moneyCondition = double.Parse(moneyCondition);
        this.moneyReturn = double.Parse(moneyReturn);
    }

    public override double GetResult(double money)
    {
        double result = money;
        if (money>moneyCondition)
        {
            result -=Math.Floor(money / moneyCondition) * moneyReturn;
        }
        return result;
    }
}