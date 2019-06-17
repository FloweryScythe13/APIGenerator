using System;

namespace API_Generator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dataProvider = new DataProvider(connectionString: "Server= localhost; Database= FleetManagement; Integrated Security=True;");

            var tables = dataProvider.GetTables();

            foreach (var table in tables)
            {
                Console.WriteLine(table.Name);

                foreach (var column in table)
                {
                    Console.WriteLine($"   {column.Type.Name}{(column.Nullable ? "?" : "")} {column.Name}");
                }
                
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}

/*
 * Ideal objective is to import in the list of tables and the associated columns 
 * from a given database, then create C# classes and CRUD methods for them. 
 * 
 * 1. Creating Classes From Tables
 *    
 *    Take a Table t, which is a collection of Colums col. For each t, 
 *    create a new C# class with public properties col[1->n]. 
 *    Questions (parameters) to ask the user:
 *          a) Should these classes have corresponding interfaces?
 *          b) Should these classes inherit from a shared base structure? (e.g. how AuditModel
 *              and ModelBase are used in Fleet.DataAccess)
 *          c) Does a relationship exist between t and t2 such that the generated class for 
 *              table t should contain a complex-typed property of type t2Class (or collection thereof)?
 *          d) TODO
 *          
 *    Using Fleet.DataAccess.Asset as example, code generation would need to know the 
 *    following data:
 *          
 *          
 *          1) Fact that the class needs to have an interface, and the name of that interface: IAsset
                1b) Desired namespace of the interface class to be created, if different from implementation                        
            2) Fact that the class inherits from a shared generic base class AuditModel<T> with T here 
                    being Asset
                2b) Namespace of the generic base class, if different from current class namespace (this must be 
                        created before any other table classes)
 *          1) Using directives: Com.TheToroCo.Fleet.Model.FleetManagement.Contracts.Base,
 *                  Com.TheToroCo.Fleet.Model.FleetManagement.Contracts.Locations, 
 *                  Com.TheToroCo.Fleet.Model.FleetManagement.Contracts.Lookup,
                    Com.TheToroCo.Fleet.Model.FleetManagement.Contracts.Maintenance,
                    Com.TheToroCo.Fleet.Model.FleetManagement.Contracts.Media,
                    Com.TheToroCo.Fleet.Model.FleetManagement.Interfaces.Assets,
                    System,
                    System.Collections.Generic,
                    System.Linq;
            2) Namespace for the class: Com.TheToroCo.Fleet.Model.FleetManagement.Contracts.Assets          
            5) Name of the class: Asset (from Table t)
            6) Fact that class should be public
            7) Any needed keywords for class (partial, static, sealed, etc)
            8) Names, nullability, constraints, and data types of all columns from table, to turn into class properties (not including inherited props)
                    (from t.Columns)
            9) Whether each property should have only a getter, only a setter, or both? (TODO come back to this)
            10) Whether to create additional properties complex-typed as nested classes based on other 
                    foreign-key columns from the table t (or foreign keys of other tables pointing to table t?)
    
    Promising library to handle this class code generation: CsCodeGenerator (https://github.com/borisdj/CsCodeGenerator)
 *
 * 
 * 
 */

        
