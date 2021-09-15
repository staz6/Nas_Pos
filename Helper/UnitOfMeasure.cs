using System.Runtime.Serialization;

namespace Nas_Pos.Helper
{
    public enum UnitOfMeasure
    {
        [EnumMember(Value = "Per unit")]
        Unit,
        [EnumMember(Value = "Per Kg")]
        Kg,
        [EnumMember(Value = "Per Gran")]
        Gram,
        [EnumMember(Value = "Per Ton")]
        Ton
    }
}