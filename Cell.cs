using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace LifeSimulator2
{
    public class Cell<X, Y>
    {
        private X _x;
        private Y _y;

        public Cell(X first, Y second)
        {
            _x = first;
            _y = second;
        }

        public X First { 
            get { 
                return _x;
            } 
            set
            {
                this._x = value;
            }
        }
        public Y Second {
            get 
            {
                return _y;
            }
            set
            {
                this._y = value;
            }
        }

    }


}

