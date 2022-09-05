using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculator
{
    //继承
    public class OperationAdd : Calculate
    {
        public override double GetResultValue()
        {
            double result = NumberA + NumberB;
            return result;
        }
    }

    public class OperationSubtract : Calculate
    {
        public override double GetResultValue()
        {
            double result = NumberA - NumberB;
            return result;
        }
    }

    public class OperationRide : Calculate
    {
        public override double GetResultValue()
        {
            double result = NumberA * NumberB;
            return result;
        }
    }

    public class OperationDivide : Calculate
    {
        public override double GetResultValue()
        {
            double result = NumberA / NumberB;
            return result;
        }
    }

//X²
    public class OperationsNSquare : Calculate
    {
        public override double GetResultValue()
        {
            double result = NumberA * NumberA;
            return result;
        }
    }
}