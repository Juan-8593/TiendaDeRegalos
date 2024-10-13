using System;

class Program
{
    static void Main(string[] args)
    {
        Tienda tienda = new Tienda();

        while (true)
        {
            Console.WriteLine("Menú Principal:");
            Console.WriteLine("1. Agregar Producto");
            Console.WriteLine("2. Eliminar Producto");
            Console.WriteLine("3. Actualizar Producto");
            Console.WriteLine("4. Realizar Compra");
            Console.WriteLine("5. Ver Historial de Transacciones");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Nombre del producto: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Precio: ");
                    decimal precio = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Cantidad: ");
                    int cantidad = Convert.ToInt32(Console.ReadLine());
                    tienda.AgregarNuevoProducto(nombre, precio, cantidad);
                    break;
                case "2":
                    Console.Write("Nombre del producto a eliminar: ");
                    tienda.EliminarProducto(Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Nombre del producto a actualizar: ");
                    string nombreActualizar = Console.ReadLine();
                    Console.Write("Nueva cantidad: ");
                    int nuevaCantidad = Convert.ToInt32(Console.ReadLine());
                    tienda.ActualizarCantidadProducto(nombreActualizar, nuevaCantidad);
                    break;
                case "4":
                    tienda.RealizarCompra();
                    break;
                case "5":
                    tienda.VerHistorial();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            tienda.InventarioProductos();
        }
    }
}
