using System;
using System.Collections.Generic;

namespace apCaminhosMarte.Data
{
    public class LsbItems : IComparable
    {
        private int id;
        private string text;

        public LsbItems(int id, string text)
        {
            this.id = id;
            this.text = text;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set 
            {
                text = value;
            }
        }

        public override string ToString()
        {
            return Text;
        }

        public int CompareTo(object comp)
        {
            var anotherLsbItems = comp as LsbItems;
            return Text.CompareTo(anotherLsbItems.Text);
        }

        public override bool Equals(object obj)
        {
            return obj is LsbItems items &&
                   Id == items.Id &&
                   Text == items.Text;
        }

        public override int GetHashCode()
        {
            int hashCode = 836823900;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            return hashCode;
        }
    }
}
