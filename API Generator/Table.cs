using System.Collections;
using System.Collections.Generic;

namespace API_Generator
{
    public class Table : IEnumerable<Column>
    {
        public string Name { get; }
        
        private Column[] Columns { get; }

        public Table(string name, Column[] columns)
        {
            Name = name;
            Columns = columns;
        }

        public Column this[int index] => Columns[index];

        public IEnumerator<Column> GetEnumerator()
        {
            return ((IEnumerable<Column>) Columns).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}