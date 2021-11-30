using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedLab
{
   public class CheckingClass
    {
        public void IsNumeric(TextCompositionEventArgs e)
        {


            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }


        }
         public void IsLetter(TextCompositionEventArgs e)
        {

            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }

        }
        public void IsNumericOrLetter(TextCompositionEventArgs e)
        {

            if (!Char.IsLetterOrDigit(e.Text, 0))
            {
                e.Handled = true;

            }
        }
      
    }
}
