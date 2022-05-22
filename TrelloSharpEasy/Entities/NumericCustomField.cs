using System;

namespace TrelloSharpEasy.Entities
{
    public class NumericCustomField : CustomField<decimal>
    {
        public NumericCustomField(string id, string name, decimal value)
            :base(id, name, value)
        {

        }
    }
}
