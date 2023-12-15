using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Krosoft.Extensions.Samples.Library.Models.Enums;

[Flags]
public enum SampleCode
{
    [Description("Description for Value1")]
    [Display(Name = "Display Name for Value1")]
    Value1 = 1,

    [Description("Description for Value2")]
    [Display(Name = "Display Name for Value2")]
    Value2 = 2,

    [Description("Description for Value3")]
    [Display(Name = "Display Name for Value3")]
    Value3 = 4,

    [Description("Description for Value4")]
    [Display(Name = "Display Name for Value4")]
    Value4 = 8,

    Value5 = 16
}