
namespace Calculator
{
    public class Calculate
    {
        //封装
        // public double NumberA 
        // {
        //     get { return _numberA; }
        //     set {  _numberA=value; }
        // }
        
        public double NumberA { get; set; }

        // public double NumberB
        // {
        //     get { return _numberB; }
        //     set { _numberB = value; }
        // }
        
        public double NumberB { get; set; }

        //多态
        public virtual double GetResultValue()
        {
            const double result = 0f;
            return result;
        }
    }
}