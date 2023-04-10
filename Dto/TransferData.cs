using Dto.Incoms.Command;
using Entities;

namespace Dto
{
    public class TransferData
    {
        public EditIncomeDto editIncomeDto { get; set; }
        public long Receipt { get; set; }
        public List<Income> incomes { get; set; }
    }
}
