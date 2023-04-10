using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class Income : BaseEntity
    {
        public long BasicSalary { get; set; }       //حقوقو پایه
        public long Allowance { get; set; }         //حق جذب
        public long Transportation { get; set; }    //حق ایاب و ذهاب
        public DateTime Date { get; set; }
        public long Receipt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.Property(a => a.BasicSalary).IsRequired();
            builder.Property(a => a.Allowance).IsRequired();
            builder.Property(a => a.Transportation).IsRequired();
            builder.HasOne(a => a.User).WithMany(a => a.Incomes).HasForeignKey(a => a.UserId);
        }
    }
}
