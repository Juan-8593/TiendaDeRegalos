using System.Collections.Generic;
using System.Linq;

public class Inventario
{
    private List<Producto> productos;

    public Inventario()
    {
        productos = new List<Producto>();
    }

    public void AgregarNuevoProducto(Producto producto)
    {
        productos.Add(producto);
    }

    public void EliminarProducto(string nombre)
    {
        var producto = productos.FirstOrDefault(p => p.Nombre == nombre);
        if (producto != null)
        {
            productos.Remove(producto);
        }
    }

    public void ActualizarCantidadProducto(string nombre, int nuevaCantidad)
    {
        var producto = productos.FirstOrDefault(p => p.Nombre == nombre);
        if (producto != null)
        {
            producto.Cantidad = nuevaCantidad;
        }
    }

    public List<Producto> ObtenerProductos()
    {
        return productos;
    }

    internal void Remove(Producto producto)
    {
        throw new NotImplementedException();
    }

    internal Producto Find(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }
}
