using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P6_3_1204039_FormControl
{
    public partial class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                try
                {
                    int.Parse(value);
                    base.Text = value;
                    return;
                }
                catch
                { }
                if (value == null)
                {
                    base.Text = value;
                    return;
                }
            }
        }

        //Raising & overriding OnKeyPress event
        public delegate void InvalidUserEntryEvent(object sender, KeyPressEventArgs e);
        public event InvalidUserEntryEvent InvalidUserEntry;

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            int asciiInteger = Convert.ToInt32(e.KeyChar);
            if (asciiInteger >= 47 && asciiInteger <= 57)
            {
                //if the value of the ASCII converted char type (e.KeyChar) represents 0-9 
                //pass the event to windows for default processing 
                e.Handled = false;
                return;
            }
            // If the value of the ASCII converted char type(e.KeyChar) represents BACKSPACE
            //pass the event to Windows for default processing
            if (asciiInteger == 0)
            {
                e.Handled = false;
                return;
            }
            //If the value of the ASCII converted char type (e. KeyChar) is anything else 
            //handle the event here by setting Handled=true which prevents the event from being 
            //passed Windows for default processing 
            e.Handled = true;
            if (InvalidUserEntry != null)
                InvalidUserEntry(this, e);
        }
    }
}
