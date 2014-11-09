using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkingAround.Models
{
    public class Animal : LivingModel
    {
        public Animal(int key, VisualContainer parent = null)
            : base(key, parent)
        {

        }
    }
}
