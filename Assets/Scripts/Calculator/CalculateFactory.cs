
//计算工厂
namespace Calculator
{
    public static class CalculateFactory
    {
        public static Calculate CreatOperation(string operate)
        {
            Calculate opener = null;

            switch (operate)
            {
                case "+":
                    opener = new OperationAdd();
                    break;
                case "-":
                    opener = new OperationSubtract();
                    break;
                case "*":
                    opener = new OperationRide();
                    break;
                case "/":
                    opener = new OperationDivide();
                    break;
                case "X²":
                    opener = new OperationsNSquare();
                    break;
                default:
                    break;
            }

            return opener;
        }
    }
}