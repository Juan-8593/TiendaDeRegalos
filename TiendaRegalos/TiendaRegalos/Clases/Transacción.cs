using System.Collections.Generic;

public class Transaccion
{
    public List<Producto> ProductosComprados { get; set; }
    public decimal Total { get; set; }

    public Transaccion()
    {
        ProductosComprados = new List<Producto>();
        Total = 0;
    }

    public void CalcularTotal()
    {
        Total = 0;
        foreach (var producto in ProductosComprados)
        {
            Total += producto.Precio * producto.Cantidad;
        }
    }
}
