//Proyecto final - Programación I
// POO en C#
// Dashboard financiero, orientado a empresas costeras (hoteles y/o restaurantes) para analizar decisiones en cuanto a la gestión de sargaso en las playas.

//Clase Abstracta: Gasto 

using System;
using System.Collections.Generic;

// Clase base que sirve como molde (Abstracción)
public abstract class Gasto
{
    public string Descripcion { get; set; }

    // Método polimórfico que cada subclase debe implementar
    public abstract double CalcularCostoMensual();
}

//Clases Derivadas: GastoManoDeObra e InversionActivo 

// Representa el gasto en personal manual (Herencia)
public class GastoManoDeObra : Gasto
{
    private int cantidadTrabajadores;
    private double tarifaPorHora;
    private double costoEquipo;

    public GastoManoDeObra(int cantidad, double tarifa, double equipo)
    {
        this.cantidadTrabajadores = cantidad;
        this.tarifaPorHora = tarifa;
        this.costoEquipo = equipo;
        this.Descripcion = "Mano de obra (limpieza manual)";
    }

    // Polimorfismo: implementación específica para trabajadores
    public override double CalcularCostoMensual()
    {
        // Asumiendo 160 horas laborales al mes por persona
        return (cantidadTrabajadores * tarifaPorHora * 160) + costoEquipo;
    }
}

// Representa la inversión en barreras (Herencia)
public class InversionActivo : Gasto
{
    private double precioCompra;
    private int añosDepreciacion;
    private double mantenimientoAnual;

    public InversionActivo(double precio, int años, double mantenimiento)
    {
        this.precioCompra = precio;
        this.añosDepreciacion = años;
        this.mantenimientoAnual = mantenimiento;
        this.Descripcion = "Inversión en Barrera Marítima";
    }

    // Polimorfismo: implementación basada en depreciación
    public override double CalcularCostoMensual()
    {
        double depreciacionMensual = precioCompra / (añosDepreciacion * 12);
        return depreciacionMensual + (mantenimientoAnual / 12);
    }
}

//Motor Principal: ModeloEconomico (Encapsulamiento)

public class ModeloEconomico
{
    private List<Gasto> listaGastos = new List<Gasto>();

    public void AgregarGasto(Gasto gasto)
    {
        listaGastos.Add(gasto);
    }

    public void MostrarAnalisis()
    {
        double totalMensual = 0;
        Console.WriteLine("--- Análisis de Impacto Financiero ---");
        foreach (var gasto in listaGastos)
        {
            double costo = gasto.CalcularCostoMensual();
            Console.WriteLine($"{gasto.Descripcion}: ${costo:F2}/mes");
            totalMensual += costo;
        }
        Console.WriteLine($"\nQUEMA TOTAL MENSUAL: ${totalMensual:F2}");
    }
}

// Ejecución del proyecto (método main)
class Program
{
    static void Main()
    {
        ModeloEconomico miModelo = new ModeloEconomico();

        // Opción A: Contratar 50 personas con rastrillos
        Gasto operacionManual = new GastoManoDeObra(50, 15.0, 500.0);
        
        // Opción B: Comprar una barrera de $50,000 con 5 años de vida útil
        Gasto barreraActivo = new InversionActivo(50000.0, 5, 2000.0);

        miModelo.AgregarGasto(operacionManual);
        miModelo.AgregarGasto(barreraActivo);

        miModelo.MostrarAnalisis();
        Console.ReadLine();
    }
}
