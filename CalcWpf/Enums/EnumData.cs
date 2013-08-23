using System;
using System.Reflection;
using System.ComponentModel;

namespace CalcWpf.Enums
{
    public static class EnumData
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }

    public enum EOperators
    {
        [Description("+")]
        op_add,
        [Description("-")]
        op_sub,
        [Description("\u00F7")]
        op_div,
        [Description("\u00D7")]
        op_mul,
        [Description("\u00A0\u0078\u00B2\u00A0")] //\u00A0\u0078\u00B2\u00A0
        op_pow_2,
        [Description("\u00A0\u0078\u00A0\u036F\u00A0")] //\u00A0\u0078\u00A0\u036F\u00A0
        op_pow_x,
        [Description("\u00A0\u00B2\u221A\u00A0")] //\u00A0\u00B2\u221A\u00A0
        op_root_2,
        [Description("\u00A0\u036F\u221A\u00A0")] //\u00A0\u036F\u221A\u00A0
        op_root_x,
        [Description("=")]
        op_res,
        [Description("C")]
        op_clear,
        [Description(",")]
        op_comma,
        [Description("<")]
        op_back,
        [Description("\u00A0\u02D7")]
        op_negative
    }
}
