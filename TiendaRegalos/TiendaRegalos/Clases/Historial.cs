using System.Collections.Generic;

public class Historial
{
    private List<Transaccion> transacciones;

    public Historial()
    {
        transacciones = new List<Transaccion>();
    }

    public void AgregarTransaccion(Transaccion transaccion)
    {
        transacciones.Add(transaccion);
    }

    public List<Transaccion> ObtenerHistorial()
    {
        return transacciones;
    }
}
