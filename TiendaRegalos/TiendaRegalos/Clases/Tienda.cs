using System;
using System.Collections.Generic;
using System.Linq;

public class Tienda
{
    private Inventario inventario;
    private Historial historialTransacciones;

    public Tienda()
    {
        inventario = new Inventario();
        historialTransacciones = new Historial();
    }

    public void AgregarNuevoProducto(string nombre, decimal precio, int cantidad)
    {
        var producto = new Producto(nombre, precio, cantidad);
        inventario.AgregarNuevoProducto(producto);
    }

    public void EliminarProducto(string nombre)
    {
        Producto producto = inventario.ObtenerProductos().FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

        if (producto != null)
        {
            Console.Write("¿Cuantas unidades desea eliminar? ");
            int cantidadAEliminar;

            if (!int.TryParse(Console.ReadLine(), out cantidadAEliminar) || cantidadAEliminar < 1)
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
                return;
            }

            if (cantidadAEliminar < producto.Cantidad)
            {
                producto.Cantidad -= cantidadAEliminar;
                Console.WriteLine($"Se han eliminado {cantidadAEliminar} unidades del producto '{producto.Nombre}'. Quedan {producto.Cantidad} en stock.");
            }
            else if (cantidadAEliminar == producto.Cantidad)
            {
                inventario.EliminarProducto(producto.Nombre);
                Console.WriteLine($"El producto '{producto.Nombre}' ha sido eliminado del inventario.");
            }
            else
            {
                Console.WriteLine($"No se puede eliminar {cantidadAEliminar} unidades. Solo hay {producto.Cantidad} en stock.");
            }
        }
        else
        {
            Console.WriteLine($"El producto '{nombre}' no se encuentra en el inventario.");
        }
    }

    public void ActualizarCantidadProducto(string nombre, int nuevaCantidad)
    {
        var producto = inventario.ObtenerProductos().FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        if (producto != null)
        {
            producto.Cantidad = nuevaCantidad;
        }
        else
        {
            Console.WriteLine($"El producto '{nombre}' esta fuera de stock");
        }
    }

    public List<Producto> ObtenerProductos()
    {
        return inventario.ObtenerProductos();
    }

    public void RealizarCompra()
    {
        var productosComprados = new List<Producto>();
        decimal total = 0;
        string continuar = "s";

        do
        {
            InventarioProductos();

            Console.Write("Producto deseado: ");
            string nombreProducto = Console.ReadLine();

            var producto = inventario.ObtenerProductos().FirstOrDefault(p => p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));

            if (producto != null)
            {
                Console.Write("Ingrese la cantidad: ");
                int cantidad;

                if (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 1)
                {
                    Console.WriteLine("Por favor, ingrese una cantidad válida.");
                    continue;
                }

                if (producto.Cantidad >= cantidad)
                {
                    var transaccion = new Transaccion();
                    transaccion.ProductosComprados.Add(new Producto(producto.Nombre, producto.Precio, cantidad));
                    transaccion.CalcularTotal();
                    historialTransacciones.AgregarTransaccion(transaccion);

                    producto.Cantidad -= cantidad;
                    total += transaccion.Total;
                    productosComprados.Add(new Producto(producto.Nombre, producto.Precio, cantidad));

                    Console.WriteLine($"Compra realizada: {producto.Nombre} x {cantidad}. Total: {transaccion.Total:C}");
                }
                else
                {
                    Console.WriteLine("La cantidad seleccionada excede la disponibilidad del producto");
                }
            }
            else
            {
                Console.WriteLine($"El producto '{nombreProducto}' no esta disponible por el momento");
            }

            Console.Write("¿Desea agregar otro producto? (s/n): ");
            continuar = Console.ReadLine();
        } while (continuar.Equals("s", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine("\nCarrito:");
        foreach (var productoComprado in productosComprados)
        {
            Console.WriteLine($" - {productoComprado.Nombre} x {productoComprado.Cantidad} a {productoComprado.Precio:C} cada uno");
        }
        decimal impuesto = total * 0.12m;
        decimal totalConImpuesto = total + impuesto;

        Console.WriteLine($"Total a pagar (sin impuestos): {total:C}");
        Console.WriteLine($"Impuesto (12%): {impuesto:C}");
        Console.WriteLine($"Total a pagar (con impuestos): {totalConImpuesto:C}");

        InventarioProductos();
    }



    public void VerHistorial()
    {
        var transacciones = historialTransacciones.ObtenerHistorial();
        foreach (var transaccion in transacciones)
        {
            Console.WriteLine($"Total de la compra: {transaccion.Total:C}");
            foreach (var producto in transaccion.ProductosComprados)
            {
                Console.WriteLine($" - {producto.Nombre} x {producto.Cantidad}");
            }
        }
    }

    public void InventarioProductos()
    {
        var productos = inventario.ObtenerProductos();
        Console.WriteLine("Inventario:");
        foreach (var producto in productos)
        {
            Console.WriteLine($"{producto.Nombre} - Precio: {producto.Precio:C}, Cantidad: {producto.Cantidad}");
        }
    }
}
