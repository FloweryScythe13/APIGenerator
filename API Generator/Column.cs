using System;
using System.Data;
using System.Web.UI.WebControls;

namespace API_Generator
{
    public class Column
    {
        public string Name { get; }

        public bool Nullable { get; }

        public SqlDbType SqlDbType { get; }

        public Type Type { get; }

        public Column(string name, string nullable, string sqlDbType)
        {
            Name = name;
            Nullable = string.Compare(nullable, "yes", StringComparison.OrdinalIgnoreCase) == 0;
            SqlDbType = Enum.TryParse(sqlDbType, true, out SqlDbType value) ? value : SqlDbType.Variant; //Bug from original code that threw an exception

            switch (SqlDbType)
            {
                case SqlDbType.BigInt:
                {
                    Type = typeof(long);
                    break;
                }

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                {
                    Type = typeof(byte[]);
                    break;
                }

                case SqlDbType.Bit:
                {
                    Type = typeof(bool);
                    break;
                }

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                {
                    Type = typeof(string);
                    break;
                }

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.DateTime2:
                {
                    Type = typeof(DateTime);
                    break;
                }

                case SqlDbType.Decimal:
                {
                    Type = typeof(decimal);
                    break;
                }

                case SqlDbType.Float:
                {
                    Type = typeof(double);
                    break;
                }

                case SqlDbType.Int:
                {
                    Type = typeof(int);
                    break;
                }

                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                {
                    Type = typeof(decimal);
                    break;
                }

                case SqlDbType.Real:
                {
                    Type = typeof(float);
                    break;
                }

                case SqlDbType.UniqueIdentifier:
                {
                    Type = typeof(Guid);
                    break;
                }

                case SqlDbType.SmallInt:
                {
                    Type = typeof(short);
                    break;
                }

                case SqlDbType.TinyInt:
                {
                    Type = typeof(byte);
                    break;
                }
                //It is unknown right now what conversions (if any) exist between these SQL types and C# object types. Look into this as a bug.
                case SqlDbType.Variant:
                case SqlDbType.Udt:
                case SqlDbType.Structured:
                {
                    Type = typeof(object);
                    break;
                }

                case SqlDbType.Xml:
                {
                    Type = typeof(Xml);
                    break;
                }

                case SqlDbType.Time:
                {
                    Type = typeof(TimeSpan);
                    break;
                }

                case SqlDbType.DateTimeOffset:
                {
                    Type = typeof(DateTimeOffset);
                    break;
                }

                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}