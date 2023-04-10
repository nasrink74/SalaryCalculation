using Common.OvertimeMethod;
using Common.VieModel;
using System.Reflection;

namespace Common.Utilities
{
    public class GetReceipt
    {
        public static long GetReceiptForUser(GetRecieptDto getRecieptDto,params Assembly[] assemblies)
        {
            //نحوه ی محاسبه اضافه کار
            var basicSalaryAndAllowance = getRecieptDto.BasicSalary + getRecieptDto.Allowance;
            var tax = 9;

            var calculatorclasses = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(a => a.IsClass && !a.IsAbstract && !a.IsSealed && a.IsPublic
                && typeof(IOvertimeMethods).IsAssignableFrom(a) && a.Name == getRecieptDto.OverTimeCalculator).FirstOrDefault();

            if (calculatorclasses != null)
            {
                var method = calculatorclasses.GetMethods().FirstOrDefault();
                var result = method.Invoke(Activator.CreateInstance(calculatorclasses), new object[] { basicSalaryAndAllowance });


                //نحوه ی محاسبه حقوق دریافتی
                var receipt = getRecieptDto.BasicSalary + getRecieptDto.Allowance +
                    getRecieptDto.Transportation + (Convert.ToInt64(result) - tax);
                return receipt;
            }
            else
            {
                return 0;
            }
        }
    }
}
