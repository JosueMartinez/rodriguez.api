using System.ComponentModel;

namespace Rodriguez.Data.Utils
{
    public enum EstadosBonos
    {
        [Description("Comprado")]
        Comprado,
        [Description("Cobrado")]
        Cobrado,
        [Description("Cancelado")]
        Cancelado
    }
}